using CinemaReservationApi.Models;

public class Screening
{
    public int Id { get; set; }

    public int MovieId { get; set; } 
    public Movie? Movie { get; set; } 

    public DateTime ScreeningTime { get; set; }
    
    public string HallNumber { get; set; } = string.Empty;

}
