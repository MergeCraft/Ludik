// src/App.jsx
import React, { useEffect } from "react";
import { BrowserRouter as Router, Routes, Route, Navigate } from "react-router-dom";

import { Provider } from "react-redux";
import { useSelector } from "react-redux";
import { store, persistor } from "./app/store";
import { PersistGate } from "redux-persist/integration/react";
import { ToastContainer } from "react-toastify";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import "react-toastify/dist/ReactToastify.css";
import "./lib/fontawesome";
import "./App.css";

import Layout from "./features/layout/Layout";
import AuthPage from "./features/auth/AuthPage";
import GroupsPage from "./features/group/GroupsPage.jsx";
import GroupPage from "./features/group/GroupPage.jsx";
import ProfilePage from "./features/profile/ProfilePage.jsx";
import MedalManagerPage from "./features/medals/MedalManagerPage.jsx";
import EquivalenceTablePage from "./features/equivalence-table/EquivalenceTablePage.jsx";
import RewardsPage from "./features/rewards/RewardsPage.jsx";

import PrivateRoute from "./features/routing/PrivateRoute";
import NotFoundPage from "./features/routing/NotFoundPage";

import { selectTheme } from "./features/generics/hooks/iuSlice.js";

// Crear cliente de React Query
const queryClient = new QueryClient();

// 👇 Componente que aplica el tema, ya DENTRO del Provider
const ThemeApplier = ({ children }) => {
  const theme = useSelector(selectTheme);

  useEffect(() => {
    if (!theme) return;

    document.body.classList.remove("light", "dark");
    document.body.classList.add(theme);

    return () => {
      document.body.classList.remove(theme);
    };
  }, [theme]);

  return children;
};

function App() {
  return (
    <Provider store={store}>
      <PersistGate loading={null} persistor={persistor}>
        <QueryClientProvider client={queryClient}>
          <ThemeApplier>
            <Router>
              <Routes>
                {/* Rutas públicas SIN Layout */}
                <Route index element={<Navigate to="/login" replace />} />
                <Route path="/login" element={<AuthPage />} />
                <Route path="/signup" element={<AuthPage />} />
                <Route path="/passwordRecovery" element={<AuthPage />} />

                {/* Rutas con Layout */}
                <Route path="/" element={<Layout />}>
                  {/* Rutas protegidas */}
                  <Route element={<PrivateRoute allowedRoles={["Profesor"]} />}>
                    <Route path="medals" element={<MedalManagerPage />} />
                    <Route path="equivalenceTable" element={<EquivalenceTablePage />} />
                  </Route>

                  <Route element={<PrivateRoute allowedRoles={["Profesor", "Estudiante"]} />}>
                    <Route path="profile" element={<ProfilePage />} />
                    <Route path="groups" element={<GroupsPage />} />
                    <Route path="grupo/:id" element={<GroupPage />} />
                    <Route path="rewards" element={<RewardsPage />} />
                  </Route>

                  {/* Ruta catch-all para páginas no encontradas */}
                  <Route path="*" element={<NotFoundPage />} />
                </Route>
              </Routes>
            </Router>

            <ToastContainer hideProgressBar={true} autoClose={3000} closeOnClick pauseOnHover />
          </ThemeApplier>
        </QueryClientProvider>
      </PersistGate>
    </Provider>
  );
}

export default App;
