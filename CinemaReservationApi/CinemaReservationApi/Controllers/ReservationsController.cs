using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CinemaReservationApi.Data;
using CinemaReservationApi.Models;

namespace CinemaReservationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReservationsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Reservation reservation)
        {
            var seat = await _context.Seats.FindAsync(reservation.SeatId);
            if (seat == null || seat.IsReserved)
                return BadRequest("Miejsce zajęte lub nie istnieje.");

            seat.IsReserved = true;
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return Ok(reservation);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetAll()
        {
            var reservations = await _context.Reservations
                .Include(r => r.Seat)
                .Include(r => r.Screening)
                .ThenInclude(s => s.Movie)
                .ToListAsync();

            return Ok(reservations);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Reservation updatedReservation)
        {
            if (id != updatedReservation.Id)
            {
                return BadRequest("ID w URL i w danych nie są zgodne.");
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound("Nie znaleziono rezerwacji do edycji.");
            }

            // Jeśli zmieniono miejsce, sprawdź czy nowe miejsce istnieje i nie jest zajęte
            if (reservation.SeatId != updatedReservation.SeatId)
            {
                var newSeat = await _context.Seats.FindAsync(updatedReservation.SeatId);
                if (newSeat == null || newSeat.IsReserved)
                {
                    return BadRequest("Nowe miejsce nie istnieje lub jest już zajęte.");
                }

                // Zwolnij poprzednie miejsce
                var oldSeat = await _context.Seats.FindAsync(reservation.SeatId);
                if (oldSeat != null)
                    oldSeat.IsReserved = false;

                // Zajmij nowe miejsce
                newSeat.IsReserved = true;

                reservation.SeatId = updatedReservation.SeatId;
            }

            reservation.UserName = updatedReservation.UserName;
            reservation.Email = updatedReservation.Email;
            reservation.PhoneNumber = updatedReservation.PhoneNumber;
            reservation.ReservationTime = updatedReservation.ReservationTime;
            reservation.ScreeningId = updatedReservation.ScreeningId;


            await _context.SaveChangesAsync();

            return Ok(reservation);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Cancel(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null) return NotFound();

            var seat = await _context.Seats.FindAsync(reservation.SeatId);
            if (seat != null) seat.IsReserved = false;

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return Ok("Rezerwacja anulowana.");
        }
    }
}
