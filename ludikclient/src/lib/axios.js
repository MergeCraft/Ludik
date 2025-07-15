// src/lib/axios.js
import axios from "axios";
import { url } from "../app/url";
import { store } from "../app/store"; // importás el store directamente

const api = axios.create({
  baseURL: url,
  headers: {
    "Content-Type": "application/json",
  },
});

// Interceptor para agregar el token desde Redux
api.interceptors.request.use((config) => {
  const state = store.getState(); // accedés al estado global
  const token = state.userData.token;

  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }

  return config;
});

export default api;
