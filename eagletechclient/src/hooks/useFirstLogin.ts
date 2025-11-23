import { useEffect } from "react";
import { handleFirstLogin } from "../service/firstlogin";
import { useNavigate } from "react-router-dom";

export function useFirstLogin() {

    const navigate = useNavigate();

    useEffect(() => {
        
        if(handleFirstLogin()) navigate('/nova-senha');
        
    },[])
}