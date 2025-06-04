// src/lib/axios.js
import axios from "axios";
import { url } from "../app/url";

const api = axios.create({
  baseURL: url,
  headers: {
    "Content-Type": "application/json",
  },
});

api.interceptors.request.use((config) => {
  const userData = JSON.parse(sessionStorage.getItem("userData"));
  const token = userData?.token || userData?.Token;
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export default api;
