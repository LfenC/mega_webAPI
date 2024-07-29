using mega_webAPI.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using mega_webAPI.models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace mega_webAPI.Controllers

{
    [ApiController]
    [Route("api/[Controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public MoviesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            try
            {
                var movies = await _context.Movies.ToListAsync();
                return Ok(movies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
        //GET popular movies
        [HttpGet("popular")]

        public async Task<ActionResult<IEnumerable<Movie>>> GetPopularMovies()
        {
            try
            {
                var popularCategoryId = await _context.Categories
                    .Where(c => c.Name == "Popular")
                    .Select(c => c.CategoryId).FirstOrDefaultAsync();

                if (popularCategoryId == 0)
                {
                    return NotFound("Categoría Popular no encontrada");
                }
                // to obtain movies in popular category
                var movies = await _context.MovieCategories
                .Where(mc => mc.CategoryId == popularCategoryId)
                .Select(mc => mc.Movie).Distinct().ToListAsync();

                if (movies == null || !movies.Any())
                {
                    return NotFound("No se encontraron películas populares");
                }

                return Ok(movies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ObjectResult( new{ error = ex.Message }));
            }
        }

        //GET upcoming movies
        [HttpGet("upcoming")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetUpcomingMovies()
        {
            try
            {
                var UpcomingCategoryId = await _context.Categories
                    .Where(c => c.Name == "Upcoming")
                    .Select(c => c.CategoryId).FirstOrDefaultAsync();

                if (UpcomingCategoryId == 0)
                {
                    return NotFound("Categoría Upcoming no encontrada");
                }
                // to obtain movies in popular category
                var movies = await _context.MovieCategories
                .Where(mc => mc.CategoryId == UpcomingCategoryId)
                .Select(mc => mc.Movie).Distinct().ToListAsync();

                if (movies == null || !movies.Any())
                {
                    return NotFound("No se encontraron películas upcoming");
                }

                return Ok(movies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ObjectResult(new { error = ex.Message }));
            }
        }
    }
}