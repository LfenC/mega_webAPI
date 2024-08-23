using mega_webAPI.Context;
using mega_webAPI.Data.interfaces;
using mega_webAPI.Data.models;
using Microsoft.EntityFrameworkCore;

namespace mega_webAPI.Data.Repositories
{
    public class MovieRepository : IMovie
    {
        private readonly AppDbContext _context;
        public MovieRepository(AppDbContext context)
        {
            _context = context;
        }

        //GET ALL movies api/Movies
        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            return await _context.Movies
             .Include(m => m.Genres)
            .ThenInclude(mg => mg.Genre)
            .Include(m => m.MovieCategories)
            .ThenInclude(mc => mc.Category)
            .Distinct()
            .ToListAsync();
        }


        //GET movies by id api/Movies/id
        public async Task<Movie?> GetMovieById(int id)
        {
            {
                return await _context.Movies
                    .Include(m => m.Genres)
                    .ThenInclude(mg => mg.Genre)
                    .Include(m => m.MovieCategories)
                    .ThenInclude(mc => mc.Category)
                    .FirstOrDefaultAsync(m => m.Id == id);
            }

        }

        public async Task<IEnumerable<Genre>> GetGenresAsync()
        {
            return await _context.Genres.ToListAsync();
        }

 
        public async Task<Movie> AddMovie(Movie movie)
        {
            if (movie.Genres != null && movie.Genres.Any())
            {
                foreach (var movieGenre in movie.Genres)
                {
                    var genreExists = await _context.Genres.AnyAsync(g => g.GenreId == movieGenre.GenreId);
                    if (genreExists)
                    {
                        throw new Exception($"Género con el ID {movieGenre.GenreId} no existe");
                    }

                }
            }
            if (movie.MovieCategories != null && movie.MovieCategories.Any())
            {
                foreach (var movieCategory in movie.MovieCategories)
                {
                    var categoryExists = await _context.Categories.AnyAsync(c => c.CategoryId == movieCategory.CategoryId);
                    if (!categoryExists)
                    {
                        throw new Exception($"Categoría con el ID {movieCategory.CategoryId} no existe");
                    }
                }

            }

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task<Movie> UpdateMovieById(Movie movie)
        {
            _context.Entry(movie).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return await _context.Movies
                .Include(m => m.Genres)
                .ThenInclude(mg => mg.Genre)
                .Include(m => m.MovieCategories)
                .ThenInclude(mc => mc.Category)
                .FirstOrDefaultAsync(m => m.Id == movie.Id);
        }

        public async Task<bool> MovieExists(int id)
        {
            return await _context.Movies.AnyAsync(e => e.Id == id);
        }

        public async Task DeleteMovie(int id)
        {
            var moviesId = await _context.Movies.FindAsync(id);
            if (moviesId != null)
            {
                _context.Movies.Remove(moviesId);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Movie>> GetPopularMoviesAsync()
        {
            var popularCategoryId = await _context.Categories
                .Where(c => c.Name == "Popular")
                .Select(c => c.CategoryId).FirstOrDefaultAsync();

            if (popularCategoryId == 0)
            {
                return Enumerable.Empty<Movie>();
            }

            // to obtain movies in popular category
            var movies = await _context.MovieCategories
            .Where(mc => mc.CategoryId == popularCategoryId)
            .Select(mc => mc.Movie)
            .Distinct()
            .Include(m => m.Genres)
            .ThenInclude(mg => mg.Genre)
            .Include(m => m.MovieCategories)
            .ThenInclude(mc => mc.Category)
            .Distinct()
            .ToListAsync();

            return movies;
        }

        public async Task<IEnumerable<Movie>> GetUpcomingMoviesAsync()
        {
            var upcomingCategoryId = await _context.Categories
                .Where(c => c.Name == "Upcoming")
                .Select(c => c.CategoryId).FirstOrDefaultAsync();

            if (upcomingCategoryId == 0)
            {
                return Enumerable.Empty<Movie>();
            }

            // to obtain movies in popular category
            var movies = await _context.MovieCategories
            .Where(mc => mc.CategoryId == upcomingCategoryId)
            .Select(mc => mc.Movie)
            .Distinct()
            .Include(m => m.Genres)
            .ThenInclude(mg => mg.Genre)
            .Include(m => m.MovieCategories)
            .ThenInclude(mc => mc.Category)
            .Distinct()
            .ToListAsync();

            return movies;
        }

        public async Task<IEnumerable<Movie>> GetTopRatedMoviesAsync()
        {
            var topratedCategoryId = await _context.Categories
                .Where(c => c.Name == "Top_Rated")
                .Select(c => c.CategoryId).FirstOrDefaultAsync();

            if (topratedCategoryId == 0)
            {
                return Enumerable.Empty<Movie>();
            }

            // to obtain movies in popular category
            var movies = await _context.MovieCategories
            .Where(mc => mc.CategoryId == topratedCategoryId)
            .Select(mc => mc.Movie)
            .Distinct()
            .Include(m => m.Genres)
            .ThenInclude(mg => mg.Genre)
            .Include(m => m.MovieCategories)
            .ThenInclude(mc => mc.Category)
            .Distinct()
            .ToListAsync();

            return movies;
        }

        public async Task<IEnumerable<Movie>> GetFeaturedMoviesAsync()
        {
            var featuredCategoryId = await _context.Categories
                .Where(c => c.Name == "Featured")
                .Select(c => c.CategoryId).FirstOrDefaultAsync();

            if (featuredCategoryId == 0)
            {
                return Enumerable.Empty<Movie>();
            }

            // to obtain movies in popular category
            var movies = await _context.MovieCategories
            .Where(mc => mc.CategoryId == featuredCategoryId)
            .Select(mc => mc.Movie)
            .Distinct()
            .Include(m => m.Genres)
            .ThenInclude(mg => mg.Genre)
            .Include(m => m.MovieCategories)
            .ThenInclude(mc => mc.Category)
            .Distinct()
            .ToListAsync();

            return movies;
        }

        public async Task<string> GetMovieVideos(int id)
        {
            var movie = await _context.Movies
                .Where(m => m.Id == id)
                .Select(m => m.VideoUrl)
                .FirstOrDefaultAsync();

            if (movie == null)
            {
                return null;
            }
            return movie;
        }

    }
}
