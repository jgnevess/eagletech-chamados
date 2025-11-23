import React from "react";
import { Link, NavLink } from "react-router-dom";
import logo from "../../images/logo.png"
import './index.css';
import { useBootstrapTheme } from "../../hooks/useBootstrapTheme";

interface Props {
    role: string
}

const Navbar = (props: Props) => {

    const { theme, toggleTheme } = useBootstrapTheme();

    const AdminLinks = [
        { nome: 'Dashboard', url: '/admin', iconClass: 'bi bi-pc-display' },
        { nome: 'Cadastrar usuário', url: '/cadastro', iconClass: 'bi bi-person-plus-fill' },
        { nome: 'Gerenciar usuários', url: '/usuarios', iconClass: 'bi bi-people-fill' },
        { nome: 'Todos os chamados', url: '/chamados-all', iconClass: 'bi bi-window-stack' },
        { nome: 'Relatório de chamados', url: '/relatorio', iconClass: 'bi bi-clipboard-data' },
        { nome: 'Meus dados', url: '/about', iconClass: 'bi bi-person-vcard-fill' },
        { nome: 'Sair', url: '/logout', iconClass: 'bi bi-door-closed-fill' }
    ]

    const SolicitanteLinks = [
        { nome: 'Dashboard', url: '/sol', iconClass: 'bi bi-pc-display' },
        { nome: 'Abrir Chamado', url: '/novo-chamado', iconClass: 'bi bi-wrench' },
        { nome: 'Meus chamados', url: '/chamados', iconClass: 'bi bi-window-stack' },
        { nome: 'Meus dados', url: '/about', iconClass: 'bi bi-person-vcard-fill' },
        { nome: 'Sair', url: '/logout', iconClass: 'bi bi-door-closed-fill' }
    ]

    const TecnicoLinks = [
        { nome: 'Dashboard', url: '/tec', iconClass: 'bi bi-pc-display' },
        { nome: 'Chamados abertos', url: '/chamados-abertos', iconClass: 'bi bi-window-stack' },
        { nome: 'Chamados atendidos', url: '/chamados-atendidos', iconClass: 'bi bi-window-stack' },
        { nome: 'Meus dados', url: '/about', iconClass: 'bi bi-person-vcard-fill' },
        { nome: 'Sair', url: '/logout', iconClass: 'bi bi-door-closed-fill' }
    ]

    const Links = props.role === "ADMIN" ? AdminLinks.map((i, k) => {
        return (
            <NavLink
                to={i.url}
                key={k}
                className={({ isActive }) =>
                    `list-group-item w-100 d-flex justify-content-between align-items-center nav-item ${isActive ? "active-link" : ""
                    }`
                }
            >
                <div className="d-flex align-items-center">
                    <span className="nav-icon me-2">
                        <i className={i.iconClass}></i>
                    </span>
                    <span className="nav-text">{i.nome}</span>
                </div>
                <i className="bi bi-caret-right-fill"></i>
            </NavLink>
        )
    }) : props.role === "SOLICITANTE" ? SolicitanteLinks.map((i, k) => {
        return (
            <NavLink
                to={i.url}
                key={k}
                className={({ isActive }) =>
                    `list-group-item w-100 d-flex justify-content-between align-items-center nav-item ${isActive ? "active-link" : ""
                    }`
                }
            >
                <div className="d-flex align-items-center">
                    <span className="nav-icon me-2">
                        <i className={i.iconClass}></i>
                    </span>
                    <span className="nav-text">{i.nome}</span>
                </div>
                <i className="bi bi-caret-right-fill"></i>
            </NavLink>
        )
    }) : props.role === "TECNICO" ? TecnicoLinks.map((i, k) => {
        return (
            <NavLink
                to={i.url}
                key={k}
                className={({ isActive }) =>
                    `list-group-item w-100 d-flex justify-content-between align-items-center nav-item ${isActive ? "active-link" : ""}`}>
                <div className="d-flex align-items-center">
                    <span className="nav-icon me-2">
                        <i className={i.iconClass}></i>
                    </span>
                    <span className="nav-text">{i.nome}</span>
                </div>
                {/* <i className="bi bi-caret-right-fill"></i> */}
            </NavLink>
        )
    }) : '';

    return (
        <nav style={{ height: "101vh", backgroundColor: "#1e233c", overflowY: 'auto' }} className="w-100">
            <div className="d-flex flex-column">
                <Link className="navbar-brand text-light" to={
                    props.role === "ADMIN" ? '/admin' : props.role === "SOLICITANTE" ? '/sol' : '/tec'
                }><img src={logo} className="rounded mt-2" width={100} />
                    <h4>EAGLETECH</h4>
                    <p>SOLUTIONS</p>
                </Link>
                <div className="w-100">
                    <ul className="list-group list-group-flush mt-5 sidebar">
                        <button onClick={toggleTheme} className="list-group-item w-100 d-flex gap-2 align-items-center nav-item">
                            <i className={theme === "light" ? "bi bi-lightbulb" : "bi bi-lightbulb-off"}></i>{theme !== "light" ? "Dark" : "Light"}
                        </button>
                        {Links}
                    </ul>
                </div>
            </div>
        </nav >
    )
}

export default Navbar;