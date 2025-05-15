import { loginSuccess } from "./userSlice.js";

// Función que realiza login y gestiona Redux + sessionStorage
export const iniciarSesion = async ({ usuario, contrasena }, dispatch) => {
  const response = await fetch("https://tuapi.com/login", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ usuario, contrasena }),
  });

  if (!response.ok) throw new Error("Credenciales inválidas");

  const data = await response.json(); // asumimos que devuelve { user, token }

  dispatch(loginSuccess(data));
  sessionStorage.setItem("userData", JSON.stringify(data));

  return data;
};
