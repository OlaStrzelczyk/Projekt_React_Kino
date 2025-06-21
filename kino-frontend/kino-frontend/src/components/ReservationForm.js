import React, { useReducer } from "react";
import "./ReservationForm.css";
import { useParams, useNavigate } from "react-router-dom";

const initialState = {
  userName: "",
  email: "",
  phoneNumber: "",
};

function formReducer(state, action) {
  return {
    ...state,
    [action.name]: action.value,
  };
}

function ReservationForm() {
  const { screeningId, seatId } = useParams();
  const navigate = useNavigate();

  const [formData, dispatch] = useReducer(formReducer, initialState);

  const handleChange = (e) => {
    dispatch({ name: e.target.name, value: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    const reservation = {
      screeningId: parseInt(screeningId),
      seatId: parseInt(seatId),
      userName: formData.userName,
      email: formData.email,
      phoneNumber: formData.phoneNumber,
    };

    const response = await fetch("/api/Reservations", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(reservation),
    });

    if (response.ok) {
      navigate("/success");
    } else {
      alert("Coś poszło nie tak. Spróbuj ponownie.");
    }
  };

  return (
    <div className="reservation-form-container">
      <h2>📝 Wprowadź dane rezerwacji 📝</h2>
      <form onSubmit={handleSubmit} className="reservation-form">
        <label>
          Imię i nazwisko:
          <input
            type="text"
            name="userName"
            required
            value={formData.userName}
            onChange={handleChange}
          />
        </label>
        <label>
          E-mail:
          <input
            type="email"
            name="email"
            required
            value={formData.email}
            onChange={handleChange}
          />
        </label>
        <label>
          Numer telefonu:
          <input
            type="tel"
            name="phoneNumber"
            required
            value={formData.phoneNumber}
            onChange={handleChange}
          />
        </label>
        <button type="submit">Zarezerwuj</button>
      </form>
    </div>
  );
}

export default ReservationForm;
