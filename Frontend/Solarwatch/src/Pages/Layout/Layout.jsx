
import React from "react";
import LandingPage from "../LandingPage.jsx";

const Layout = ({ children }) => {
  return (
    <div className="Layout">
      <LandingPage />
      {children}
    </div>
  );
};

export default Layout;
