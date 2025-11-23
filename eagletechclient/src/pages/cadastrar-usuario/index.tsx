import React from "react";
import Container from "../../components/container";
import CadastrarUsuario from "../../components/cadastrarUsuario";
import { useFirstLogin } from "../../hooks/useFirstLogin";


const CadastrarUsuarioPage = () => {
  useFirstLogin();
  return (
    <Container>
      <div className="row w-100 d-flex justify-content-center">
        <div className="col-12 col-xl-6">
          <CadastrarUsuario />
        </div>
      </div>
    </Container>
  )

}

export default CadastrarUsuarioPage;