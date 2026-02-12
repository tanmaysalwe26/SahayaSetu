import { useNavigate } from "react-router-dom";
import React, { useState } from "react";
import { useAuth } from "../contexts/AuthContext";
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap-icons/font/bootstrap-icons.css";

export default function NGOLogin() {
  const navigate = useNavigate();
  const { login } = useAuth();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [errors, setErrors] = useState({});
  const [showPassword, setShowPassword] = useState(false);

  const validate = () => {
    const newErrors = {};

    if (!email) {
      newErrors.email = "Email is required";
    } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email)) {
      newErrors.email = "Enter a valid email address";
    }

    if (!password) {
      newErrors.password = "Password is required";
    } else if (password.length < 6) {
      newErrors.password = "Password must be at least 6 characters";
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    if (validate()) {
      // Simulate successful NGO login
      const userData = {
        type: 'ngo',
        email: email,
        name: email.split('@')[0] // Use email prefix as name
      };
      
      login(userData);
      navigate("/ngo-dashboard"); // Redirect to NGO dashboard
    }
  };

  return (
    <div className="d-flex align-items-center justify-content-center vh-100 bg-light" style={{ paddingTop: '80px' }}>
      <div
        className="card shadow-lg border-0"
        style={{ width: "420px", borderRadius: "12px" }}
      >
        <div className="card-body p-4 text-center">
          
          <div className="mb-3">
            <div
              className="rounded-circle bg-primary bg-opacity-10 d-inline-flex align-items-center justify-content-center"
              style={{ width: "70px", height: "70px" }}
            >
              <i className="bi bi-building text-primary fs-2"></i>
            </div>
          </div>

          <h3 className="fw-bold text-primary">NGO Login</h3>
          <p className="text-muted mb-4">
            Login to manage your NGO and donation requests.
          </p>

          <form onSubmit={handleSubmit}>
            
            <div className="mb-3 text-start">
              <label className="form-label">Email Address</label>
              <div className="input-group">
                <span className="input-group-text bg-white">
                  <i className="bi bi-envelope"></i>
                </span>
                <input
                  type="email"
                  className={`form-control ${
                    errors.email ? "is-invalid" : ""
                  }`}
                  placeholder="Enter Email"
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                />
              </div>
              {errors.email && (
                <div className="invalid-feedback d-block">
                  {errors.email}
                </div>
              )}
            </div>

            <div className="mb-4 text-start">
              <label className="form-label">Password</label>
              <div className="input-group">
                <span className="input-group-text bg-white">
                  <i className="bi bi-key"></i>
                </span>
                <input
                  type={showPassword ? "text" : "password"}
                  className={`form-control ${
                    errors.password ? "is-invalid" : ""
                  }`}
                  placeholder="Enter Password"
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                />
                <button
                  type="button"
                  className="btn btn-outline-secondary"
                  onClick={() => setShowPassword(!showPassword)}
                >
                  <i className={`bi ${showPassword ? 'bi-eye-slash' : 'bi-eye'}`}></i>
                </button>
              </div>
              {errors.password && (
                <div className="invalid-feedback d-block">
                  {errors.password}
                </div>
              )}
            </div>

            <button
              type="submit"
              className="btn btn-primary w-100 py-2 fw-semibold"
            >
              <i className="bi bi-box-arrow-in-right me-2"></i>
              Login
            </button>
          </form>

          <p className="mt-4 mb-0 text-muted">
            Don&apos;t have an account?{" "}
            <a href="/register" className="text-primary fw-semibold">
              Register here
            </a>
          </p>

        </div>
      </div>
    </div>
  );
}