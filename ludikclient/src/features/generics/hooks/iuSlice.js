// src/features/ui/uiSlice.js
import { createSlice } from "@reduxjs/toolkit";

const normalizeUserId = (id) => (typeof id === "string" ? id.toLowerCase() : id);

// helper para garantizar que byUser siempre exista
const ensureByUser = (state) => {
  if (!state.byUser || typeof state.byUser !== "object") {
    state.byUser = {};
  }
};

const initialState = {
  currentUserId: null,
  theme: "light", // tema actual
  pathChosed: null, // ruta actual
  byUser: {}, // { [userIdLower]: { theme, pathChosed } }
};

const uiSlice = createSlice({
  name: "ui",
  initialState,
  reducers: {
    // Cargar config para un usuario cuando se loguea
    loadUserUiConfig: (state, action) => {
      const rawId = action.payload;
      const userId = normalizeUserId(rawId);

      // si no viene id, no hacemos nada raro
      if (!userId) {
        state.currentUserId = null;
        return;
      }

      ensureByUser(state);

      state.currentUserId = userId;

      const existingConfig = state.byUser[userId];

      if (existingConfig) {
        // Si ya había config guardada para ese user, la aplicamos
        state.theme = existingConfig.theme ?? "light";
        state.pathChosed = existingConfig.pathChosed ?? null;
      } else {
        // Si es la primera vez que se loguea, inicializamos con los valores actuales
        state.byUser[userId] = {
          theme: "light",
          pathChosed: null,
        };
        state.theme = "light";
        state.pathChosed = null;
      }
    },

    setTheme: (state, action) => {
      state.theme = action.payload; // "dark" o "light"

      const userId = state.currentUserId;
      if (!userId) return;

      ensureByUser(state);

      const prev = state.byUser[userId] ?? {};
      state.byUser[userId] = {
        ...prev,
        theme: state.theme,
        // si ya había pathChosed, lo respetamos
        pathChosed: prev.pathChosed ?? state.pathChosed,
      };
    },

    toggleTheme: (state) => {
      state.theme = state.theme === "dark" ? "light" : "dark";

      const userId = state.currentUserId;
      if (!userId) return;

      ensureByUser(state);

      const prev = state.byUser[userId] ?? {};
      state.byUser[userId] = {
        ...prev,
        theme: state.theme,
      };
    },

    setPathChosed: (state, action) => {
      state.pathChosed = action.payload;

      const userId = state.currentUserId;
      if (!userId) return;

      ensureByUser(state);

      const prev = state.byUser[userId] ?? {};
      state.byUser[userId] = {
        ...prev,
        pathChosed: state.pathChosed,
        theme: prev.theme ?? state.theme,
      };
    },

    // Resetea solo el contexto actual, pero mantiene el historial byUser
    resetUiCurrent: (state) => {
      state.currentUserId = null;
      state.theme = "light";
      state.pathChosed = null;
    },

    // Si algún día querés borrar TODO
    resetUiAll: () => initialState,
  },
});

export const { loadUserUiConfig, setTheme, toggleTheme, setPathChosed, resetUiCurrent, resetUiAll } = uiSlice.actions;

// Selectores
export const selectTheme = (state) => state.ui.theme;
export const selectPathChosed = (state) => state.ui.pathChosed;

// También normalizamos y defendemos byUser acá
export const selectUiForUser = (userId) => (state) => {
  const key = normalizeUserId(userId);
  const byUser = state.ui.byUser || {};
  return byUser[key] || null;
};

export default uiSlice.reducer;
