# 🎬 Cinema Reservation System

A full-stack web application for managing movie reservations in a cinema. The system is built with a **React frontend** and a **.NET Core Web API backend**, supporting core functionalities such as listing movies, managing screenings, selecting seats, and making reservations.

---

## 📂 Structure
```bash
cinema_project/
├── CinemaReservationApi/                 # 🎬 Backend (.NET Core Web API)
│   ├── Controllers/                      # 📡 API endpoints
│   │   ├── MoviesController.cs           # Handle movie-related requests
│   │   ├── ScreeningsController.cs       # Manage screenings (GET, POST)
│   │   ├── SeatsController.cs            # Seat layout and availability
│   │   └── ReservationsController.cs     # Handle seat reservations
│   ├── Models/                           # 🧱 Entity Framework models
│   │   ├── Movie.cs                      # Movie entity (title, description)
│   │   ├── Screening.cs                  # Screening (date, movie, room)
│   │   ├── Seat.cs                       # Individual seat (row, number)
│   │   └── Reservation.cs                # Reservation (seat, user data)
│   ├── Data/                             # 💾 DbContext configuration
│   ├── Migrations/                       # 🔁 EF Core migrations
│   └── CinemaReservationApi.csproj       # 🔧 Project file
│
├── kino-frontend/                        # 💻 Frontend (React)
│   ├── public/                           # 🌐 HTML shell and static assets
│   └── src/
│       ├── components/                   # 🧩 Reusable UI components
│       │   ├── ScreeningTiles.js         # Grid of available screenings
│       │   ├── SeatRoom.js               # Seat selection for a screening
│       │   ├── ReservationForm.js        # User form for booking
│       │   └── Success.js                # Booking confirmation view
│       ├── store/                        # 🧠 Zustand global state store
│       │   └── useFavorites.js           # Store for favorite movies
│       ├── App.js                        # 🚦 App entry with routing
│       ├── MovieDetails.js               # 🎥 Single movie detail page
│       └── index.js                      # ⚙️ ReactDOM root
│
└── README.md                             # 📘 Project documentation
```

## 🚀 Features

### 🎥 Backend (ASP.NET Core Web API)

- Get a list of available movies and their details.
- Manage screenings and seating arrangements.
- Handle reservations and seat availability.
- Controllers:
  - `MoviesController.cs`
  - `ScreeningController.cs`
  - `SeatController.cs`
  - `ReservationsController.cs`

### 💻 Frontend (React.js)

- Browse movies and their descriptions.
- View available screening times.
- Select and reserve seats.
- Frontend built with React components and Bootstrap styling.

---

## 🛠️ Technologies Used

**Backend**:
- ASP.NET Core 8.0
- Entity Framework Core
- RESTful API
- C#

**Frontend**:
- React.js
- JavaScript (ES6+)
- JSX
- CSS Modules

---

## 🧪 Local Setup

### 1. Clone the repository
```bash
git clone https://github.com/OlaBluszcz/cinema_project.git
```

### 2. Run the Backend
```bash
cd CinemaReservationApi/CinemaReservationApi
dotnet restore
dotnet build
dotnet run
```
### 3. Run the Frontend
```bash
cd kino-frontend/kino-frontend
npm install
npm start
```



## 📌 Notes

Backend uses EF Core for ORM and SQLite/SQL Server for data storage (adjustable in configuration).

## Authors

Created by **Aleksandra Bluszcz** and **Aleksandra Strzelczyk**

 

