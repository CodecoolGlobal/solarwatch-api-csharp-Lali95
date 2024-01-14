import React from "react";
import { Outlet, Link, useNavigate } from "react-router-dom";
import "../index.css";

const LandingPage = ({ children }) => {
  const [searchTerm, setSearchTerm] = React.useState("");
  const navigate = useNavigate();

  const handleSearchChange = (e) => {
    const term = e.target.value;
    setSearchTerm(term);
  };

  const handleSearchSubmit = (e) => {
    e.preventDefault();
   
    navigate(`/search/${searchTerm}`);
  };

  return (
    <div className="landing-page">
      <header className="logo-header">
        {/* Add your logo component or image here */}
      </header>
      <nav className="nav-container">
        <ul className="nav-list">
          <li>
            <Link to="/products">Products</Link>
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
      <div className="search-bar-container">
        <form onSubmit={handleSearchSubmit}>
          <div className="search-bar">
            <input
              type="text"
              placeholder="Search cities..."
              value={searchTerm}
              onChange={handleSearchChange}
              style={{ width: "400px" }}
            />
            <button type="submit">Search</button>
          </div>
        </form>
      </div>
      <Outlet />
    </div>
  );
};

export default LandingPage;
