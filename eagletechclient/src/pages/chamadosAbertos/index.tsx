import React, { useEffect, useState, useRef } from "react";
import Container from "../../components/container";
import { ChamadoDatails, handleChamadosAbertos } from "../../service/chamado";
import { useNavigate } from "react-router-dom";
import TabelaChamados from "../../components/tabelaChamados";
import { useFirstLogin } from "../../hooks/useFirstLogin";
import { ResponseList } from "../../service/user/user.models";
import BtnChamados from "../../components/btn-chamados";

const ChamadosAbertos = () => {
    useFirstLogin();
    const [chamados, setChamados] = useState<ChamadoDatails[]>([]);
    const [page, setPage] = useState(1);
    const [hasMore, setHasMore] = useState(true);
    const [loading, setLoading] = useState(false);
    const navigate = useNavigate();
    const containerRef = useRef<HTMLDivElement | null>(null);

    useEffect(() => {
        if (!hasMore) return;
        setLoading(true);
        handleChamadosAbertos(20, page)
            .then((res) => {
                if (res.status !== 200) {
                    navigate("/tec");
                    return;
                }
                const data = res.data as ResponseList;
                const list = data.data as ChamadoDatails[];
                if (list.length === 0) {
                    setHasMore(false);
                    return;
                }
                setChamados((prev) => [...prev, ...list]);
                setTimeout(() => {
                    containerRef.current?.scrollTo({
                        top: containerRef.current.scrollHeight,
                        behavior: "smooth",
                    });
                }, 100);
            })
            .finally(() => setLoading(false));
    }, [page, hasMore]);

    return (
        <Container>
            <div
                ref={containerRef}
                className="d-flex flex-column justify-content-start w-100 mt-2"
                style={{ height: "95vh", width: "75vw", overflowY: "auto" }}
            >
                <TabelaChamados chamados={chamados} />
                <BtnChamados onLoadMore={() => setPage((prev) => prev + 1)} hasMore={hasMore} loading={loading} page={page} />
            </div>
        </Container>
    );
};

export default ChamadosAbertos;
