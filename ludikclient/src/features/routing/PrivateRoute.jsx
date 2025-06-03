// src/components/PrivateRoute.jsx
import { Navigate, Outlet, useLocation } from "react-router-dom";
import { useSelector } from "react-redux";
import { selectUserRole } from "../auth/hooks/userSlice";

const PrivateRoute = ({ allowedRoles }) => {
  const userRole = useSelector(selectUserRole);
  const location = useLocation();

  if (!userRole) {
    return (
      <Navigate
        to="/login"
        state={{ from: location, message: "Debes iniciar sesión para acceder." }}
        replace
      />
    );
  }

  if (!allowedRoles.includes(userRole)) {
    return (
      <Navigate
        to="/login"
        state={{ from: location, message: "No tienes autorización para acceder a esta sección." }}
        replace
      />
    );
  }

  return <Outlet />;
};

export default PrivateRoute;
