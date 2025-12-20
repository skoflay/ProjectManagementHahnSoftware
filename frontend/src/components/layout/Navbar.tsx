import { Link, useNavigate } from "react-router-dom";
import { useContext } from "react";
import "../../styles/navbar.css";
import { ThemeContext } from "../../context/ThemeContext";

export const Navbar = () => {
  const navigate = useNavigate();
  const user = getUserFromToken();
  const { theme, toggleTheme } = useContext(ThemeContext); 
  const logout = () => {
    localStorage.removeItem("token");
    navigate("/login");
  };

  return (
    <nav
      className={`navbar navbar-expand-lg ${
        theme === "light" ? "navbar-light bg-light" : "navbar-dark bg-dark"
      } px-4`}
    >
      <Link className="navbar-brand fw-bold" to="/projects">
        EPSILON
      </Link>

      <div className="ms-auto d-flex align-items-center">
        
        <button
          className={`btn btn-sm me-3 ${
            theme === "light" ? "btn-outline-dark" : "btn-outline-light"
          }`}
          onClick={toggleTheme}
        >
          {theme === "light" ? "🌞 Light" : "🌙 Dark"}
        </button>

        {user && (
          <>
            <span className="text-light me-3">
              👤 {user.email}
            </span>

            <Link className="nav-link text-light me-3" to="/projects">
              Projects
            </Link>

            <button className="btn btn-outline-light btn-sm" onClick={logout}>
              Logout
            </button>
          </>
        )}
      </div>
    </nav>
  );
};

const getUserFromToken = () => {
  const token = localStorage.getItem("token");
  if (!token) return null;

  try {
    const payload = JSON.parse(atob(token.split(".")[1]));
    return {
      email:
        payload.email ||
        payload[
          "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"
        ],
      userId: payload.UserId,
    };
  } catch {
    return null;
  }
};
