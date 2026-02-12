import { useState } from 'react';
import { useAuth } from '../contexts/AuthContext';
import NGODonationRequests from './NGODonationRequests';
import NGOProfileEdit from './NGOProfileEdit';
import './NGODashboard.css';

export default function NGODashboard() {
  const { user } = useAuth();
  const [activeTab, setActiveTab] = useState('dashboard');

  return (
    <div className="ngo-dashboard">
      <div className="container-fluid">
        <div className="dashboard-header">
          <div className="text-center py-5">
            <h1 className="display-4">NGO Dashboard</h1>
            <p className="lead">Welcome, {user?.name || 'NGO'}! Manage your profile and donation activities here.</p>
          </div>
        </div>

        {/* Tab Navigation */}
        <div className="tab-navigation">
          <button
            className={`tab-btn ${activeTab === 'dashboard' ? 'active' : ''}`}
            onClick={() => setActiveTab('dashboard')}
          >
            Donation Requests
          </button>
          <button
            className={`tab-btn ${activeTab === 'profile' ? 'active' : ''}`}
            onClick={() => setActiveTab('profile')}
          >
            Edit Profile
          </button>
        </div>

        {/* Content Area */}
        <div className="content-area">
          {activeTab === 'dashboard' && (
            <div>
              {/* Feature Cards */}
              <div className="row mb-5">
                <div className="col-md-6 col-lg-3 mb-4">
                  <div className="feature-card emergency-card">
                    <div className="feature-icon">🚨</div>
                    <h5>Emergency Funding</h5>
                    <p>Request urgent financial support for critical needs</p>
                  </div>
                </div>
                <div className="col-md-6 col-lg-3 mb-4">
                  <div className="feature-card campaign-card">
                    <div className="feature-icon">📢</div>
                    <h5>Campaigns</h5>
                    <p>Launch fundraising campaigns for your initiatives</p>
                  </div>
                </div>
                <div className="col-md-6 col-lg-3 mb-4">
                  <div className="feature-card material-card">
                    <div className="feature-icon">📦</div>
                    <h5>Material Requests</h5>
                    <p>Request medicines, equipment, and other supplies</p>
                  </div>
                </div>
                <div className="col-md-6 col-lg-3 mb-4">
                  <div className="feature-card volunteer-card">
                    <div className="feature-icon">👥</div>
                    <h5>Volunteers</h5>
                    <p>Find and manage volunteer support</p>
                  </div>
                </div>
              </div>

              {/* Medicine Donation Requests Section */}
              <NGODonationRequests ngoName={user?.name || 'NGO'} />
            </div>
          )}

          {activeTab === 'profile' && (
            <NGOProfileEdit ngoData={user} />
          )}
        </div>
      </div>
    </div>
  );
}