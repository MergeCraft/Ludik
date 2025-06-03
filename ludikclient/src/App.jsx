// src/App.jsx
import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { Provider } from "react-redux";
import { store } from "./app/store";
import { ToastContainer } from "react-toastify";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import "react-toastify/dist/ReactToastify.css";
import "./lib/fontawesome";
import "./App.css";

import Layout from "./features/layout/Layout";
import Home from "./features/home/Home.jsx";
import AuthPage from "./features/auth/AuthPage";
import GroupsPage from "./features/group/GroupsPage.jsx";
import PrivateRoute from "./features/routing/PrivateRoute";

// Crear cliente de React Query
const queryClient = new QueryClient();

function App() {
  return (
    <Provider store={store}>
      <QueryClientProvider client={queryClient}>
        <Router>
          <Routes>
            {/* Rutas públicas SIN Layout */}
            <Route path="/login" element={<AuthPage />} />
            <Route path="/signup" element={<AuthPage />} />

            {/* Rutas con Layout */}
            <Route path="/" element={<Layout />}>
              <Route index element={<Home />} />

              {/* Rutas protegidas */}
              <Route element={<PrivateRoute allowedRoles={["Profesor"]} />}>
                
              </Route>

              <Route path="groups" element={<GroupsPage />} />

              {/* Puedes agregar más rutas protegidas aquí */}
            </Route>
          </Routes>
        </Router>

        <ToastContainer hideProgressBar={true} />
      </QueryClientProvider>
    </Provider>
  );
}

export default App;
