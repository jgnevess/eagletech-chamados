import React, { JSX, useEffect, useState } from "react";
import { Navigate } from "react-router-dom";
import NaoAutorizado from "../pages/naoautorizado";
import Loading from "../components/loading";

interface Props {
    children: JSX.Element;
    roles: string[];
}

const PrivateRouter = ({ children, roles }: Props) => {
    const [role, setRole] = useState<string | null>(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const token = sessionStorage.getItem("token");
        const userRole = sessionStorage.getItem("role");

        if (!token || !userRole) {
            setRole(null);
        } else {
            setRole(userRole);
        }
        setLoading(false);
    }, []);

    if (loading) {
        return <Loading />
    }

    if (!role) {
        return <Navigate to="/logout" replace />;
    }

    if (!roles.includes(role)) {
        return <NaoAutorizado />;
    }

    return children;
};

export default PrivateRouter;