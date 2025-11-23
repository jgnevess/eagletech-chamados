import React from "react";
import Container from "../../components/container";
import { Link } from "react-router-dom";
import { useFirstLogin } from "../../hooks/useFirstLogin";

const NaoAutorizado = () => {
    useFirstLogin();
    return (
        <Container>
            <>
                <h1>401</h1>
                <h2>Você não tem autorização para acessar essa página</h2>
                <Link className="btn btn-dark" to={"/"}>Voltar</Link>
            </>
        </Container>
    )
}

export default NaoAutorizado