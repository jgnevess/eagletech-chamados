import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";


const Redirect = () => {
  const [role, setRole] = useState('');
  const navigate = useNavigate();
  useEffect(() => {
    setRole(sessionStorage.getItem("role")!)

    switch (role) {
      case 'ADMIN':
        navigate("/admin")
        break;
      case 'TECNICO':
        navigate("/tec")
        break;
      case 'SOLICITANTE':
        navigate("/sol")
        break;
      default:
        navigate("/logout")
        break;
    }
  })

  return (
    <></>
  )
}


export default Redirect;