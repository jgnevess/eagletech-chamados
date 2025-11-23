import axios, { AxiosError } from "axios";
import { Error, LoginCredentials, LoginResponse, Response, UserIn, UserOut } from "./login.models";

const headers = () => {
    return {
        'Authorization': `Bearer ${sessionStorage.getItem('token')}`,
        'Content-Type': 'application/json'
    }
}

const apiUrl = `${process.env.REACT_APP_API_URL}/api/Auth`

const handleLoginAsync = async (payload: LoginCredentials): Promise<Response> => {
    try {
        const res = await axios.post<LoginResponse>(`${apiUrl}/login`, payload);
        return { status: 200, response: res.data };
    } catch (err) {
        const error = err as AxiosError;
        const data = error.response?.data as Error
        return { status: 400, response: data }
    }
}

const handleRegister = async (user: UserIn): Promise<Response> => {
    try {
        const res = await axios.post<UserOut>(`${apiUrl}/register`, user, {
            headers: headers()
        });
        return { status: 200, response: res.data };
    } catch (err) {
        const error = err as AxiosError;
        const data = error.response?.data as Error
        return { status: 500, response: data }
    }
}

export { handleLoginAsync, handleRegister }