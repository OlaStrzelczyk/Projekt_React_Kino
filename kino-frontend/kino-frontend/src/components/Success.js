import React, { useContext } from "react";
import { Link } from "react-router-dom";
import { motion } from "framer-motion";
import { UserContext } from "../store/UserContext";
import withLogger from "./withLogger"; // ⬅️ import HOC
import "./Success.css";

function Success() {
  const { userName } = useContext(UserContext);

  return (
    <motion.div
      className="success-container"
      initial={{ opacity: 0, y: 30 }}
      animate={{ opacity: 1, y: 0 }}
      transition={{ duration: 0.6, ease: "easeOut" }}
    >
      <h2>✨ {userName}, rezerwacja zakończona sukcesem! ✨</h2>
      <p>
        Dziękujemy za dokonanie rezerwacji. Link do płatności został wysłany na podanego maila. Czas rozliczenia wynosi 20min. Po jego upłynięciu rezerwacja zostanie anulowana.
      </p>
      <p>Miłego seansu!</p>
      <Link to="/" className="back-home">Powrót do repertuaru</Link>
    </motion.div>
  );
}

// ⬇️ owijamy komponent HOC-em
export default withLogger(Success);
