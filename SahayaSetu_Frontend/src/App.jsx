import React from "react";
import { Routes, Route } from "react-router-dom";
import { AuthProvider } from "./contexts/AuthContext";
import Navbar from "./components/Navbar.jsx";
import Footer from "./components/Footer.jsx";
import ProtectedRoute from "./components/ProtectedRoute.jsx";
import Home from "./Pages/Home.jsx";
import AboutUs from "./Pages/AboutUs.jsx";
import DonorDashboard from "./Pages/DonorDashboard.jsx";
import NGODashboard from "./components/NGODashboard.jsx";
import LoginSelection from "./Pages/LoginSelection.jsx";
import RegisterSelection from "./Pages/RegisterSelection.jsx";
import DonorLogin from "./Pages/DonorLogin.jsx";
import NGOLogin from "./Pages/NGOLogin.jsx";
import DonorRegistration from "./Pages/DonorRegistration.jsx";
import NGORegistration from "./Pages/Registration.jsx";

function App() {
  return (
    <AuthProvider>
      <div className="bg-white">
        <Navbar />

        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/about" element={<AboutUs />} />
          
          {/* Login Routes */}
          <Route path="/login" element={<LoginSelection />} />
          <Route path="/login/donor" element={<DonorLogin />} />
          <Route path="/login/ngo" element={<NGOLogin />} />
          
          {/* Register Routes */}
          <Route path="/register" element={<RegisterSelection />} />
          <Route path="/register/donor" element={<DonorRegistration />} />
          <Route path="/register/ngo" element={<NGORegistration />} />
          
          {/* Protected Routes */}
          <Route 
            path="/donor-dashboard" 
            element={
              <ProtectedRoute requireDonor={true}>
                <DonorDashboard />
              </ProtectedRoute>
            } 
          />
          <Route 
            path="/ngo-dashboard" 
            element={
              <ProtectedRoute requireNGO={true}>
                <NGODashboard />
              </ProtectedRoute>
            } 
          />
        </Routes>

        <Footer />
      </div>
    </AuthProvider>
  );
}

export default App;
