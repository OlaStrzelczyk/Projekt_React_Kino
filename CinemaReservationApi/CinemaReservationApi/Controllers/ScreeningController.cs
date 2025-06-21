using Microsoft.AspNetCore.Mvc;
using CinemaReservationApi.Data;
using CinemaReservationApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaReservationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreeningsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ScreeningsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Screening>>> GetAll([FromQuery] int? movieId)
        {
            try
            {
                var query = _context.Screenings.AsQueryable();

                if (movieId.HasValue)
                {
                    query = query.Where(s => s.MovieId == movieId);
                }

                var result = await query.ToListAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("🔥 Błąd w GetAll: " + ex.Message);
                return StatusCode(500, "Wewnętrzny błąd serwera: " + ex.Message);
            }
        }





        [HttpPost]
        public async Task<ActionResult<Screening>> Create(Screening screening)
        {
            _context.Screenings.Add(screening);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAll), new { id = screening.Id }, screening);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Screening updatedScreening)
        {
            if (id != updatedScreening.Id)
            {
                return BadRequest("ID w URL i w przesłanym obiekcie muszą być zgodne.");
            }

            var screening = await _context.Screenings.FindAsync(id);
            if (screening == null)
            {
                return NotFound("Nie znaleziono seansu o podanym ID.");
            }

            screening.MovieId = updatedScreening.MovieId;
            screening.ScreeningTime = updatedScreening.ScreeningTime;
            screening.HallNumber = updatedScreening.HallNumber;

            await _context.SaveChangesAsync();

            return Ok(screening);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var screening = await _context.Screenings.FindAsync(id);
            if (screening == null)
            {
                return NotFound("Nie znaleziono seansu o podanym ID.");
            }

            _context.Screenings.Remove(screening);
            await _context.SaveChangesAsync();

            return Ok($"Seans o ID {id} został usunięty.");
        }

        [HttpPost("bulk")]
        [ProducesResponseType(typeof(List<Screening>), 200)]
        public async Task<IActionResult> CreateMany([FromBody] List<Screening> screenings)
        {
            if (screenings == null || screenings.Count == 0)
            {
                return BadRequest("Lista seansów nie może być pusta.");
            }

            _context.Screenings.AddRange(screenings);
            await _context.SaveChangesAsync();

            var ids = screenings.Select(s => s.Id).ToList();

            var screeningsWithMovies = await _context.Screenings
                .Include(s => s.Movie)
                .Where(s => ids.Contains(s.Id))
                .ToListAsync();

            return Ok(screeningsWithMovies);
        }

    }
}




