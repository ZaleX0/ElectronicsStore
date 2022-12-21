import { createContext, useState } from "react";
import { useLocalStorage } from "../hooks/useLocalStorage";
import AuthService from "../services/AuthService";

export const AuthContext = createContext({})

export function AuthProvider( { children } ) {
  const authService = new AuthService()
  const [isLogin, setIsLogin] = useState(false)
  const [user, setUser] = useLocalStorage("user", {})

  async function login(email, password) {
    const response = await authService.login(email, password);
    if (response.status === 200) {
      setUser({ email: email, token: response.text });
      setIsLogin(true);
    }
    return response;
  }

  function logout() {
    if (window.confirm("Do you want to logout?")) {
      localStorage.removeItem("user");
      setIsLogin(false);
    }
  }

  return (
    <AuthContext.Provider value={{
      user,
      isLogin,
      login,
      logout
    }}>
      {children}
    </AuthContext.Provider>
  )
}