using CinemaReservationApi.Models;

public class Screening
{
    public int Id { get; set; }

    public int MovieId { get; set; } // To jest klucz obcy do filmu
    public Movie? Movie { get; set; } // NAZWA właściwości musi być TAKA SAMA jak powyżej + "Movie"

    public DateTime ScreeningTime { get; set; }
    // Screening.cs
    public string HallNumber { get; set; } = string.Empty;

}
