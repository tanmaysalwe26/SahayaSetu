import React from "react";
import { FaLinkedin, FaWhatsapp, FaEnvelope } from "react-icons/fa";
import TanmayImg from "../assets/Tanmay.jpg";
import SaloniImg from "../assets/Saloni.jpg";

const teamMembers = [
  {
    name: "Tanmay Salwe",
    role: "Founder & Director",
    image: TanmayImg,
    bio: "Visionary leader with 15+ years in social work."
  },
  {
    name: "Saloni Chavande",
    role: "Head of Education",
    image: SaloniImg,
    bio: "Passionate about rural literacy and child development."
  },
  {
    name: "Dhanashree",
    role: "Operations Manager",
    image: "https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?q=80&w=200",
    bio: "Expert in logistics and village outreach programs."
  },
  {
    name: "Poonam More",
    role: "Operations Manager",
    image: "https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?q=80&w=200",
    bio: "Expert in logistics and village outreach programs."
  },
  {
    name: "Amarnath Malpuri",
    role: "Operations Manager",
    image: "https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?q=80&w=200",
    bio: "Expert in logistics and village outreach programs."
  }
];

export default function AboutUs() {
  return (
    <div className="pt-5 mt-5">
      <div className="container py-5">
        <div className="text-center mb-5">
          <h1 className="fw-bold">
            Our Dedicated <span className="text-success">Team</span>
          </h1>
          <p className="text-muted">The hearts and minds behind SahyaSetu's mission.</p>
        </div>

        <div className="row g-4 justify-content-center">
          {teamMembers.map((member, index) => (
            <div
              className="col-12 col-sm-6 col-md-4 col-lg-4 d-flex align-items-stretch"
              key={index}
            >
              <div
                className="card border-0 shadow-sm text-center p-4 h-100 team-card"
                style={{
                  transition: "transform 0.3s, box-shadow 0.3s",
                  cursor: "pointer"
                }}
              >
                {/* Hover effect */}
                <div className="team-card-inner" style={{ transition: "transform 0.3s" }}>
                  <img
                    src={member.image}
                    alt={member.name}
                    className="rounded-circle mx-auto mb-3 shadow-sm img-fluid"
                    style={{
                      maxWidth: "200px",
                      width: "100%",
                      height: "auto",
                      objectFit: "cover",
                      transition: "transform 0.3s"
                    }}
                  />

                  <h5 className="fw-bold mb-1">{member.name}</h5>
                  <p className="text-success small fw-bold mb-3">{member.role}</p>
                  <p className="text-muted small mb-4">{member.bio}</p>

                  <div className="d-flex justify-content-center gap-3 flex-wrap">
                    <FaLinkedin size={24} className="text-primary" />
                    <FaWhatsapp size={24} className="text-success" />
                    <FaEnvelope size={18} className="text-secondary cursor-pointer" />
                  </div>
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>

      {/* Inline hover CSS */}
      <style>
        {`
          .team-card:hover {
            transform: translateY(-10px);
            box-shadow: 0 1rem 2rem rgba(0,0,0,0.3);
          }

          @media (max-width: 768px) {
            .team-card {
              margin-bottom: 20px;
            }
          }

          @media (max-width: 576px) {
            .team-card img {
              max-width: 150px;
            }
          }
        `}
      </style>
    </div>
  );
}
