import React, { useEffect, useState, useRef } from "react";
import Container from "../../components/container";
import {
    ChamadoDatails,
    handleBuscarChamado,
    handleChamadosByStatus,
    StatusChamado,
} from "../../service/chamado";
import { useNavigate } from "react-router-dom";
import TabelaChamados from "../../components/tabelaChamados";
import { useFirstLogin } from "../../hooks/useFirstLogin";
import { ResponseList } from "../../service/user/user.models";
import BtnChamados from "../../components/btn-chamados";
import InputForm from "../../components/inputForm";

const ChamadosAdmin = () => {
    useFirstLogin();

    const [chamados, setChamados] = useState<ChamadoDatails[]>([]);
    const [status, setStatus] = useState<StatusChamado>("ABERTO");
    const [page, setPage] = useState(1);
    const [numeroChamado, setNumeroChamado] = useState('');
    const [hasMore, setHasMore] = useState(true);
    const [loading, setLoading] = useState(false);
    const [filter, setFilter] = useState(true);

    const navigate = useNavigate();
    const containerRef = useRef<HTMLDivElement | null>(null);

    // Reset ao trocar o status
    useEffect(() => {
        setChamados([]);
        setPage(1);
        setHasMore(true);
    }, [status]);

    useEffect(() => {
        if (!hasMore) return;
        setLoading(true);

        handleChamadosByStatus(status, page, 20)
            .then((response) => {
                if (response.status !== 200) {
                    navigate("/admin");
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
    }, [page, status, hasMore, navigate]);


    const handleBuscarChamadoId = (e: string) => {
        if (!/^\d*$/.test(e)) {
            return;
        }
        else {
            setNumeroChamado(e);
        }
        setHasMore(true);
        setPage(1)
        setChamados([])
        if (e === "") {
            setFilter(true);
            handleChamadosByStatus(status, page, 20).then(res => {
                const data = res.data as ResponseList;
                const list = data.data as ChamadoDatails[]
                if (list.length === 0) {
                    setHasMore(false);
                    return;
                }
                setChamados(prev => [...prev, ...list]);

            })
        }
        else {
            setFilter(false);
            handleBuscarChamado(Number.parseInt(e)).then(response => {
                const data = response.data as ChamadoDatails
                const list = [data];
                setChamados(list)
            })
        }
    };

    return (
        <Container>
            <div
                ref={containerRef}
                className="d-flex flex-column justify-content-start w-100 mt-2"
                style={{ height: "95vh", width: "75vw", overflowY: "auto" }}
            >
                <InputForm inputStyle="input" id="numeroChamado" placeholder="Buscar por número do chamado" set={(e) => handleBuscarChamadoId(e.target.value)} type="text" value={numeroChamado} />
                <div className="d-flex justify-content-around w-100 p-3">
                    {filter ? [
                        { id: "Aberto", label: "Aberto", value: "ABERTO" },
                        { id: "Em_Andamento", label: "Em Andamento", value: "EM_ANDAMENTO" },
                        { id: "Fechado", label: "Fechado", value: "FECHADO" },
                    ].map(({ id, label, value }) => (
                        <div className="form-check" key={id}>
                            <input
                                onChange={(e) =>
                                    setStatus(e.target.value as StatusChamado)
                                }
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
                    )) : ''}
                </div>

                <TabelaChamados chamados={chamados} />
                <BtnChamados onLoadMore={() => setPage((prev) => prev + 1)} hasMore={hasMore} loading={loading} page={page} />
            </div>
        </Container>
    );
};

export default ChamadosAdmin;