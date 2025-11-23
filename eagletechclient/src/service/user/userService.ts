import axios, { AxiosError } from "axios";
import { Error, PasswordUpdate, Response, ResponseList, SimplePasswordUpdate, UserUpdateIn } from "./user.models";
import { UserOut } from "../login/login.models";

const headers = () => {
    return {
        'Authorization': `Bearer ${sessionStorage.getItem('token')}`,
        'Content-Type': 'application/json'
    }
}

const apiUrl = `${process.env.REACT_APP_API_URL}/api/Usuario
`

const handleMudarSenha = async (spu: SimplePasswordUpdate): Promise<Response> => {
    try {
        const res = await axios.put(`${apiUrl}/nova-senha`, spu, {
            headers: headers()
        })
        return { status: 200 };
    } catch (err) {
        const error = err as AxiosError;
        const data = error.response?.data as Error
        return { status: 400, response: data };
    }
}

const handleGetAllUsuarios = async (pageNumber: number, pageSize: number): Promise<Response> => {
    const res = await axios.get<ResponseList>(`${apiUrl}/Usuarios`, {
        params: {
            pageNumber: pageNumber,
            pageSize: pageSize
        },
        headers: headers()
    })
    return { status: res.status, response: res.data }
}

const handleGetAllUsuariosByNome = async (nome: string): Promise<Response> => {
    const res = await axios.get<UserOut[]>(`${apiUrl}/nome`, {
        params: {
            nome: nome
        },
        headers: headers()
    })
    return { status: res.status, response: res.data }
}

const handleGetUsuario = async (matricula: string): Promise<Response> => {
    try {
        const res = await axios.get<UserOut>(`${apiUrl}/matricula/${matricula}`, {
            headers: headers()
        });
        return { status: res.status, response: res.data }
    } catch (err) {
        const error = err as AxiosError;
        const data = error.response?.data as Error
        return { status: 400, response: data };
    }
}


const handleAlterarSenhaUsuario = async (pu: PasswordUpdate): Promise<Response> => {
    try {
        const res = await axios.put(`${apiUrl}/alterar-senha`, pu, {
            headers: headers()
        });
        return { status: res.status }
    } catch (err) {
        const error = err as AxiosError;
        const data = error.response?.data as Error
        return { status: 400, response: data };
    }
}

const handleEditarUsuario = async (matricula: string, Usuario: UserUpdateIn): Promise<Response> => {
    try {
        const res = await axios.put<UserOut>(`${apiUrl}/editar/${matricula}`, Usuario, {
            headers: headers()
        });
        return { status: res.status, response: res.data }
    } catch (err) {
        const error = err as AxiosError;
        const data = error.response?.data as Error
        return { status: 400, response: data };
    }
}

const handleDeleteUser = async (matricula: string): Promise<Response> => {
    try {
        const res = await axios.delete(`${apiUrl}/deletar/${matricula}`, {
            headers: headers()
        })
        return { status: res.status }
    } catch (err) {
        const error = err as AxiosError;
        const data = error.response?.data as Error
        return { status: 400, response: data };
    }
}

const handleActiveUser = async (matricula: string): Promise<Response> => {
    try {
        const res = await axios.put(`${apiUrl}/ativar/${matricula}`, {}, {
            headers: headers()
        })
        return { status: res.status }
    } catch (err) {
        const error = err as AxiosError;
        const data = error.response?.data as Error
        return { status: 400, response: data };
    }
}

const handleResetarSenhaUser = async (matricula: string): Promise<Response> => {
    try {
        const res = await axios.put(`${apiUrl}/resetar-senha/${matricula}`, {}, {
            headers: headers()
        })
        return { status: res.status }
    } catch (err) {
        const error = err as AxiosError;
        const data = error.response?.data as Error
        return { status: 400, response: data };
    }
}

const handleAlterarTema = async (matricula: string, theme: string): Promise<Response> => {
    try {
        const res = await axios.put<UserOut>(`${apiUrl}/alterar-tema/${matricula}`, JSON.stringify(theme), {
            headers: headers()
        });
        return { status: res.status, response: res.data }
    } catch (err) {
        const error = err as AxiosError;
        const data = error.response?.data as Error
        return { status: 400, response: data };
    }
}


export {
    handleMudarSenha,
    handleGetAllUsuarios,
    handleGetUsuario,
    handleGetAllUsuariosByNome,
    handleEditarUsuario,
    handleAlterarSenhaUsuario,
    handleDeleteUser as handleDeleteUserDb,
    handleResetarSenhaUser,
    handleActiveUser,
    handleAlterarTema
}