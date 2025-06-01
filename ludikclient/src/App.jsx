import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { Provider } from "react-redux";
import { store } from "./app/store";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import "./lib/fontawesome";
import "./App.css"; // O tu CSS general
import Layout from "./features/layout/Layout";
import Home from "./features/home/Home.jsx"; // Este tendrá el main y section
import AuthPage from "./features/auth/AuthPage";

import StudentGroups from "./features/student/StudentGroups.jsx";

function App() {
  return (
    <Provider store={store}>
      <Router>
        <Routes>
          {/* Rutas públicas SIN Layout */}
          <Route path="/login" element={<AuthPage />} />
          <Route path="/signup" element={<AuthPage />} />

          {/* Rutas protegidas o con Layout */}
          <Route path="/" element={<Layout />}>
            <Route index element={<Home />} />
            <Route path="studentGroups" element={<StudentGroups />} />
            {/* Más rutas dentro del layout acá */}
          </Route>
        </Routes>
      </Router>

      <ToastContainer hideProgressBar={true} />
    </Provider>
  );
}

export default App;
