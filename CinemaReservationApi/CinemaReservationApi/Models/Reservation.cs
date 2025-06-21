using CinemaReservationApi.Models;
using System.Text.Json.Serialization;

public class Reservation
{
    public int Id { get; set; }

    public int ScreeningId { get; set; }

    [JsonIgnore]
    public Screening? Screening { get; set; }

    public int SeatId { get; set; }

    [JsonIgnore]
    public Seat? Seat { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public DateTime ReservationTime { get; set; } = DateTime.Now;
}
