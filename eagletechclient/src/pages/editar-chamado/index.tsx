import React, { useEffect, useState } from "react";
import { useFirstLogin } from "../../hooks/useFirstLogin";
import Container from "../../components/container";
import InputForm from "../../components/inputForm";
import { Categoria, Chamado, ChamadoAberto, ChamadoDatails, handleAbrirChamado, handleBuscarChamado, handleEditarChamado } from "../../service/chamado";
import Alert, { PropsAlert } from "../../components/alert";
import { useNavigate, useParams } from "react-router-dom";
import { Error, UserOut } from "../../service/login/login.models";

const EditarChamado = () => {

    useFirstLogin();
    const [numeroChamado, setNumeroChamado] = useState(0);
    const [titulo, setTitulo] = useState('');
    const [descricao, setDescricao] = useState('');
    const [categoria, setCategoria] = useState('Selecione uma categoria')

    const [alert, setAlert] = useState(false);
    const [message, setMessage] = useState('');
    const [alertType, setAlertType] = useState<PropsAlert["type"]>('alert alert-primary');

    const param = useParams();
    const navigate = useNavigate();

    const categorias = [
        { label: "Hardware", value: "HARDWARE" },
        { label: "Software", value: "SOFTWARE" },
        { label: "Rede", value: "REDE" },
        { label: "Impressora", value: "IMPRESSORA" },
        { label: "Sistema Operacional", value: "SISTEMA_OPERACIONAL" },
        { label: "Banco de Dados", value: "BANCO_DE_DADOS" },
        { label: "Segurança", value: "SEGURANCA" },
        { label: "Outros", value: "OUTROS" },
    ];

    const Categorias = categorias.map((c, k) => {
        return <option key={k} value={c.value}>{c.label}</option>
    })

    useEffect(() => {
        handleBuscarChamado(Number.parseInt(param.id!)).then(response => {
            if (response.status !== 200) {
                setAlert(true)
                setAlertType('alert alert-danger')
                setMessage("Erro ao carregar o chamado")

                setTimeout(() => {
                    setMessage('')
                    setAlert(false)
                    setAlertType('alert alert-primary')
                }, 2500)
            } else if (response.status === 200) {
                const res = response.data as ChamadoDatails
                setTitulo(res.titulo)
                setDescricao(res.descricao)
                setCategoria(res.categoria)
                setNumeroChamado(res.numeroChamado)
            }
        })
    }, [])

    const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        const usuario = JSON.parse(sessionStorage.getItem("usuario")!) as UserOut;
        const usuarioId = Number(usuario.matricula);

        if (categoria === 'Selecione uma categoria') {
            setAlert(true)
            setAlertType('alert alert-danger')
            setMessage("Selecione uma categoria")

            setTimeout(() => {
                setMessage('')
                setAlert(false)
                setAlertType('alert alert-primary')
            }, 2500)

            return;
        }

        const chamado = { titulo, descricao, categoria: categoria as Categoria, usuarioId };

        handleEditarChamado(numeroChamado, chamado).then(response => {
            if (response.status === 400) {
                setAlert(true)
                setAlertType('alert alert-danger')
                const err = response.data as Error
                setMessage(err.Error)

                setTimeout(() => {
                    setMessage('')
                    setAlert(false)
                    setAlertType('alert alert-primary')
                }, 2500)
            }
            else {
                setAlert(true)
                setAlertType("alert alert-success")
                const chamado = response.data as ChamadoAberto
                setMessage(`Seu chamado de número ${chamado.numeroChamado} foi atualizado`)
                setTitulo('')
                setDescricao('')
                setCategoria('Selecione uma categoria')
                setTimeout(() => {
                    setMessage('')
                    setAlert(false)
                    setAlertType('alert alert-primary')
                    navigate('/chamados')
                }, 2500)
            }
        })
    }

    return (
        <Container>
            <>
                {alert ? <Alert message={message} type={alertType} /> : ''}
                <form onSubmit={handleSubmit} className="w-50 p-5 rounded">
                    <div className="d-grid mb-1">
                        <h1>Editar chamado</h1>
                    </div>
                    <InputForm
                        inputStyle="input"
                        id="titulo"
                        placeholder="Título"
                        type="text"
                        set={(e) => setTitulo(e.target.value)}
                        value={titulo} />
                    <InputForm
                        inputStyle="textarea"
                        id="descricao"
                        placeholder="Descrição"
                        type="text"
                        set={(e) => setDescricao(e.target.value)}
                        value={descricao} />

                    <div className="form-floating">
                        <select value={categoria} onChange={(e) => setCategoria(e.target.value)} className="form-select" id="funcao">
                            <option value="Selecione uma categoria" selected disabled>Selecione uma categoria</option>
                            {Categorias}
                        </select>
                        <label htmlFor="funcao">Selecione uma categoria</label>
                    </div>
                    <div className="d-grid mt-3">
                        <button className="btn btn-dark">Editar chamado</button>
                    </div>
                </form>

            </>
        </Container>
    )

}

export default EditarChamado;