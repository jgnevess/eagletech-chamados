import React, { useEffect, useState } from "react";
import Container from "../../components/container";
import { Link, useNavigate, useParams } from "react-router-dom";
import { handleActiveUser, handleDeleteUserDb, handleEditarUsuario, handleGetUsuario, handleResetarSenhaUser } from "../../service/user/userService";
import CadastrarUsuario from "../../components/cadastrarUsuario";
import Alert, { PropsAlert } from "../../components/alert";
import InputForm from "../../components/inputForm";
import { UserOut } from "../../service/login/login.models";
import { Error } from "../../service/user/user.models";
import { useFirstLogin } from "../../hooks/useFirstLogin";
import NotFound from "../notfound";

const UsuarioPage = () => {
  useFirstLogin();
  const [usuario, setUsuario] = useState<UserOut>();
  const [statusCode, setStatusCode] = useState(0)

  const [nomeCompleto, setNome] = useState('')
  const [email, setEmail] = useState('')
  const [tel, setTelefone] = useState('')
  const [funcao, setFuncao] = useState('SOLICITANTE')
  const [ativo, setAtivo] = useState(false)

  const [alert, setAlert] = useState(false);
  const [message, setMessage] = useState<any>('');
  const [alertType, setAlertType] = useState<PropsAlert["type"]>('alert alert-primary');

  const param = useParams()
  const navigate = useNavigate();

  useEffect(() => {
    if (param.matricula) {
      handleGetUsuario(param.matricula).then(res => {
        setStatusCode(res.status)
        if (res.status == 200) {
          const data = res.response as UserOut
          setNome(data.nomeCompleto)
          setEmail(data.username)
          setFuncao(data.funcao)
          setTelefone(data.telefone)
          setAtivo(data.ativo)
        }
      })
    }
    else {
      setMessage("Erro ao carregar usuário")
      setAlert(true)
      setAlertType('alert alert-danger')
      setTimeout(() => {
        setMessage('')
        setAlert(false)
        setAlertType('alert alert-primary')
      }, 5000)

    }
  }, [ativo])

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    var telefone = tel.replace("-", "").replace("(", "").replace(")", "").replace(" ", "")

    const user = {
      matricula: Number.parseInt(param.matricula!),
      nomeCompleto: nomeCompleto,
      telefone: telefone,
      funcao: funcao,
      email: email,
    }

    handleEditarUsuario(param.matricula!, user).then(res => {
      if (res.status === 200) {
        const data = res.response as UserOut
        setMessage(`Usuário ${data.nomeCompleto} Alterado com sucesso! Login com a matricula: ${data.matricula}`)
        setAlert(true)
        setAlertType('alert alert-success')
        setTimeout(() => {
          setMessage('')
          setAlert(false)
          setAlertType('alert alert-primary')
          navigate('/usuarios')
        }, 2500)

      } else if (res.status !== 200) {
        const data = res.response as Error
        setMessage(data.Error)
        setAlert(true)
        setAlertType('alert alert-danger')
        setTimeout(() => {
          setMessage('')
          setAlert(false)
          setAlertType('alert alert-primary')
        }, 5000)
      }
    });
  }

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    let value = e.target.value.replace(/\D/g, "");
    if (value.length > 11) value = value.slice(0, 11);
    value = value.replace(/(\d{2})(\d{5})(\d{0,4})/, "($1) $2-$3");
    setTelefone(value);
  };

  const handleDeleteuser = () => {

    if (param.matricula === sessionStorage.getItem("matricula")) {
      setMessage("Você não pode excluir seu próprio usuário")
      setAlert(true)
      setAlertType('alert alert-danger')
      setTimeout(() => {
        setMessage('')
        setAlert(false)
        setAlertType('alert alert-primary')
      }, 5000)
      return;
    }

    handleDeleteUserDb(param.matricula!).then(res => {
      if (res.status === 204) {
        setMessage("Usuário inativado com sucesso e não terá mais acesso ao sistema")
        setAlert(true)
        setAlertType('alert alert-success')
        setTimeout(() => {
          setMessage('')
          setAlert(false)
          setAlertType('alert alert-primary')
          navigate('/usuarios')
        }, 2500)

      } else if (res.status !== 200) {
        const data = res.response as Error
        setMessage(data.Error)
        setAlert(true)
        setAlertType('alert alert-danger')
        setTimeout(() => {
          setMessage('')
          setAlert(false)
          setAlertType('alert alert-primary')
        }, 5000)
      }
    });
  }

  const handleActiveuser = () => {
    handleActiveUser(param.matricula!).then(res => {
      if (res.status === 200) {
        setMessage(`Usuário está autorizado a usar o sistema`)
        setAlert(true)
        setAtivo(true)
        setAlertType('alert alert-success')
        setTimeout(() => {
          setMessage('')
          setAlert(false)
          setAlertType('alert alert-primary')
        }, 2500)

      } else if (res.status !== 200) {
        const data = res.response as Error
        setMessage(data.Error)
        setAlert(true)
        setAlertType('alert alert-danger')
        setTimeout(() => {
          setMessage('')
          setAlert(false)
          setAlertType('alert alert-primary')
        }, 5000)
      }
    });
  }

  const handleResetarSenha = () => {

    if (param.matricula === sessionStorage.getItem("matricula")) {
      setMessage("Você não pode resetar sua própria senha, vá em alterar senha")
      setAlert(true)
      setAlertType('alert alert-danger')
      setTimeout(() => {
        setMessage('')
        setAlert(false)
        setAlertType('alert alert-primary')
      }, 5000)
      return;
    }

    handleResetarSenhaUser(param.matricula!).then(res => {
      if (res.status === 200) {
        setMessage(`Senha resetada com sucesso`)
        setAlert(true)
        setAlertType('alert alert-success')
        setTimeout(() => {
          setMessage('')
          setAlert(false)
          setAlertType('alert alert-primary')
          navigate('/usuarios')
        }, 2500)

      } else if (res.status !== 200) {
        const data = res.response as Error
        setMessage(data.Error)
        setAlert(true)
        setAlertType('alert alert-danger')
        setTimeout(() => {
          setMessage('')
          setAlert(false)
          setAlertType('alert alert-primary')
        }, 5000)
      }
    });
  }

  return (
    <Container>
      <>
        {
          statusCode === 200 ?
            <div className="w-75">
              {alert ? <Alert type={alertType} message={message} /> : ''}
              <form onSubmit={handleSubmit} className="w-100 p-5 rounded">
                <h4>Editar usuário</h4>
                <InputForm inputStyle="input" id="nome" placeholder="Nome Completo" set={(e) => setNome(e.target.value)} type="text" value={nomeCompleto} />
                <InputForm inputStyle="input" id="email" placeholder="Username" type="email" value={email} disabled={true} />
                <InputForm inputStyle="input" id="telefone" placeholder="Telefone" set={handleChange} type="text" value={tel} />
                <div className="form-floating">
                  <select value={funcao} onChange={(e) => setFuncao(e.target.value)} className="form-select" id="funcao">
                    <option selected disabled>Selecione a função</option>
                    <option value="ADMIN">Administrador</option>
                    <option value="TECNICO">Técnico</option>
                    <option value="SOLICITANTE">Solicitante</option>
                  </select>
                  <label htmlFor="funcao">Selecione a função</label>
                </div>
                <div className="d-flex mt-3 gap-4 justify-content-between">
                  <button onClick={handleResetarSenha} type="button" className="btn btn-dark">Resetar senha</button>
                  {
                    ativo ?
                      <button data-bs-toggle="modal" data-bs-target="#deleteModal" type="button" className="btn btn-danger">Inativar usuário</button> :
                      <button onClick={handleActiveuser} type="button" className="btn btn-dark">Ativar usuário</button>
                  }
                  <button type="submit" className="btn btn-dark">Salvar as alterações</button>
                </div>
              </form>

              <div className="modal fade" id="deleteModal" aria-labelledby="deleteModalLabel" aria-hidden="true">
                <div className="modal-dialog">
                  <div className="modal-content">
                    <div className="modal-header">
                      <h1 className="modal-title fs-5" id="deleteModalLabel">Inativar usuário</h1>
                      <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div className="modal-body">
                      <p>Ao inativar este usuário, ele ficará inativo e não terá mais acesso ao sistema.</p>
                      <p>Deseja mesmo inativar?</p>
                    </div>
                    <div className="modal-footer">
                      <button type="button" className="btn btn-dark" data-bs-dismiss="modal">Não</button>
                      <button onClick={handleDeleteuser} type="button" className="btn btn-danger" data-bs-dismiss="modal">Sim</button>
                    </div>
                  </div>
                </div>
              </div>

            </div> :
            <>
              <h1>404</h1>
              <h2>Usuário não encontrado</h2>
              <Link to="/usuarios" className="btn btn-dark">Voltar ao inicio</Link>
            </>
        }
      </>
    </Container>
  );
}

export default UsuarioPage;
