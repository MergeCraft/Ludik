import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { Provider } from "react-redux";
import { store } from "./app/store";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import "./lib/fontawesome";
import "./App.css"; // O tu CSS general
import Layout from "./components/Layout";
import Home from "./pages/Home"; // Este tendrá el main y section
import LoginPage from "./pages/LoginPage";
import SignupPage from "./pages/SignupPage";
import StudentGroups from "./pages/StudentGroups.jsx";

function App() {
  return (
    <Provider store={store}>
      <Router>
        <Routes>
          <Route path="/" element={<Layout />}>
            <Route index element={<Home />} />
            <Route path="login" element={<LoginPage />} />
            <Route path="signup" element={<SignupPage />} />
            <Route path="studentGroups" element={<StudentGroups />} />
          </Route>
        </Routes>
      </Router>

      <ToastContainer 
        hideProgressBar={true} 
      />
    </Provider>
  );
}

export default App;
