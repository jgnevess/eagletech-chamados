import axios, { AxiosError } from "axios"
import { Error } from "./login/login.models";
import { ResponseList } from "./user/user.models";

const headers = () => {
    return {
        'Authorization': `Bearer ${sessionStorage.getItem('token')}`,
        'Content-Type': 'application/json'
    }
}

export interface Fechamento {
    numeroChamado: number;
    tecnicoId: number;
    justificativaFechamento: string;
}

export interface Usuario {
    matricula: number;
    nomeCompleto: string;
    telefone: string;
    funcao: string;
    email: string;
}

export interface ChamadoDatails {
    numeroChamado: number;
    titulo: string;
    descricao: string;
    status: Status;
    prioridade: Prioridade;
    categoria: Categoria;
    abertura: string;
    fechamento: string;
    solicitante: Usuario;
    tecnico: Usuario | null;
    justificativaFechamento: string | null;
}


export interface Response {
    status: number
    data?: ChamadoAberto | Error | ChamadoDatails[] | ResponseList
}

export interface Chamado {
    titulo: string
    descricao: string
    categoria: Categoria
    usuarioId: number
}

export enum Categoria {
    HARDWARE = "HARDWARE",
    SOFTWARE = "SOFTWARE",
    REDE = "REDE",
    IMPRESSORA = "IMPRESSORA",
    SISTEMA_OPERACIONAL = "SISTEMA_OPERACIONAL",
    BANCO_DE_DADOS = "BANCO_DE_DADOS",
    SEGURANCA = "SEGURANCA",
    OUTROS = "OUTROS",
}

export enum Prioridade {
    CRITICA = "CRITICA",
    ALTA = "ALTA",
    MEDIA = "MEDIA",
    BAIXA = "BAIXA",
}

export enum Status {
    ABERTO = "ABERTO",
    EM_ANDAMENTO = "EM_ANDAMENTO",
    FECHADO = "FECHADO",
}

export interface ChamadoAberto {
    numeroChamado: number
    titulo: string,
    descricao: string,
    status: Status,
    prioridade: Prioridade,
    categoria: Categoria,
    abertura: string,
    fechamento: string,
}

export type StatusChamado = "ABERTO" | "EM_ANDAMENTO" | "FECHADO";

const apiUrl = `${process.env.REACT_APP_API_URL}/api/Chamados`


const handleAbrirChamado = async (chamado: Chamado): Promise<Response> => {
    try {
        const response = await axios.post<ChamadoAberto>(`${apiUrl}/abrir-chamado`, chamado, {
            headers: headers()
        });
        return { status: response.status, data: response.data };
    } catch (err) {
        const error = err as AxiosError
        const data = error.response?.data as Error
        return { status: error.response?.status!, data: data }
    }
}

const handleEditarChamado = async (numeroChamado: number, chamado: Chamado): Promise<Response> => {
    try {
        const response = await axios.put<ChamadoAberto>(`${apiUrl}/editar-chamado`, chamado, {
            headers: headers(),
            params: {
                numeroChamado: numeroChamado
            }
        });
        return { status: response.status, data: response.data };
    } catch (err) {
        const error = err as AxiosError
        const data = error.response?.data as Error
        return { status: error.response?.status!, data: data }
    }
}

const handleChamadosByMatricula = async (matricula: number, status: string, pageSize: number, page: number): Promise<Response> => {
    try {
        const response = await axios.get<ChamadoDatails[]>(`${apiUrl}/chamados-solicitante`, {
            headers: headers(),
            params: {
                solicitante: matricula,
                status: status,
                pageSize: pageSize,
                page: page
            }
        })
        return { status: response.status, data: response.data }
    } catch (err) {
        const error = err as AxiosError
        const data = error.response?.data as Error
        return { status: error.response?.status!, data: data }
    }
}

