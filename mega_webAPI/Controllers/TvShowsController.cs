using mega_webAPI.Context;
using mega_webAPI.Data.interfaces;
using mega_webAPI.Data.models;
using Microsoft.AspNetCore.Http.HttpResults;
using mega_webAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using mega_webAPI.Data.Repositories;


namespace mega_webAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class TvShowsController:ControllerBase
    { 
        private readonly ITvShow _tvShowRepository;
        public TvShowsController(ITvShow tvShowRepository)
        {
            _tvShowRepository = tvShowRepository;
        }

        //GET ALL tvshows api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TvShowDto>>> GetTvShows()
        {
            try
            {
                var tvshows = await _tvShowRepository.GetAllTvShows();
                if (tvshows == null || !tvshows.Any())
                {
                    return NotFound("No se encontraron series");
                }
                var tvShowDtos = tvshows.Select(tvshow => new TvShowDto
                {
                    Id = tvshow.Id,
                    Title = tvshow.Title,
                    Description = tvshow.Description,
                    ReleaseDate = tvshow.ReleaseDate,
                    Rating = tvshow.Rating,
                    Votes = tvshow.Votes,
                    ImageUrl = tvshow.ImageUrl,
                    VideoUrl = tvshow.VideoUrl,
                    AddedDate = tvshow.AddedDate,

                    TvShowCategories = tvshow.TvShowCategories.Select(tc => new TvShowCategoryDto
                    {
                        CategoryId = tc.CategoryId,
                        Name = tc.Category.Name
                    }).ToList() ?? new List<TvShowCategoryDto>(),
                    
                    Genres = tvshow.Genres.Select(mg => new TvShowGenreDto
                    {
                        GenreId = mg.GenreId,
                        Name = mg.Genre.Name
                    }).ToList() ?? new List<TvShowGenreDto>()
                }).ToList();          
                return Ok(tvShowDtos);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        //GET tvshow by id api/tvshow/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TvShowDto>> GetTvShow(int id)
        {
            try
            {
                var tvshow = await _tvShowRepository.GetTvShowById(id);
                if (tvshow == null)
                {
                    return NotFound(new { message = $"La serie con el Id {id} no se encontró." });
                }
                var tvShowDto = new TvShowDto
                {
                    Id = tvshow.Id,
                    Title = tvshow.Title,
                    Description = tvshow.Description,
                    ReleaseDate = tvshow.ReleaseDate,
                    Rating = tvshow.Rating,
                    Votes = tvshow.Votes,
                    ImageUrl = tvshow.ImageUrl,
                    VideoUrl = tvshow.VideoUrl,
                    AddedDate = tvshow.AddedDate,

                    TvShowCategories = tvshow.TvShowCategories.Select(tc => new TvShowCategoryDto
                    {
                        CategoryId = tc.CategoryId,
                        Name = tc.Category.Name
                    }).ToList(),
                    /*MovieActors = movie.MovieActors.Select(ma => new MovieActorDto
                    {
                        ActorId = ma.ActorId
                    }).ToList(),*/

                    Genres = tvshow.Genres.Select(tg => new TvShowGenreDto
                    {
                        GenreId = tg.GenreId,
                        Name = tg.Genre.Name
                    }).ToList()
                };
                return Ok(tvShowDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }

        }


        //POST api/tvshows
        [HttpPost]
        public async Task<ActionResult<TvShowDto>> PostMovie(Tvshow tvshow)
        {
            try
            {
                var createdTvShow = await _tvShowRepository.AddTvShow(tvshow);
                return CreatedAtAction(nameof(GetTvShow), new { tvshowId = createdTvShow.Id }, createdTvShow);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        //PUT api/tvshows/id Update by the id
        [HttpPut("{id}")]
        public async Task<ActionResult<Movie>> PutMovie(int id, Tvshow tvshow)
        {
            if (id != tvshow.Id)
            {
                return BadRequest();
            }

            try
            {
                await _tvShowRepository.UpdateTvShowById(tvshow);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _tvShowRepository.TvShowExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        //delete tvshows in databse by id
        //DELETE: api/tvshows/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Movie>> DeleteMovie(int id)
        {
            try
            {
                var tvShows = await _tvShowRepository.GetTvShowById(id);
                if (tvShows == null)
                {
                    return NotFound();
                }
                await _tvShowRepository.DeleteTvShow(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }

        }

        //GET popular tvshows
        [HttpGet("popular")]
        public async Task<ActionResult<IEnumerable<TvShowDto>>> GetPopularTvShows()
        {
            try
            {
                var tvShows = await _tvShowRepository.GetPopularTvShowsAsync();
                if (tvShows == null || !tvShows.Any())
                {
                    return NotFound("No se encontraron películas populares");
                }
                var tvShowDtos = tvShows.Select(tvShow => new TvShowDto
                {
                    Id = tvShow.Id,
                    Title = tvShow.Title,
                    Description = tvShow.Description,
                    ReleaseDate = tvShow.ReleaseDate,
                    Rating = tvShow.Rating,
                    Votes = tvShow.Votes,
                    ImageUrl = tvShow.ImageUrl,
                    VideoUrl = tvShow.VideoUrl,
                    AddedDate = tvShow.AddedDate,

                    TvShowCategories = tvShow.TvShowCategories.Select(tc => new TvShowCategoryDto
                    {
                        CategoryId = tc.CategoryId,
                        Name = tc.Category.Name
                    }).ToList() ?? new List<TvShowCategoryDto>(),
                    /*MovieActors = movie.MovieActors.Select(ma => new MovieActorDto
                    {
                        ActorId = ma.ActorId
                    }).ToList(),*/
                    Genres = tvShow.Genres.Select(tg => new TvShowGenreDto
                    {
                        GenreId = tg.GenreId,
                        Name = tg.Genre.Name
                    }).ToList() ?? new List<TvShowGenreDto>()
                }).ToList();

                return Ok(tvShowDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        //GET upcoming movies
        [HttpGet("upcoming")]
        public async Task<ActionResult<IEnumerable<TvShowDto>>> GetUpcomingTvShows()
        {
            try
            {
                var tvShows = await _tvShowRepository.GetUpcomingTvShowsAsync();

                if (tvShows == null || !tvShows.Any())
                {
                    return NotFound("Categoría Upcoming no encontrada");
                }
                var tvShowDtos = tvShows.Select(tvShow => new TvShowDto
                {
                    Id = tvShow.Id,
                    Title = tvShow.Title,
                    Description = tvShow.Description,
                    ReleaseDate = tvShow.ReleaseDate,
                    Rating = tvShow.Rating,
                    Votes = tvShow.Votes,
                    ImageUrl = tvShow.ImageUrl,
                    VideoUrl = tvShow.VideoUrl,
                    AddedDate = tvShow.AddedDate,

                    TvShowCategories = tvShow.TvShowCategories.Select(tc => new TvShowCategoryDto
                    {
                        CategoryId = tc.CategoryId,
                        Name = tc.Category.Name
                    }).ToList() ?? new List<TvShowCategoryDto>(),
                    /*MovieActors = movie.MovieActors.Select(ma => new MovieActorDto
                    {
                        ActorId = ma.ActorId
                    }).ToList(),*/
                    Genres = tvShow.Genres.Select(tg => new TvShowGenreDto
                    {
                        GenreId = tg.GenreId,
                        Name = tg.Genre.Name
                    }).ToList() ?? new List<TvShowGenreDto>()
                }).ToList();
                return Ok(tvShowDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ObjectResult(new { error = ex.Message }));
            }
        }

        //GET Top Rated Movies
        [HttpGet("toprated")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetTopRatedTvShows()
        {
            try
            {
                var tvShows = await _tvShowRepository.GetTopRatedTvShowsAsync();

                if (tvShows == null || !tvShows.Any())
                {
                    return NotFound("Categoría Top Rated no encontrada");
                }
                var tvShowDtos = tvShows.Select(tvShow => new TvShowDto
                {
                    Id = tvShow.Id,
                    Title = tvShow.Title,
                    Description = tvShow.Description,
                    ReleaseDate = tvShow.ReleaseDate,
                    Rating = tvShow.Rating,
                    Votes = tvShow.Votes,
                    ImageUrl = tvShow.ImageUrl,
                    VideoUrl = tvShow.VideoUrl,
                    AddedDate = tvShow.AddedDate,

                    TvShowCategories = tvShow.TvShowCategories.Select(tc => new TvShowCategoryDto
                    {
                        CategoryId = tc.CategoryId,
                        Name = tc.Category.Name
                    }).ToList() ?? new List<TvShowCategoryDto>(),
                    /*MovieActors = movie.MovieActors.Select(ma => new MovieActorDto
                    {
                        ActorId = ma.ActorId
                    }).ToList(),*/
                    Genres = tvShow.Genres.Select(tg => new TvShowGenreDto
                    {
                        GenreId = tg.GenreId,
                        Name = tg.Genre.Name
                    }).ToList() ?? new List<TvShowGenreDto>()
                }).ToList();
                return Ok(tvShowDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ObjectResult(new { error = ex.Message }));

            }
        }
        [HttpGet("tvshows/genres")]
        public async Task<ActionResult<IEnumerable<TvShowGenreDto>>> GetTvShowGenres()
        {
            try
            {
                var genres = await _tvShowRepository.GetGenresAsync();
                if (genres == null || !genres.Any())
                {
                    return NotFound("No se encontraron géneros");
                }
                var genreDtos = genres.Select(g => new GenreDto
                //se cambio de new MovieGenreDto a GenreDto
                {
                    GenreId = g.GenreId,
                    Name = g.Name,

                }).ToList();

                return Ok(genreDtos);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });

            }
        }
        [HttpGet("{id}/videos")]
        public async Task<ActionResult<string>> GetTvShowVideos(int id)
        {
            try
            {
                var videos = await _tvShowRepository.GetTvShowVideos(id);
                if (videos == null)
                {
                    return NotFound(new { message = $"No se encontraron videos para la serie con Id {id}" });
                }
                return Ok(videos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

    }
}
