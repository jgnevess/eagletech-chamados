import { useEffect, useState } from "react";
import { handleAlterarTema } from "../service/user/userService";

export function useBootstrapTheme() {
  const [theme, setTheme] = useState(() => {
    const usuarioStr = sessionStorage.getItem("usuario");
    if (usuarioStr) {
      try {
        const usuario = JSON.parse(usuarioStr);
        return usuario.theme || "auto";
      } catch {
        return "auto";
      }
    }
    return "auto";
  });

  useEffect(() => {
    document.documentElement.setAttribute("data-bs-theme", theme);
  }, [theme]);

  const toggleTheme = () => {
    const newTheme = theme === "light" ? "dark" : "light";
    handleAlterarTema(sessionStorage.getItem("matricula")!, newTheme)
      .then(res => {
        const usuarioStr = sessionStorage.getItem("usuario");
        if (usuarioStr) {
          const usuario = JSON.parse(usuarioStr);
          usuario.theme = newTheme;
          sessionStorage.setItem("usuario", JSON.stringify(usuario));
        }
      })
      .catch(err => { });
    setTheme(newTheme);
  };

  const toggleThemeLight = () => {
    setTheme("light");
  };

  return { theme, toggleTheme, toggleThemeLight };
}
