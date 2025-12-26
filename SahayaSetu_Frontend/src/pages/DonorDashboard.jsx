import "./DonorDashboard.css";
import { useAuth } from "../contexts/AuthContext";

function DonorDashboard() {
  const { user } = useAuth();

  return (
    <>
      {/* HEADER */}
      <section className="dashboard-header">
        <h1>Donor Dashboard</h1>
        <p>Welcome, {user?.name || 'Donor'}! Browse active donation and support requests.</p>
      </section>

      {/* CARDS */}
      <section className="cards-container">

        <div className="card">
          <h3>Paracetamol</h3>
          <span className="badge">Medium</span>
          <p><strong>Requested by:</strong> Apple Foundation</p>
          <p>Medicine for fever and pain relief.</p>
          <p className="quantity">Quantity Needed: 50</p>
          <button>Offer Help / Contact NGO</button>
          <div className="posted">Posted 3 days ago</div>
        </div>

        <div className="card">
          <h3>Blankets for Old Age Home</h3>
          <span className="badge">Medium</span>
          <p><strong>Requested by:</strong> Smile Foundation</p>
          <p>Providing warm blankets to senior citizens during winter.</p>
          <p className="quantity">Quantity Needed: 100</p>
          <button>Offer Help / Contact NGO</button>
          <div className="posted">Posted 6 months ago</div>
        </div>

        <div className="card">
          <h3>Dog Food</h3>
          <span className="badge">Medium</span>
          <p><strong>Requested by:</strong> Mai Dog Shelter</p>
          <p>Food for stray dogs in the shelter.</p>
          <p className="quantity">Quantity Needed: 10 bags</p>
          <button>Offer Help / Contact NGO</button>
          <div className="posted">Posted 6 months ago</div>
        </div>

      </section>
    </>
  );
}

export default DonorDashboard;