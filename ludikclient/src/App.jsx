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
import LoginPage from "./features/auth/LoginPage";
import SignupPage from "./features/auth/SignupPage";
import StudentGroups from "./features/student/StudentGroups.jsx";

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
