import React from "react";
import { Outlet, Link } from "react-router-dom";

import "../index.css";

const LandingPage = ({ children }) => {
  return (
    <div className="landing-page">
      <header className="logo-header">
   
      </header>
      <nav className="nav-container">
        <ul className="nav-list">
          <li>
            <Link to="/products">Forecast</Link>
          </li>
          <li>
            <a href="/login">Login</a>
          </li>
          <li>
            <Link to="/registration">Registration</Link>
          </li>
          <li>
            <Link to="/profile">Profile</Link>
          </li>
        </ul>
      </nav>

      <Outlet />
    </div>
  );
};

export default LandingPage;
