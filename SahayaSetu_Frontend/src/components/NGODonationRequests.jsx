import { useState } from 'react';

export default function NGODonationRequests({ ngoName }) {
  const [requests, setRequests] = useState([]);
  const [showForm, setShowForm] = useState(false);
  const [formData, setFormData] = useState({
    type: 'Medicine',
    itemName: '',
    description: '',
    quantity: '',
    unit: 'Boxes',
    priority: 'Normal',
    deadline: ''
  });

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: value
    }));
  };

  const handleSubmitRequest = (e) => {
    e.preventDefault();
    const newRequest = {
      id: Date.now(),
      ...formData,
      status: 'Pending',
      createdDate: new Date().toLocaleDateString()
    };
    setRequests([newRequest, ...requests]);
    setFormData({
      type: 'Medicine',
      itemName: '',
      description: '',
      quantity: '',
      unit: 'Boxes',
      priority: 'Normal',
      deadline: ''
    });
    setShowForm(false);
  };

  const deleteRequest = (id) => {
    setRequests(requests.filter(req => req.id !== id));
  };

  return (
    <div className="donation-section">
      <div className="section-header">
        <div>
          <h2>💊 Medicine & Material Donation Requests</h2>
          <p className="text-muted">Manage your NGO's specific medicine and material needs here.</p>
        </div>
        <button
          className="btn btn-success"
          onClick={() => setShowForm(!showForm)}
        >
          + Create New Request
        </button>
      </div>

      {/* Request Form */}
      {showForm && (
        <div className="request-form-container">
          <h4>Create New Request</h4>
          <form onSubmit={handleSubmitRequest}>
            <div className="row">
              <div className="col-md-6 mb-3">
                <label className="form-label">Request Type</label>
                <select
                  className="form-select"
                  name="type"
                  value={formData.type}
                  onChange={handleInputChange}
                >
                  <option>Medicine</option>
                  <option>Equipment</option>
                  <option>Food</option>
                  <option>Other</option>
                </select>
              </div>

              <div className="col-md-6 mb-3">
                <label className="form-label">Item Name</label>
                <input
                  type="text"
                  className="form-control"
                  name="itemName"
                  value={formData.itemName}
                  onChange={handleInputChange}
                  placeholder="e.g., Antibiotics, Wheelchairs"
                  required
                />
              </div>

              <div className="col-md-6 mb-3">
                <label className="form-label">Quantity</label>
                <input
                  type="number"
                  className="form-control"
                  name="quantity"
                  value={formData.quantity}
                  onChange={handleInputChange}
                  placeholder="Enter quantity"
                  required
                />
              </div>

              <div className="col-md-6 mb-3">
                <label className="form-label">Unit</label>
                <select
                  className="form-select"
                  name="unit"
                  value={formData.unit}
                  onChange={handleInputChange}
                >
                  <option>Boxes</option>
                  <option>Units</option>
                  <option>Bottles</option>
                  <option>Packets</option>
                  <option>kg</option>
                </select>
              </div>

              <div className="col-md-6 mb-3">
                <label className="form-label">Priority</label>
                <select
                  className="form-select"
                  name="priority"
                  value={formData.priority}
                  onChange={handleInputChange}
                >
                  <option>Normal</option>
                  <option>High</option>
                  <option>Urgent</option>
                </select>
              </div>

              <div className="col-md-6 mb-3">
                <label className="form-label">Deadline</label>
                <input
                  type="date"
                  className="form-control"
                  name="deadline"
                  value={formData.deadline}
                  onChange={handleInputChange}
                />
              </div>

              <div className="col-12 mb-3">
                <label className="form-label">Description</label>
                <textarea
                  className="form-control"
                  name="description"
                  value={formData.description}
                  onChange={handleInputChange}
                  placeholder="Describe your request in detail..."
                  rows="3"
                />
              </div>
            </div>

            <div className="form-actions">
              <button type="submit" className="btn btn-primary">
                Submit Request
              </button>
              <button
                type="button"
                className="btn btn-secondary"
                onClick={() => setShowForm(false)}
              >
                Cancel
              </button>
            </div>
          </form>
        </div>
      )}

      {/* Requests List */}
      {requests.length === 0 ? (
        <div className="empty-state">
          <div className="empty-icon">⚠️</div>
          <h5>No Donation Requests Yet</h5>
          <p>Click "Create New Request" to list medicines your NGO currently needs.</p>
        </div>
      ) : (
        <div className="requests-grid">
          {requests.map(request => (
            <div key={request.id} className="request-card">
              <div className="card-header">
                <div className="type-badge" style={{
                  backgroundColor: request.type === 'Medicine' ? '#e8f5e9' : 
                                  request.type === 'Equipment' ? '#e3f2fd' : '#fff3e0'
                }}>
                  {request.type === 'Medicine' && '💊'}
                  {request.type === 'Equipment' && '⚙️'}
                  {request.type === 'Food' && '🍎'}
                  {request.type === 'Other' && '📦'}
                </div>
                <span className={`badge bg-${request.priority === 'Urgent' ? 'danger' : request.priority === 'High' ? 'warning' : 'info'}`}>
                  {request.priority}
                </span>
              </div>
              <h5>{request.itemName}</h5>
              <p className="request-description">{request.description}</p>
              <div className="request-details">
                <div className="detail-item">
                  <small className="label">Quantity</small>
                  <strong>{request.quantity} {request.unit}</strong>
                </div>
                <div className="detail-item">
                  <small className="label">Status</small>
                  <span className="status-badge">{request.status}</span>
                </div>
                {request.deadline && (
                  <div className="detail-item">
                    <small className="label">Deadline</small>
                    <strong>{request.deadline}</strong>
                  </div>
                )}
              </div>
              <small className="text-muted">Created: {request.createdDate}</small>
              <button
                className="btn btn-sm btn-danger mt-2"
                onClick={() => deleteRequest(request.id)}
              >
                Delete
              </button>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}