import React from "react";
import "./App.css";
import TextSection from "./components/TextSection";
import FormSection from "./components/FormSection";
import LoginSection from "./components/LoginSection";
import Navbar from "./components/Navbar";
import HomeSection from "./components/HomeSection";
import { BrowserRouter as Router, Routes, Route, useLocation } from "react-router-dom";

function App() {
  const location = useLocation();

  return (
    <div className="app">
      <Navbar />
      {location.pathname === "/register" || location.pathname === "/login" ? <TextSection /> : null}
      <Routes>
        <Route path="/register" element={<FormSection />} />
        <Route path="/login" element={<LoginSection />} />
        <Route path="/home"  element={<HomeSection />} />
        <Route path="/"  element={<HomeSection />} />
      </Routes>
    </div>
  );
}

export default function WrappedApp() {
  return (
    <Router>
      <App />
    </Router>
  );
}
