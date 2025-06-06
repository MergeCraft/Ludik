import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  user: null,
  token: null,
  role: null,
  isAuthenticated: false,
};

const userSlice = createSlice({
  name: "userData",
  initialState,
  reducers: {
    loginSuccess: (state, action) => {
      state.user = action.payload.nombreUsuario;
      state.token = action.payload.token;
      state.role = action.payload.rol;
      state.isAuthenticated = true;
    },
    logout: (state) => {
      state.user = null;
      state.token = null;
      state.role = null;
      state.isAuthenticated = false;
    },
  },
});

export const { loginSuccess, logout } = userSlice.actions;

// Selector para obtener el rol del usuario
export const selectUserRole = (state) => state.userData.role;

export default userSlice.reducer;
