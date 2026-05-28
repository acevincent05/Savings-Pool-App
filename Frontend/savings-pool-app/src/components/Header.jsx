import React from 'react';
import { FaUserCircle } from "react-icons/fa";
import '../CSS/Header.css';

export default function Header() {
  return (
    <div className="header">
      <div className="header-content">
        <h1 className="header-title">Savings Pool</h1>
        <nav className="nav-links">
          <a href="#profile" className="profile-link">
            <FaUserCircle className="profile-icon" />
          </a>
        </nav>
      </div>
    </div>
  )
}
