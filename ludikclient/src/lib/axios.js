// src/lib/axios.js
import axios from "axios";
import { url } from "../app/url";

const api = axios.create({
  baseURL: url,
  headers: {
    "Content-Type": "application/json",
  },
});

export default api;
