using System.Text.Json.Serialization;

namespace CinemaReservationApi.Models
{
    public class Seat
    {
        public int Id { get; set; }

        public int ScreeningId { get; set; }

        [JsonIgnore] 
        public Screening? Screening { get; set; }

        public string SeatNumber { get; set; } = string.Empty;

        public bool IsReserved { get; set; } = false;
    }
}
