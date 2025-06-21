using Microsoft.AspNetCore.Mvc;
using CinemaReservationApi.Data;
using CinemaReservationApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaReservationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SeatsController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ Pobierz miejsca (opcjonalnie z filteringiem po screeningId)
        [HttpGet]
        public async Task<IActionResult> GetSeats([FromQuery] int? screeningId)
        {
            if (screeningId.HasValue)
            {
                Console.WriteLine($"[GET] /api/Seats?screeningId={screeningId}");

                var seats = await _context.Seats
                    .Where(s => s.ScreeningId == screeningId)
                    .Select(s => new
                    {
                        s.Id,
                        SeatNumber = s.SeatNumber,
                        s.IsReserved
                    })
                    .ToListAsync();

                return Ok(seats);
            }

            return BadRequest("Brakuje parametru screeningId.");
        }



        // ➕ Dodaj jedno miejsce
        [HttpPost]
        public async Task<ActionResult<Seat>> Create(Seat seat)
        {
            _context.Seats.Add(seat);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSeats), new { id = seat.Id }, seat);
        }

        // 🛠️ Zaktualizuj miejsce
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Seat updatedSeat)
        {
            if (id != updatedSeat.Id)
            {
                return BadRequest("ID w URL i w przesłanym obiekcie muszą być zgodne.");
            }

            var seat = await _context.Seats.FindAsync(id);
            if (seat == null)
            {
                return NotFound("Nie znaleziono miejsca o podanym ID.");
            }

            if (seat.IsReserved && seat.SeatNumber != updatedSeat.SeatNumber)
            {
                return BadRequest("Nie można zmienić numeru miejsca, gdy jest zarezerwowane.");
            }

            seat.SeatNumber = updatedSeat.SeatNumber;
            seat.ScreeningId = updatedSeat.ScreeningId;

            await _context.SaveChangesAsync();

            return Ok(seat);
        }

        // ❌ Usuń miejsce
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var seat = await _context.Seats.FindAsync(id);
            if (seat == null)
            {
                return NotFound("Nie znaleziono miejsca o podanym ID.");
            }

            if (seat.IsReserved)
            {
                return BadRequest("Nie można usunąć zarezerwowanego miejsca.");
            }

            _context.Seats.Remove(seat);
            await _context.SaveChangesAsync();

            return Ok($"Miejsce o ID {id} zostało usunięte.");
        }

        // 📦 Dodaj wiele miejsc jednocześnie
        [HttpPost("bulk")]
        public async Task<IActionResult> CreateMany([FromBody] List<Seat> seats)
        {
            if (seats == null || seats.Count == 0)
            {
                return BadRequest("Lista miejsc nie może być pusta.");
            }

            _context.Seats.AddRange(seats);
            await _context.SaveChangesAsync();

            return Ok(seats);
        }
    }
}
