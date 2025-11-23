import React, { useEffect, useState } from "react";
import Loading from "../../components/loading";
import { Navigate } from "react-router-dom";
import { useBootstrapTheme } from "../../hooks/useBootstrapTheme";



const Logout = () => {

  const [loading, setLoading] = useState(true)
  const { theme, toggleTheme, toggleThemeLight } = useBootstrapTheme();

  useEffect(() => {
    toggleThemeLight()
    if (sessionStorage.length > 0) {
      sessionStorage.clear()
      localStorage.clear()
      setLoading(false)
    }
    else {
      setLoading(false)
    }
  }, [loading])

  return loading ? <Loading /> : <Navigate to="/login" replace />
}

export default Logout