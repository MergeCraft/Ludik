// src/app/store.js
import { configureStore, combineReducers } from "@reduxjs/toolkit";
import userReducer from "../features/auth/hooks/userSlice";
import uiReducer from "../features/generics/hooks/iuSlice"; // 👈 nuevo slice
import { persistStore, persistReducer, FLUSH, REHYDRATE, PAUSE, PERSIST, PURGE, REGISTER } from "redux-persist";
import storage from "redux-persist/lib/storage"; // usa localStorage

// Combina tus reducers
const rootReducer = combineReducers({
  userData: userReducer,
  ui: uiReducer, // 👈 agregamos el slice de configuración visual
});

// Configuración del persist
const persistConfig = {
  key: "root",
  storage,
  whitelist: ["userData", "ui"], // 👈 persistimos auth + preferencias visuales
};

const persistedReducer = persistReducer(persistConfig, rootReducer);

// Crear store con middleware compatible con redux-persist
export const store = configureStore({
  reducer: persistedReducer,
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware({
      serializableCheck: {
        // Ignora acciones de persist
        ignoredActions: [FLUSH, REHYDRATE, PAUSE, PERSIST, PURGE, REGISTER],
      },
    }),
});

export const persistor = persistStore(store);
