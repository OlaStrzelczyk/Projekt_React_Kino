import React, { useState, useEffect } from "react";
import "./SeatRoom.css";
import { useParams, useNavigate } from "react-router-dom";

function SeatRoom() {
  const { screeningId } = useParams();
  const navigate = useNavigate();
  const [seats, setSeats] = useState([]);
  const [selectedSeat, setSelectedSeat] = useState(null);

  useEffect(() => {
    fetch(`https://localhost:7162/api/Seats?screeningId=${screeningId}`)
      .then((res) => {
        if (!res.ok) throw new Error(`Server error: ${res.status}`);
        return res.json();
      })
      .then((data) => setSeats(data))
      .catch((error) => {
        console.error("Błąd pobierania miejsc:", error);
      });
  }, [screeningId]);

  const handleSeatClick = (seat) => {
    if (seat.isReserved) return;
    setSelectedSeat(seat);
    navigate(`/screenings/${screeningId}/seats/${seat.id}/form`);
  };

  return (
    <div className="seat-room-container">
      <h2>🎟️ Wybierz miejsce dla seansu #{screeningId} 🎟️</h2>
      <div className="screen">EKRAN</div>
      <div className="seat-grid">
        {[...seats]
          .sort((a, b) => Number(a.seatNumber) - Number(b.seatNumber)) // ✅ poprawne sortowanie numeryczne
          .map((seat) => (
            <button
              key={seat.id}
              className={`seat ${seat.isReserved ? "reserved" : ""}`}
              onClick={() => handleSeatClick(seat)}
              disabled={seat.isReserved}
            >
              {seat.seatNumber}
            </button>
          ))}
      </div>
    </div>
  );
}

export default SeatRoom;
