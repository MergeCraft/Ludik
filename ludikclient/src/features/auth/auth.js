import { loginSuccess } from "./userSlice.js";

// Función que realiza login y gestiona Redux + sessionStorage
export const iniciarSesion = async ({ usuario, contrasena }, dispatch) => {
  const response = await fetch("https://localhost:7215/api/Usuario/login", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ NombreUsuario: usuario, Contrasenia: contrasena }),
  });

  if (!response.ok) throw new Error("Credenciales inválidas");

  console.log(response.json());
  const data = await response.json(); // asumimos que devuelve { Token, Rol, Email, UsuarioId }
  console.log(data);


  dispatch(loginSuccess(data));
  sessionStorage.setItem("userData", JSON.stringify(data));

  return data;
};
