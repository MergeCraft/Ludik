// src/features/auth/hooks/userSlice.js
import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  id: null,
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
      state.id = action.payload.id; // nuevo
      state.user = action.payload.nombreUsuario;
      state.token = action.payload.token;
      state.role = action.payload.rol;
      state.isAuthenticated = true;
    },
    logout: (state) => {
      state.id = null;
      state.user = null;
      state.token = null;
      state.role = null;
      state.isAuthenticated = false;
    },
  },
});

export const { loginSuccess, logout } = userSlice.actions;

// Selectors
export const selectUserId = (state) => state.userData.id;
export const selectUser = (state) => state.userData.user;
export const selectUserRole = (state) => state.userData.role;
export const selectIsAuthenticated = (state) => state.userData.isAuthenticated;

export default userSlice.reducer;
