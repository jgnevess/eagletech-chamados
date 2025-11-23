export interface Response {
  status: number
  response?: LoginResponse | Error | UserOut
}

export type Role = "SOLICITANTE" | "ADMIN" | "TECNICO";

export interface UserOut {
  matricula: number;
  nomeCompleto: string;
  telefone: string;
  funcao: Role;
  username: string;
  ativo: boolean
  theme: string
}

export interface LoginResponse {
  token: string
  role: string
  firstLogin: boolean
  matricula: number
  user: UserOut
}

export interface Error {
  status?: number
  Error: string
}

export interface UserIn {
  nomeCompleto: string;
  senha: string;
  telefone: string;
  funcao: string;
  username: string;
}

export interface LoginCredentials {
  username: string
  password: string
}