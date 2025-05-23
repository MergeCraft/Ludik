import { loginSuccess } from "./userSlice.js";
import { url } from "../../app/url.js";

// Función que realiza login y gestiona Redux + sessionStorage
export const iniciarSesion = async ({ usuario, contrasena }, dispatch) => {
  const response = await fetch(`${url}/api/Usuario/login`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ NombreUsuario: usuario, Contrasenia: contrasena }),
  });

  if (!response.ok) {
    const errorMessage = await response.text(); // o response.json() si sabes que la API devuelve JSON
    console.log(errorMessage);
    throw new Error(errorMessage);
  }

  console.log(response);

  const data = await response.json(); // asumimos que devuelve { Token, Rol, Email, UsuarioId }

  dispatch(loginSuccess(data));
  sessionStorage.setItem("userData", JSON.stringify(data));

  return data;
};

export const cerrarSesion = (dispatch) => {
  sessionStorage.removeItem("userData");
};
