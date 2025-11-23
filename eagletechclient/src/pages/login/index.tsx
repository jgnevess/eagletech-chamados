import React, { useEffect, useState } from "react";
import { handleLoginAsync } from "../../service/login/login";
import { useNavigate } from "react-router-dom";
import Alert, { PropsAlert } from "../../components/alert";
import Container from "../../components/container";
import { useFirstLogin } from "../../hooks/useFirstLogin";
import { Error, LoginCredentials, LoginResponse } from "../../service/login/login.models";

import "./login.css"
import logo from "../../images/logo.png"
import Loading from "../../components/loading";


const LoginPage = () => {
    useFirstLogin();


    const [username, setUsername] = useState('');
    const [senha, setSenha] = useState('');
    const [alert, setAlert] = useState(false);
    const [message, setMessage] = useState('');
    const [alertType, setAlertType] = useState<PropsAlert["type"]>('alert alert-primary');
    const [showSenha, setShowSenha] = useState(false);
    const [loading, setLoading] = useState(false);
    const navigate = useNavigate();

    useEffect(() => {
        if (sessionStorage.length > 0) {
            sessionStorage.clear();
            window.location.href = '/login'
        }
    }, [])

    const handleLogin = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        setLoading(true)
        const payload = { username: username, password: senha } as LoginCredentials
        handleLoginAsync(payload).then(res => {
            if (res.status === 200) {
                const data = res.response as LoginResponse

                sessionStorage.setItem("token", data.token);
                sessionStorage.setItem("role", data.role);
                sessionStorage.setItem("matricula", data.matricula.toLocaleString())
                sessionStorage.setItem("usuario", JSON.stringify(data.user));
                sessionStorage.setItem("first", JSON.stringify(data.firstLogin));

                if (data.firstLogin) {
                    navigate('/nova-senha')
                }
                else {
                    if (data.role === "ADMIN") {
                        return navigate('/admin')
                    }

                    if (data.role === "TECNICO") {
                        return navigate('/tec')
                    }

                    if (data.role === "SOLICITANTE") {
                        return navigate('/sol');
                    }

                    return navigate('/');
                }

            } else if (res.status === 400) {
                const data = res.response as Error
                setMessage(data.Error)
                setAlert(true)
                setAlertType('alert alert-danger')

                setTimeout(() => {
                    setMessage('')
                    setAlert(false)
                    setAlertType('alert alert-primary')
                }, 2500)
            }
        });
        setLoading(false)
    }


    return (
        <>
            {loading ? <Loading /> : <div className="row bg-login">
                <div className="text-light d-flex flex-column align-items-center" style={{ width: '10vh', position: "fixed", bottom: 10, right: 40, zIndex: 1050 }}>
                    <img src={logo} className="rounded mt-2" width={70} />
                    <h6>EAGLETECH</h6>
                    <p>SOLUTIONS</p>
                </div>
                <div className="col-12">
                    <div className="w-100 bg d-flex flex-column align-items-center justify-content-center"
                        style={{
                            height: '100vh'
                        }}>
                        {alert ? <Alert type={alertType} message={message} /> : ''}
                        <form onSubmit={handleLogin} className="p-5 rounded">
                            <h1 className="mb-5 fw-bold text-light">Login</h1>
                            <div className="input-group input-group-lg mb-3">
                                <span className="input-group-text input-icon-bg" id="visible-addon"><i className="bi bi-person-circle"></i></span>
                                <input value={username} onChange={(e) => setUsername(e.target.value)} type="text" className="form-control py-3 input-bg" id="floatingInput" placeholder="Username" />
                            </div>
                            <div className="mb-3">
                                <div className="input-group input-group-lg mb-3">
                                    <span className="input-group-text input-icon-bg" id="visible-addon"><i className="bi bi-shield-lock-fill"></i></span>
                                    <input
                                        value={senha}
                                        onChange={(e) => setSenha(e.target.value)}
                                        type={showSenha ? "text" : "password"}
                                        className="form-control py-3 input-bg"
                                        placeholder="Senha"
                                    />
                                    <button
                                        className="btn btn-light input-icon-eye-bg"
                                        type="button"
                                        onClick={() => setShowSenha(!showSenha)}
                                    >
                                        <i className={!showSenha ? "bi bi-eye-slash" : "bi bi-eye"}></i>
                                    </button>
                                </div>
                            </div>
                            <div className="d-grid gap-2">
                                <button className="btn btn-login btn-lg" disabled={loading}>Entrar</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>}
        </>
    )
}

export default LoginPage;