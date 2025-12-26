import { useState } from "react";
import { useNavigate } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap-icons/font/bootstrap-icons.css";

export default function NGORegistration() {
  const navigate = useNavigate();
  const [formData, setFormData] = useState({
    ngoName: "",
    ngoType: "Community Health",
    fullAddress: "",
    email: "",
    phone: "",
    website: "",
    description: "",
    password: "",
    confirmPassword: "",
  });
  const [errors, setErrors] = useState({});
  const [showPassword, setShowPassword] = useState(false);
  const [showConfirmPassword, setShowConfirmPassword] = useState(false);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const validate = () => {
    const newErrors = {};

    if (!formData.ngoName) newErrors.ngoName = "NGO name is required";
    if (!formData.email) newErrors.email = "Email is required";
    if (!formData.phone) newErrors.phone = "Phone is required";
    if (!formData.fullAddress) newErrors.fullAddress = "Address is required";
    if (!formData.description) newErrors.description = "Description is required";
    if (!formData.password) newErrors.password = "Password is required";
    if (formData.password !== formData.confirmPassword) {
      newErrors.confirmPassword = "Passwords do not match";
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (validate()) {
      alert("NGO Registration Successful! Please login to continue.");
      navigate("/login/ngo");
    }
  };

  return (
    <div className="d-flex align-items-center justify-content-center min-vh-100 bg-light" style={{ paddingTop: '100px', paddingBottom: '50px' }}>
      <div className="container">
        <div className="row justify-content-center">
          <div className="col-md-8 col-lg-6">
            <div className="card shadow-lg border-0" style={{ borderRadius: "12px" }}>
              <div className="card-body p-4">
                
                <div className="text-center mb-4">
                  <div
                    className="rounded-circle bg-success bg-opacity-10 d-inline-flex align-items-center justify-content-center mb-3"
                    style={{ width: "70px", height: "70px" }}
                  >
                    <i className="bi bi-building text-success fs-2"></i>
                  </div>
                  <h3 className="fw-bold text-success">NGO Registration</h3>
                  <p className="text-muted">
                    Register your NGO to manage donations and campaigns
                  </p>
                </div>

                <form onSubmit={handleSubmit}>
                  <div className="mb-3">
                    <label className="form-label">NGO Name</label>
                    <input
                      type="text"
                      className={`form-control ${errors.ngoName ? 'is-invalid' : ''}`}
                      name="ngoName"
                      value={formData.ngoName}
                      onChange={handleChange}
                      required
                    />
                    {errors.ngoName && <div className="invalid-feedback">{errors.ngoName}</div>}
                  </div>

                  <div className="mb-3">
                    <label className="form-label">Type of NGO</label>
                    <select
                      className="form-select"
                      name="ngoType"
                      value={formData.ngoType}
                      onChange={handleChange}
                    >
                      <option>Community Health</option>
                      <option>Education</option>
                      <option>Environmental</option>
                      <option>Disaster Relief</option>
                      <option>Animal Welfare</option>
                      <option>Other</option>
                    </select>
                  </div>

                  <div className="mb-3">
                    <label className="form-label">Full Address</label>
                    <textarea
                      className={`form-control ${errors.fullAddress ? 'is-invalid' : ''}`}
                      name="fullAddress"
                      value={formData.fullAddress}
                      onChange={handleChange}
                      rows="3"
                      required
                    />
                    {errors.fullAddress && <div className="invalid-feedback">{errors.fullAddress}</div>}
                  </div>

                  <div className="mb-3">
                    <label className="form-label">Email</label>
                    <input
                      type="email"
                      className={`form-control ${errors.email ? 'is-invalid' : ''}`}
                      name="email"
                      value={formData.email}
                      onChange={handleChange}
                      required
                    />
                    {errors.email && <div className="invalid-feedback">{errors.email}</div>}
                  </div>

                  <div className="mb-3">
                    <label className="form-label">Phone</label>
                    <input
                      type="tel"
                      className={`form-control ${errors.phone ? 'is-invalid' : ''}`}
                      name="phone"
                      value={formData.phone}
                      onChange={handleChange}
                      required
                    />
                    {errors.phone && <div className="invalid-feedback">{errors.phone}</div>}
                  </div>

                  <div className="mb-3">
                    <label className="form-label">Website <span className="text-muted">(Optional)</span></label>
                    <input
                      type="url"
                      className="form-control"
                      name="website"
                      value={formData.website}
                      onChange={handleChange}
                      placeholder="https://yourwebsite.com"
                    />
                  </div>

                  <div className="mb-3">
                    <label className="form-label">Description</label>
                    <textarea
                      className={`form-control ${errors.description ? 'is-invalid' : ''}`}
                      name="description"
                      value={formData.description}
                      onChange={handleChange}
                      rows="3"
                      required
                    />
                    {errors.description && <div className="invalid-feedback">{errors.description}</div>}
                  </div>

                  <div className="mb-3">
                    <label className="form-label">Password</label>
                    <div className="input-group">
                      <input
                        type={showPassword ? "text" : "password"}
                        className={`form-control ${errors.password ? 'is-invalid' : ''}`}
                        name="password"
                        value={formData.password}
                        onChange={handleChange}
                        required
                      />
                      <button
                        type="button"
                        className="btn btn-outline-secondary"
                        onClick={() => setShowPassword(!showPassword)}
                      >
                        <i className={`bi ${showPassword ? 'bi-eye-slash' : 'bi-eye'}`}></i>
                      </button>
                    </div>
                    {errors.password && <div className="invalid-feedback d-block">{errors.password}</div>}
                  </div>

                  <div className="mb-4">
                    <label className="form-label">Confirm Password</label>
                    <div className="input-group">
                      <input
                        type={showConfirmPassword ? "text" : "password"}
                        className={`form-control ${errors.confirmPassword ? 'is-invalid' : ''}`}
                        name="confirmPassword"
                        value={formData.confirmPassword}
                        onChange={handleChange}
                        required
                      />
                      <button
                        type="button"
                        className="btn btn-outline-secondary"
                        onClick={() => setShowConfirmPassword(!showConfirmPassword)}
                      >
                        <i className={`bi ${showConfirmPassword ? 'bi-eye-slash' : 'bi-eye'}`}></i>
                      </button>
                    </div>
                    {errors.confirmPassword && <div className="invalid-feedback d-block">{errors.confirmPassword}</div>}
                  </div>

                  <button type="submit" className="btn btn-success w-100 py-2 fw-semibold">
                    <i className="bi bi-check-circle me-2"></i>
                    Register NGO
                  </button>
                </form>

                <p className="mt-4 mb-0 text-muted text-center">
                  Already have an account?{" "}
                  <a href="/login" className="text-success fw-semibold">
                    Login here
                  </a>
                </p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}