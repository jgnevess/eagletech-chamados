import React, { useEffect, useState } from "react";
import Container from "../../components/container";
import { useFirstLogin } from "../../hooks/useFirstLogin";
import { Link } from "react-router-dom";

import img from "../../images/35932-1-desktop-computer.png";
import { handleChamadosByMatricula } from "../../service/chamado";
import { ResponseList } from "../../service/user/user.models";


const SolicitanteDashboard = () => {
    useFirstLogin();

    const [chamadosAbertos, setChamadosAbertos] = useState(0)
    const [chamadosAtendidos, setChamadosAtendidos] = useState(0)
    const [chamadosResolvidos, setChamadosResolvidos] = useState(0)

    useEffect(() => {
        const matricula = sessionStorage.getItem("matricula")
        handleChamadosByMatricula(Number.parseInt(matricula!), "ABERTO", 1, 1).then(res => {
            if (res.status === 200) {
                const data = res.data as ResponseList
                setChamadosAbertos(data.quantities.naoAtendidos!)
                setChamadosAtendidos(data.quantities.atendidos!)
                setChamadosResolvidos(data.quantities.resolvidos!)

            }
        })
    })

    return (
        <Container>
            <div className="w-75 d-flex align-items-center justify-content-around row" style={{ height: '80vh' }}>
                <h1>Resumo dos chamados</h1>
                <div className="col-12 col-xl-4">
                    <div className="card">
                        <div className="card-body">
                            <h5 className="card-title">Não atendidos</h5>
                            <h1 className="card-title">{chamadosAbertos}</h1>
                        </div>
                    </div>
                </div>
                <div className="col-12 col-xl-4">
                    <div className="card">
                        <div className="card-body">
                            <h5 className="card-title">Em atendimento</h5>
                            <h1 className="card-title">{chamadosAtendidos}</h1>
                        </div>
                    </div>
                </div>
                <div className="col-12 col-xl-4">
                    <div className="card">
                        <div className="card-body">
                            <h5 className="card-title">Resolvidos</h5>
                            <h1 className="card-title">{chamadosResolvidos}</h1>
                        </div>
                    </div>
                </div>
            </div>
        </Container>
    )
}

export default SolicitanteDashboard