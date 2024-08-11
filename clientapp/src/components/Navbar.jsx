import React from 'react';
import { Link } from 'react-router-dom';
import './styles/Navbar.css'

function Navbar() {
  return (
    <nav className="navbar">
      <div className="logo">
        <Link to="/home" className="nav-logo">
          <h1>ChatGram</h1>
        </Link>
      </div>
      <div className="nav-links">
        <Link to="/login" className="nav-link">Login</Link>
        <Link to="/register" className="nav-link">Sign Up</Link>
      </div>
    </nav>
  );
}

export default Navbar;
