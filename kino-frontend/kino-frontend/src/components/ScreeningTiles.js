// src/components/ScreeningTiles.js
import React from "react";
import { useNavigate } from "react-router-dom";
import "./ScreeningTiles.css";

function ScreeningTiles({ screenings }) {
  const navigate = useNavigate();

  return (
    <div className="tiles-container">
      {screenings.map((s) => (
        <div
          key={s.id}
          className="tile"
          onClick={() => navigate(`/screenings/${s.id}/seats`)}
        >
          <div className="tile-content">
            <p><span className="emoji">🎟️</span> Sala: <strong>{s.hallNumber}</strong></p>
            <p><span className="emoji">🕒</span> {new Date(s.screeningTime).toLocaleString()}</p>
          </div>
        </div>
      ))}
    </div>
  );
}

export default ScreeningTiles;
