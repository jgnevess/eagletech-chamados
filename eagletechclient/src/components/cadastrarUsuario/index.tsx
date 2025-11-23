import React, { useState } from "react";
import { handleRegister } from "../../service/login/login";
import Alert, { PropsAlert } from "../alert";
import InputForm from "../inputForm";
import { Error, UserIn, UserOut } from "../../service/login/login.models";

const CadastrarUsuario = () => {

    const [nomeCompleto, setNome] = useState('')
    const [email, setEmail] = useState('')
    const [tel, setTelefone] = useState('')
    const [funcao, setFuncao] = useState('SOLICITANTE')

    const [alert, setAlert] = useState(false);
    const [message, setMessage] = useState('');
    const [alertType, setAlertType] = useState<PropsAlert["type"]>('alert alert-primary');

    const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        var telefone = tel.replace("-", "").replace("(", "").replace(")", "").replace(" ", "")

        const user = {
            nomeCompleto,
            senha: '',
            telefone,
            funcao,
            username: email
        } as UserIn

        handleRegister(user).then(res => {
            if (res.status === 200) {
                const data = res.response as UserOut
                setMessage(`Usuário ${data.nomeCompleto} cadastrado com sucesso! Login com a matrícula: ${data.matricula}`)
                setAlert(true)
                setAlertType('alert alert-success')
                setEmail('')
                setNome('')
                setTelefone('')
                setFuncao('SOLICITANTE')
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

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        let value = e.target.value.replace(/\D/g, "");
        if (value.length > 11) value = value.slice(0, 11);
        value = value.replace(/(\d{2})(\d{5})(\d{0,4})/, "($1) $2-$3");
        setTelefone(value);
    };

    return (
        <>
            {alert ? <Alert type={alertType} message={message} /> : ''}
            <form onSubmit={handleSubmit} className="w-100 p-5 rounded">
                <h2>Cadastrar Usuário</h2>
                <InputForm inputStyle="input" id="nome" placeholder="Nome completo" set={(e) => setNome(e.target.value)} type="text" value={nomeCompleto} />
                <InputForm inputStyle="input" id="email" placeholder="Username" set={(e) => setEmail(e.target.value)} type="email" value={email} />
                <InputForm inputStyle="input" id="telefone" placeholder="Telefone" set={handleChange} type="text" value={tel} />
                <div className="form-floating">
                    <select value={funcao} onChange={(e) => setFuncao(e.target.value)} className="form-select" id="funcao">
                        <option selected disabled>Selecione uma função</option>
                        <option value="ADMIN">Administrador</option>
                        <option value="TECNICO">Técnico</option>
                        <option value="SOLICITANTE">Solicitante</option>
                    </select>
                    <label htmlFor="funcao">Selecione a função</label>
                </div>
                <div className="d-grid mt-3">
                    <button className="btn btn-dark">Cadastrar</button>
                </div>
            </form>
        </>

    )
}


export default CadastrarUsuario;