using mega_webAPI.Context;
using mega_webAPI.Data.interfaces;
using mega_webAPI.Data.models;
using Microsoft.EntityFrameworkCore;

namespace mega_webAPI.Data.Repositories
{
    public class TvShowRepository : ITvShow
    {
        private readonly AppDbContext _context;
        public TvShowRepository(AppDbContext context)
        {
            _context = context;
        }

        //GET ALL actors api/actors

        public async Task<Tvshow> AddTvShow(Tvshow tvshow)
        {
            if (tvshow.Genres != null && tvshow.Genres.Any())
            {
                foreach (var tvShowGenre in tvshow.Genres)
                {
                    var genreExists = await _context.Genres.AnyAsync(g => g.GenreId == tvShowGenre.GenreId);
                    if (genreExists)
                    {
                        throw new Exception($"Género con el ID {tvShowGenre.GenreId} no existe");
                    }

                }
            }
            if (tvshow.TvShowCategories != null && tvshow.TvShowCategories.Any())
            {
                foreach (var tvShowCategory in tvshow.TvShowCategories)
                {
                    var categoryExists = await _context.Categories.AnyAsync(c => c.CategoryId == tvShowCategory.CategoryId);
                    if (!categoryExists)
                    {
                        throw new Exception($"Categoría con el ID {tvShowCategory.CategoryId} no existe");
                    }
                }

            }
            _context.Tvshows.Add(tvshow);
            await _context.SaveChangesAsync();
            return tvshow;
        }


        public async Task DeleteTvShow(int id)
        {
            var tvshowsId = await _context.Movies.FindAsync(id);
            if (tvshowsId != null)
            {
                _context.Movies.Remove(tvshowsId);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Tvshow>> GetAllTvShows()
        {
            return await _context.Tvshows
             .Include(m => m.Genres)
            .ThenInclude(mg => mg.Genre)
            .Include(m => m.TvShowCategories)
            .ThenInclude(mc => mc.Category)
            .Distinct()
            .ToListAsync();
        }
        public async Task<IEnumerable<Tvshow>> GetPopularTvShowsAsync()
        {
            var popularCategoryId = await _context.Categories
                .Where(c => c.Name == "Popular")
                .Select(c => c.CategoryId).FirstOrDefaultAsync();

            if (popularCategoryId == 0)
            {
                return Enumerable.Empty<Tvshow>();
            }

            // to obtain movies in popular category
            var tvshows = await _context.TvShowCategories
            .Where(tc => tc.CategoryId == popularCategoryId)
            .Select(tc => tc.Tvshow)
            .Distinct()
            .Include(t => t.Genres)
            .ThenInclude(tg => tg.Genre)
            .Include(t => t.TvShowCategories)
            .ThenInclude(tc => tc.Category)
            .Distinct()
            .ToListAsync();

            return tvshows;
        }

        public async Task<IEnumerable<Tvshow>> GetTopRatedTvShowsAsync()
        {
            var topratedCategoryId = await _context.Categories
                .Where(c => c.Name == "Top Rated")
                .Select(c => c.CategoryId).FirstOrDefaultAsync();

            if (topratedCategoryId == 0)
            {
                return Enumerable.Empty<Tvshow>();
            }

            // to obtain tvshows in popular category
            var tvshows = await _context.TvShowCategories
            .Where(mc => mc.CategoryId == topratedCategoryId)
            .Select(mc => mc.Tvshow)
            .Distinct()
            .ToListAsync();

            return tvshows;
        }

        //GET tvshow by id api/Actors/id
        public async Task<Tvshow?> GetTvShowById(int id)
        {
            return await _context.Tvshows
                .Include(t => t.Genres)
                .ThenInclude(tg => tg.Genre)
                .Include(t => t.TvShowCategories)
                .ThenInclude(tc => tc.Category)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Tvshow>> GetUpcomingTvShowsAsync()
        {
            var upcomingCategoryId = await _context.Categories
                .Where(c => c.Name == "Upcoming")
                .Select(c => c.CategoryId).FirstOrDefaultAsync();

            if (upcomingCategoryId == 0)
                {
                    return Enumerable.Empty<Tvshow>();
                }

            // to obtain tvshows in popular category
            var tvshows = await _context.TvShowCategories
            .Where(tc => tc.CategoryId == upcomingCategoryId)
            .Select(tc => tc.Tvshow)
            .Distinct()      
            .Include(t => t.Genres)
            .ThenInclude(tg => tg.Genre)
            .Include(t => t.TvShowCategories)
            .ThenInclude(tc => tc.Category)
            .Distinct()
            .ToListAsync();
            return tvshows;
        }

        public async Task<bool> TvShowExists(int id)
        {
            return await _context.Tvshows.AnyAsync(e => e.Id == id);

        }
        public async Task<Tvshow> UpdateTvShowById(Tvshow tvshow)
        {
            _context.Entry(tvshow).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return await _context.Tvshows
                .Include(m => m.Genres)
                .ThenInclude(mg => mg.Genre)
                .Include(m => m.TvShowCategories)
                .ThenInclude(mc => mc.Category)
                .FirstOrDefaultAsync(m => m.Id == tvshow.Id);
        }

    }
}