const handleChamadosByMatriculaTecnico = async (matricula: number, status: string, pageSize: number, page: number): Promise<Response> => {
    try {
        const response = await axios.get<ChamadoDatails[]>(`${apiUrl}/chamados-tecnico`, {
            headers: headers(),
            params: {
                tecnico: matricula,
                status: status,
                pageSize: pageSize,
                page: page
            }
        })
        return { status: response.status, data: response.data }
    } catch (err) {
        const error = err as AxiosError
        const data = error.response?.data as Error
        return { status: error.response?.status!, data: data }
    }
}

const handleChamadosAbertos = async (pageSize: number, page: number): Promise<Response> => {
    try {
        const response = await axios.get<ResponseList>(`${apiUrl}/chamados`, {
            headers: headers(),
            params: {
                status: 'ABERTO',
                pageSize: pageSize,
                page: page
            }
        })
        return { status: response.status, data: response.data }
    } catch (err) {
        const error = err as AxiosError
        const data = error.response?.data as Error
        return { status: error.response?.status!, data: data }
    }
}

const handleChamadosByStatus = async (status: StatusChamado, page: number, pageSize: number): Promise<Response> => {
    try {
        const response = await axios.get<ResponseList>(`${apiUrl}/chamados`, {
            headers: headers(),
            params: {
                status: status,
                pageSize: pageSize,
                page: page
            }
        })
        return { status: response.status, data: response.data }
    } catch (err) {
        const error = err as AxiosError
        const data = error.response?.data as Error
        return { status: error.response?.status!, data: data }
    }
}

const handleBuscarChamado = async (numeroChamado: number): Promise<Response> => {
    try {
        const response = await axios.get<ChamadoDatails>(`${apiUrl}/chamado`, {
            params: {
                numeroChamado: numeroChamado
            },
            headers: headers()
        })
        return { status: 200, data: response.data };
    } catch (err) {
        const error = err as AxiosError
        const data = error.response?.data as Error
        return { status: error.response?.status!, data: data }
    }
}

const handleAceitarChamado = async (numeroChamado: number, tecnicoMatricula: number): Promise<Response> => {
    try {
        const response = await axios.put<ChamadoDatails>(`${apiUrl}/aceitar-tecnico`, {}, {
            params: {
                tecnicoMatricula: tecnicoMatricula,
                numeroChamado: numeroChamado
            },
            headers: headers()
        })
        return { status: 200, data: response.data }
    } catch (err) {
        const error = err as AxiosError
        const data = error.response?.data as Error
        return { status: error.response?.status!, data: data }
    }
}

const handleFinalizarChamado = async (fechamento: Fechamento): Promise<Response> => {
    try {
        const response = await axios.put<ChamadoDatails>(`${apiUrl}/fechar-tecnico`, fechamento, {
            headers: headers()
        })
        return { status: 200, data: response.data }
    } catch (err) {
        const error = err as AxiosError
        const data = error.response?.data as Error
        return { status: error.response?.status!, data: data }
    }
}

const handleCancelarChamado = async (numeroChamado: number): Promise<Response> => {
    try {
        const response = await axios.delete(`${apiUrl}/cancelar-chamado`, {
            params: {
                numeroChamado: numeroChamado
            },
            headers: headers()
        })
        return { status: response.status }
    } catch (err) {
        const error = err as AxiosError
        const data = error.response?.data as Error
        return { status: error.response?.status!, data: data }
    }
}

const handleReabrirChamado = async (numeroChamado: number): Promise<Response> => {
    try {
        const response = await axios.put<ChamadoDatails>(`${apiUrl}/reabir-chamado`, {}, {
            params: {
                numeroChamado: numeroChamado
            },
            headers: headers()
        })
        return { status: 200, data: response.data }
    } catch (err) {
        const error = err as AxiosError
        const data = error.response?.data as Error
        return { status: error.response?.status!, data: data }
    }
}

export {
    handleAbrirChamado,
    handleBuscarChamado,
    handleChamadosByMatricula,
    handleChamadosAbertos,
    handleChamadosByMatriculaTecnico,
    handleAceitarChamado,
    handleFinalizarChamado,
    handleCancelarChamado,
    handleEditarChamado,
    handleChamadosByStatus,
    handleReabrirChamado
}