import React from "react";
import { ChamadoDatails } from "../../service/chamado";
import { useNavigate } from "react-router-dom";

interface Props {
    chamados: ChamadoDatails[]
}

const TabelaChamados = ({ chamados }: Props) => {

    const navigate = useNavigate()

    const handleClick = (numeroChamado: number) => {
        navigate(`chamado/${numeroChamado}`);
    }

    const Chamados = chamados?.map((c, k) => {
        return (
            <tr key={k} onClick={() => handleClick(c.numeroChamado)} style={{ cursor: "pointer" }}>
                <td>{c.numeroChamado}</td>
                <td>{c.titulo}</td>
                <td>{c.status}</td>
                <td>{c.categoria}</td>
                <td>{new Date(c.abertura).toLocaleString('pt-BR')}</td>
                <td>{c.fechamento !== '0001-01-01T00:00:00' ? new Date(c.fechamento).toLocaleString('pt-BR') : 'n/a'}</td>
                <td>{c.prioridade}</td>
            </tr>
        )
    })
    return (
        <div className="w-100 d-flex justify-content-center" style={{
            overflowY: "auto"
        }}>
            <table className="table table-hover w-100">
                <thead>
                    <tr>
                        <th scope="col">Nº</th>
                        <th scope="col">Título</th>
                        <th scope="col">Status</th>
                        <th scope="col">Categoria</th>
                        <th scope="col">Abertura</th>
                        <th scope="col">Fechamento</th>
                        <th scope="col">Prioridade</th>
                    </tr>
                </thead>
                <tbody>
                    {Chamados}
                </tbody>
            </table>
        </div>
    )
}


export default TabelaChamados;