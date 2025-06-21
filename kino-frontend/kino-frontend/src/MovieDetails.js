import React, {
  useEffect,
  useState,
  useRef,
  useMemo,
  useCallback,
} from "react";
import { useParams } from "react-router-dom";
import ScreeningTiles from "./components/ScreeningTiles";
import { useForm } from "react-hook-form";
import "./components/ScreeningTiles.css"; 

function MovieDetails() {
  const { id } = useParams();
  const [screenings, setScreenings] = useState([]);
  const [movieTitle, setMovieTitle] = useState("");
  const headingRef = useRef();

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm();

  useEffect(() => {
    headingRef.current?.focus();
  }, []);

  useEffect(() => {
    fetch(`https://localhost:7162/api/Movies/${id}`)
      .then((res) => {
        if (!res.ok) throw new Error(`Błąd HTTP: ${res.status}`);
        return res.json();
      })
      .then((movie) => setMovieTitle(movie.title))
      .catch((err) => console.error("Błąd ładowania tytułu filmu:", err));
  }, [id]);

  useEffect(() => {
    fetch("https://localhost:7162/api/Screenings")
      .then((res) => {
        if (!res.ok) throw new Error(`Błąd HTTP: ${res.status}`);
        return res.json();
      })
      .then((data) => {
        const filtered = data.filter((s) => s.movieId === parseInt(id));
        setScreenings(filtered);
      })
      .catch((err) => console.error("Błąd ładowania seansów:", err));
  }, [id]);

  const memoizedScreenings = useMemo(() => screenings, [screenings]);

  const onSubmit = useCallback(
    (data) => {
      console.log("Form submitted:", data);
      reset();
    },
    [reset]
  );

  return (
    <div>
      <h2 tabIndex={-1} ref={headingRef}>
        🎬 Seanse dla filmu: <strong>{movieTitle || `#${id}`}</strong>
      </h2>

      {memoizedScreenings.length === 0 ? (
        <p>Brak seansów dla tego filmu.</p>
      ) : (
        <ScreeningTiles screenings={memoizedScreenings} />
      )}

      {memoizedScreenings.length > 0 && (
        <div className="report-form-container">
          <h3>Zgłoś problem z rezerwacją</h3>
          <form onSubmit={handleSubmit(onSubmit)}>
            <div>
              <input
                {...register("email", {
                  required: "Email jest wymagany",
                  pattern: {
                    value: /^\S+@\S+$/i,
                    message: "Nieprawidłowy adres email",
                  },
                })}
                placeholder="Twój email"
              />
              {errors.email && (
                <span className="form-error">{errors.email.message}</span>
              )}
            </div>

            <div>
              <input
                {...register("message", {
                  required: "Wiadomość jest wymagana",
                  minLength: {
                    value: 5,
                    message: "Wiadomość musi mieć min. 5 znaków",
                  },
                })}
                placeholder="Wiadomość"
              />
              {errors.message && (
                <span className="form-error">{errors.message.message}</span>
              )}
            </div>

            <button type="submit" className="form-button">📨 Wyślij</button>
          </form>
        </div>
      )}
    </div>
  );
}

export default MovieDetails;
