using mega_webAPI.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using mega_webAPI.Data.models;
using Microsoft.AspNetCore.Http.HttpResults;
using mega_webAPI.Data.interfaces;
using mega_webAPI.DTOs;

namespace mega_webAPI.Controllers

{
    [ApiController]
    [Route("api/[Controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovie _movieRepository;
        public MoviesController(IMovie movieRepository)
        {
            _movieRepository = movieRepository;
        }

        //GET ALL movies api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
        {
            try
            {
                var movies = await _movieRepository.GetAllMovies();
                if (movies == null || !movies.Any())
                    {   
                        return NotFound("No se encontraron películas");
                    }
                var movieDtos = movies.Select(movie => new MovieDto
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Description = movie.Description,
                    ReleaseDate = movie.ReleaseDate,
                    Rating = movie.Rating,
                    Votes = movie.Votes,
                    ImageUrl = movie.ImageUrl,
                    VideoUrl = movie.VideoUrl,
                    AddedDate = movie.AddedDate,

                    MovieCategories = movie.MovieCategories.Select(mc => new MovieCategoryDto
                    {
                        CategoryId = mc.CategoryId,
                        Name = mc.Category.Name
                    }).ToList() ?? new List<MovieCategoryDto>(),
                    /*MovieActors = movie.MovieActors.Select(ma => new MovieActorDto
                    {
                        ActorId = ma.ActorId
                    }).ToList(),*/
                    Genres = movie.Genres.Select(mg => new MovieGenreDto
                    {
                        GenreId = mg.GenreId,
                        Name = mg.Genre.Name
                    }).ToList() ?? new List<MovieGenreDto>()
                }).ToList();
                return Ok(movieDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }


        //GET movies by id api/Movies/id
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(int id)
        {
            try
            {
                var movie = await _movieRepository.GetMovieById(id);
                if (movie == null)
                {
                    return NotFound(new { message = $"La película con el Id {id} no se encontró." });
                }

                var movieDto = new MovieDto
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Description = movie.Description,
                    ReleaseDate = movie.ReleaseDate,
                    Rating = movie.Rating,
                    Votes = movie.Votes,
                    ImageUrl = movie.ImageUrl,
                    VideoUrl = movie.VideoUrl,
                    AddedDate = movie.AddedDate,

                    MovieCategories = movie.MovieCategories.Select(mc => new MovieCategoryDto
                    {
                        CategoryId = mc.CategoryId,
                        Name = mc.Category.Name
                    }).ToList(),
                    /*MovieActors = movie.MovieActors.Select(ma => new MovieActorDto
                    {
                        ActorId = ma.ActorId
                    }).ToList(),*/

                    Genres = movie.Genres.Select(mg => new MovieGenreDto
                    {
                        GenreId = mg.GenreId,
                        Name = mg.Genre.Name
                    }).ToList()
                };

                return Ok(movieDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }

        }


        //POST api/movies
        [HttpPost]
        public async Task<ActionResult<MovieDto>> PostMovie(MovieDto movieDto)
        {
            try
            {
                var movie = new Movie
                {
                    Id = movieDto.Id,
                    Title = movieDto.Title,
                    Description = movieDto.Description,
                    ReleaseDate = movieDto.ReleaseDate,
                    Rating = movieDto.Rating,
                    Votes = movieDto.Votes,
                    ImageUrl = movieDto.ImageUrl,
                    VideoUrl = movieDto.VideoUrl,
                    AddedDate = movieDto.AddedDate,
                    Genres = movieDto.Genres.Select(g => new MovieGenre { GenreId = g.GenreId }).ToList(),
                    MovieCategories = movieDto.MovieCategories.Select(c => new MovieCategory { CategoryId = c.CategoryId }).ToList()
                };

                var createdMovie = await _movieRepository.AddMovie(movie);

                var  movieConvertDto = new MovieDto
                {
                    Id = createdMovie.Id,
                    Title = createdMovie.Title,
                    Description = createdMovie.Description,
                    ReleaseDate = createdMovie.ReleaseDate,
                    Rating = createdMovie.Rating,
                    Votes = createdMovie.Votes,
                    ImageUrl = createdMovie.ImageUrl,
                    VideoUrl = createdMovie.VideoUrl,
                    AddedDate = createdMovie.AddedDate,
                    Genres = createdMovie.Genres.Select(g => new MovieGenreDto { GenreId = g.GenreId, Name = g.Genre.Name }).ToList(),
                    MovieCategories = createdMovie.MovieCategories.Select(c => new MovieCategoryDto { CategoryId = c.CategoryId, Name = c.Category.Name }).ToList()
                };
            
                return CreatedAtAction(nameof(GetMovie), new { movieId = createdMovie.Id }, movieConvertDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }


        //PUT api/movies/id Update by the id
        [HttpPut("{id}")]
        public async Task<ActionResult<MovieDto>> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            try
            {
                var updatedMovie = await _movieRepository.UpdateMovieById(movie);
                var movieDto = new MovieDto
                {
                    Id = updatedMovie.Id,
                    Title = updatedMovie.Title,
                    Description = updatedMovie.Description,
                    ReleaseDate = updatedMovie.ReleaseDate,
                    Rating = updatedMovie.Rating,
                    Votes = updatedMovie.Votes,
                    ImageUrl = updatedMovie.ImageUrl,
                    VideoUrl = updatedMovie.VideoUrl,
                    AddedDate = updatedMovie.AddedDate,
                    MovieCategories = updatedMovie.MovieCategories.Select(mc => new MovieCategoryDto
                    {
                        CategoryId = mc.CategoryId,
                        Name = mc.Category.Name
                    }).ToList(),
                    Genres = updatedMovie.Genres.Select(mg => new MovieGenreDto
                    {
                        GenreId = mg.GenreId,
                        Name = mg.Genre.Name
                    }).ToList()
                };

                return Ok(movieDto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _movieRepository.MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        //delete movie in databse by id
        //DELETE: api/movies/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Movie>> DeleteMovie(int id)
        {
            try
            {
                var movies = await _movieRepository.GetMovieById(id);
                if (movies == null)
                {
                    return NotFound();
                }
                await _movieRepository.DeleteMovie(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }

        }

        //GET popular movies
        [HttpGet("popular")]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetPopularMovies()
        {
            try
            {
                var movies = await _movieRepository.GetPopularMoviesAsync();
                if (movies == null || !movies.Any())
                {
                    return NotFound("No se encontraron películas populares");
                }

                var movieDtos = movies.Select(movie => new MovieDto
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Description = movie.Description,
                    ReleaseDate = movie.ReleaseDate,
                    Rating = movie.Rating,
                    Votes = movie.Votes,
                    ImageUrl = movie.ImageUrl,
                    VideoUrl = movie.VideoUrl,
                    AddedDate = movie.AddedDate,

                    MovieCategories = movie.MovieCategories.Select(mc => new MovieCategoryDto
                    {   
                        CategoryId = mc.CategoryId,
                        Name = mc.Category.Name
                    }).ToList() ?? new List<MovieCategoryDto>(),
                    /*MovieActors = movie.MovieActors.Select(ma => new MovieActorDto
                    {
                        ActorId = ma.ActorId
                    }).ToList(),*/
                    Genres = movie.Genres.Select(mg => new MovieGenreDto
                    {
                        GenreId = mg.GenreId,
                        Name = mg.Genre.Name
                    }).ToList() ?? new List<MovieGenreDto>()
                }).ToList();

                return Ok(movieDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        //GET upcoming movies
        [HttpGet("upcoming")]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetUpcomingMovies()
        {
            try
            {
                var movies = await _movieRepository.GetUpcomingMoviesAsync();

                if (movies == null || !movies.Any())
                {
                    return NotFound("No se encontraron películas Upcoming");
                }

                var movieDtos = movies.Select(movie => new MovieDto
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Description = movie.Description,
                    ReleaseDate = movie.ReleaseDate,
                    Rating = movie.Rating,
                    Votes = movie.Votes,
                    ImageUrl = movie.ImageUrl,
                    VideoUrl = movie.VideoUrl,
                    AddedDate = movie.AddedDate,

                    MovieCategories = movie.MovieCategories.Select(mc => new MovieCategoryDto
                    {
                        CategoryId = mc.CategoryId,
                        Name = mc.Category.Name
                    }).ToList() ?? new List<MovieCategoryDto>(),
                    /*MovieActors = movie.MovieActors.Select(ma => new MovieActorDto
                    {
                        ActorId = ma.ActorId
                    }).ToList(),*/
                    Genres = movie.Genres.Select(mg => new MovieGenreDto
                    {
                        GenreId = mg.GenreId,
                        Name = mg.Genre.Name
                    }).ToList() ?? new List<MovieGenreDto>()
                }).ToList();
                return Ok(movieDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ObjectResult(new { error = ex.Message }));
            }
        }

        //GET Top Rated Movies
        [HttpGet("toprated")]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetTopRatedMovies()
        {
            try
            {
                var movies = await _movieRepository.GetTopRatedMoviesAsync();

                if (movies == null || !movies.Any())
                {
                    return NotFound("No se encontraron películas Top rated");
                }

                var movieDtos = movies.Select(movie => new MovieDto
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Description = movie.Description,
                    ReleaseDate = movie.ReleaseDate,
                    Rating = movie.Rating,
                    Votes = movie.Votes,
                    ImageUrl = movie.ImageUrl,
                    VideoUrl = movie.VideoUrl,
                    AddedDate = movie.AddedDate,

                    MovieCategories = movie.MovieCategories.Select(mc => new MovieCategoryDto
                    {
                        CategoryId = mc.CategoryId,
                        Name = mc.Category.Name
                    }).ToList() ?? new List<MovieCategoryDto>(),
                    /*MovieActors = movie.MovieActors.Select(ma => new MovieActorDto
                    {
                        ActorId = ma.ActorId
                    }).ToList(),*/
                    Genres = movie.Genres.Select(mg => new MovieGenreDto
                    {
                        GenreId = mg.GenreId,
                        Name = mg.Genre.Name
                    }).ToList() ?? new List<MovieGenreDto>()
                }).ToList();
                return Ok(movieDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ObjectResult(new { error = ex.Message }));

            }
        }

        //GET upcoming movies
        [HttpGet("featured")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetFeaturedMovies()
        {
            try
            {
                var movies = await _movieRepository.GetFeaturedMoviesAsync();

                if (movies == null || !movies.Any())
                {
                    return NotFound("No se encontraron películas featured");
                }

                var movieDtos = movies.Select(movie => new MovieDto
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Description = movie.Description,
                    ReleaseDate = movie.ReleaseDate,
                    Rating = movie.Rating,
                    Votes = movie.Votes,
                    ImageUrl = movie.ImageUrl,
                    VideoUrl = movie.VideoUrl,
                    AddedDate = movie.AddedDate,

                    MovieCategories = movie.MovieCategories.Select(mc => new MovieCategoryDto
                    {
                        CategoryId = mc.CategoryId,
                        Name = mc.Category.Name
                    }).ToList() ?? new List<MovieCategoryDto>(),
                    /*MovieActors = movie.MovieActors.Select(ma => new MovieActorDto
                    {
                        ActorId = ma.ActorId
                    }).ToList(),*/
                    Genres = movie.Genres.Select(mg => new MovieGenreDto
                    {
                        GenreId = mg.GenreId,
                        Name = mg.Genre.Name
                    }).ToList() ?? new List<MovieGenreDto>()
                }).ToList();
                return Ok(movieDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ObjectResult(new { error = ex.Message }));
            }
        }

        [HttpGet("{id}/videos")]
        public async Task<ActionResult<string>> GetMovieVideos(int id)
        {
            try
            {
                var videos = await _movieRepository.GetMovieVideos(id);
                if (videos == null)
                {
                    return NotFound(new { message = $"No se encontraron videos para la película con Id {id}" });
                }
                return Ok(videos);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("movies/genres")]
        public async Task<ActionResult<IEnumerable<MovieGenreDto>>> GetMovieGenres()
        {
            try
            {
                var genres = await _movieRepository.GetGenresAsync();
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

    } 
}