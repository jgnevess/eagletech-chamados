import React, { useEffect, useState, useRef } from "react";
import Container from "../../components/container";
import { ChamadoDatails, handleChamadosByMatriculaTecnico } from "../../service/chamado";
import { useNavigate } from "react-router-dom";
import TabelaChamados from "../../components/tabelaChamados";
import { useFirstLogin } from "../../hooks/useFirstLogin";
import { ResponseList } from "../../service/user/user.models";
import BtnChamados from "../../components/btn-chamados";

const ChamadosAtendidos = () => {
    useFirstLogin();

    const [chamados, setChamados] = useState<ChamadoDatails[]>([]);
    const [status, setStatus] = useState("EM_ANDAMENTO");
    const [page, setPage] = useState(1);
    const [hasMore, setHasMore] = useState(true);
    const [loading, setLoading] = useState(false);

    const navigate = useNavigate();
    const containerRef = useRef<HTMLDivElement | null>(null);
    const matricula = Number.parseInt(sessionStorage.getItem("matricula") || "0");

    // Reset ao trocar o status
    useEffect(() => {
        setChamados([]);
        setPage(1);
        setHasMore(true);
    }, [status]);

    useEffect(() => {
        if (!hasMore) return;
        setLoading(true);

        handleChamadosByMatriculaTecnico(matricula, status, 20, page)
            .then((response) => {
                if (response.status !== 200) {
                    navigate("/tec");
                    return;
                }

                const data = response.data as ResponseList;
                const list = data.data as ChamadoDatails[];

                if (list.length === 0) {
                    setHasMore(false);
                    return;
                }

                setChamados((prev) => [...prev, ...list]);

                // Scroll automático pro fim
                setTimeout(() => {
                    containerRef.current?.scrollTo({
                        top: containerRef.current.scrollHeight,
                        behavior: "smooth",
                    });
                }, 100);
            })
            .finally(() => setLoading(false));
    }, [page, status, hasMore, matricula, navigate]);

    return (
        <Container>
            <div
                ref={containerRef}
                className="d-flex flex-column justify-content-start w-100 mt-2"
                style={{ height: "95vh", width: "75vw", overflowY: "auto" }}
            >
                <div className="d-flex justify-content-around w-100 p-3">
                    {[
                        { id: "Em_Andamento", label: "Em Andamento", value: "EM_ANDAMENTO" },
                        { id: "Fechado", label: "Fechado", value: "FECHADO" },
                    ].map(({ id, label, value }) => (
                        <div className="form-check" key={id}>
                            <input
                                onChange={(e) => setStatus(e.target.value)}
                                className="form-check-input"
                                type="radio"
                                name="status"
                                id={id}
                                value={value}
                                checked={status === value}
                            />
                            <label className="form-check-label" htmlFor={id}>
                                {label}
                            </label>
                        </div>
                    ))}
                </div>

                <TabelaChamados chamados={chamados} />
                <BtnChamados onLoadMore={() => setPage((prev) => prev + 1)} hasMore={hasMore} loading={loading} page={page} />
            </div>
        </Container>
    );
};

export default ChamadosAtendidos;