import { useEffect, useState } from "react";
import { handleMudarSenha } from "../../service/user/userService";
import { redirect, useNavigate } from "react-router-dom";
import Alert, { PropsAlert } from "../../components/alert";
import Container from "../../components/container";
import { handleFirstLogin } from "../../service/firstlogin";
import { Error } from "../../service/login/login.models";
import { SimplePasswordUpdate } from "../../service/user/user.models";


const NovaSenha = () => {
    const [show, setShow] = useState(false);
    const [showC, setShowC] = useState(false);
    const [senha, setSenha] = useState('');
    const [confirmar, setConfirmar] = useState('');
    const [alert, setAlert] = useState(false);
    const [message, setMessage] = useState('');
    const [alertType, setAlertType] = useState<PropsAlert["type"]>('alert alert-primary');
    const navigate = useNavigate();

    useEffect(() => {
        if (!handleFirstLogin()) navigate('/');
    }, [])

    const handleNewPassword = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        if (senha.trim() === '' || confirmar.trim() === '') {
            setMessage("Preencha todos os campos")
            setAlert(true)
            setAlertType('alert alert-danger')

            setTimeout(() => {
                setMessage('')
                setAlert(false)
                setAlertType('alert alert-primary')
            }, 5000)
            return;
        }


        const payload = {
            matricula: Number.parseInt(sessionStorage.getItem("matricula")!),
            newPassword: senha,
            confirmNewPassword: confirmar
        } as SimplePasswordUpdate

        handleMudarSenha(payload).then(res => {
            if (res.status === 200) {
                setMessage("Sua senha foi alterada! Você será redirecionado")
                setAlert(true)
                setAlertType('alert alert-success')

                setTimeout(() => {
                    setMessage('')
                    setAlert(false)
                    setAlertType('alert alert-primary')
                    sessionStorage.clear();
                    window.location.href = '/login'
                }, 2500)

            } else if (res.status === 400) {
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
                {alert ? <Alert type={alertType} message={message} /> : ''}

                <form onSubmit={handleNewPassword} className="w-75 p-5 rounded">
                    <h4>Nova senha</h4>
                    <div className="input-group input-group-lg mb-3">
                        <input
                            value={senha}
                            onChange={(e) => setSenha(e.target.value)}
                            type={show ? "text" : "password"}
                            className="form-control"
                            placeholder="Senha"
                        />
                        <button
                            onClick={() => setShow(!show)}
                            className="btn btn-outline-secondary"
                            type="button"><i className={!show ? "bi bi-eye-slash": "bi bi-eye"}></i></button>
                    </div>
                    <div className="input-group input-group-lg mb-3">
                        <input
                            value={confirmar}
                            onChange={(e) => setConfirmar(e.target.value)}
                            type={showC ? "text" : "password"}
                            className="form-control"
                            placeholder="Confirmar senha"
                        />
                        <button
                            onClick={() => setShowC(!showC)}
                            className="btn btn-outline-secondary"
                            type="button"><i className={!showC ? "bi bi-eye-slash": "bi bi-eye"}></i></button>
                    </div>
                    <button type="submit" className="btn btn-dark">Alterar senha</button>
                </form>
            </>
        </Container>
    )
}

export default NovaSenha;