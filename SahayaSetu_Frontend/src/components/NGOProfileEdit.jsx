import { useState } from 'react';

export default function NGOProfileEdit({ ngoData }) {
  const [formData, setFormData] = useState(ngoData || {
    name: '',
    email: '',
    ngoType: 'Community Health',
    fullAddress: '',
    phone: '',
    website: '',
    description: ''
  });
  const [edited, setEdited] = useState(false);
  const [saveSuccess, setSaveSuccess] = useState(false);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: value
    }));
    setEdited(true);
  };

  const handleSave = (e) => {
    e.preventDefault();
    setSaveSuccess(true);
    setEdited(false);
    setTimeout(() => setSaveSuccess(false), 3000);
  };

  return (
    <div className="profile-section">
      <div className="row">
        <div className="col-lg-8">
          <div className="profile-card">
            <h2>Edit Your NGO Profile</h2>
            <p className="text-muted">Keep your information up-to-date for donors.</p>

            {saveSuccess && (
              <div className="alert alert-success alert-dismissible fade show">
                ✓ Profile updated successfully!
              </div>
            )}

            <form onSubmit={handleSave}>
              <div className="mb-4">
                <label className="form-label">NGO Name</label>
                <input
                  type="text"
                  className="form-control form-control-lg"
                  name="name"
                  value={formData.name}
                  onChange={handleChange}
                />
              </div>

              <div className="mb-4">
                <label className="form-label">Type of NGO</label>
                <select
                  className="form-select form-select-lg"
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

              <div className="mb-4">
                <label className="form-label">Full Address</label>
                <textarea
                  className="form-control"
                  name="fullAddress"
                  value={formData.fullAddress}
                  onChange={handleChange}
                  rows="3"
                />
              </div>

              <div className="row">
                <div className="col-md-6 mb-4">
                  <label className="form-label">Email Address</label>
                  <input
                    type="email"
                    className="form-control"
                    name="email"
                    value={formData.email}
                    onChange={handleChange}
                  />
                </div>

                <div className="col-md-6 mb-4">
                  <label className="form-label">Phone Number</label>
                  <input
                    type="tel"
                    className="form-control"
                    name="phone"
                    value={formData.phone}
                    onChange={handleChange}
                  />
                </div>
              </div>

              <div className="mb-4">
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

              <div className="mb-4">
                <label className="form-label">Organization Description</label>
                <textarea
                  className="form-control"
                  name="description"
                  value={formData.description}
                  onChange={handleChange}
                  rows="4"
                />
              </div>

              <div className="form-actions">
                <button
                  type="submit"
                  className="btn btn-primary btn-lg"
                  disabled={!edited}
                >
                  Save Changes
                </button>
              </div>
            </form>
          </div>
        </div>

        <div className="col-lg-4">
          <div className="profile-summary">
            <h5>Profile Summary</h5>
            <div className="summary-item">
              <strong>Name:</strong>
              <span>{formData.name}</span>
            </div>
            <div className="summary-item">
              <strong>Type:</strong>
              <span>{formData.ngoType}</span>
            </div>
            <div className="summary-item">
              <strong>Email:</strong>
              <span>{formData.email}</span>
            </div>
            <div className="summary-item">
              <strong>Phone:</strong>
              <span>{formData.phone}</span>
            </div>
            {formData.website && (
              <div className="summary-item">
                <strong>Website:</strong>
                <a href={formData.website} target="_blank" rel="noopener noreferrer">
                  {formData.website}
                </a>
              </div>
            )}
          </div>
        </div>
      </div>
    </div>
  );
}