using mega_webAPI.Data.models;

namespace mega_webAPI.Data.interfaces
{
    public interface IMovie
    {
        Task<IEnumerable<Movie>> GetAllMovies();
        Task<Movie?> GetMovieById(int id);
        Task<Movie> AddMovie(Movie movie);
        Task<Movie> UpdateMovieById(Movie movie);

        //method to see if the id in the db is equal to the id given
        Task<bool> MovieExists(int id);
        Task DeleteMovie(int id);
        Task<IEnumerable<Movie>> GetPopularMoviesAsync();
        Task<IEnumerable<Movie>> GetUpcomingMoviesAsync();
        Task<IEnumerable<Movie>> GetTopRatedMoviesAsync();
        Task<IEnumerable<Movie>> GetFeaturedMoviesAsync();
        Task<IEnumerable<Genre>> GetGenresAsync();
        Task<IEnumerable<Movie>> GetMoviesByGenreIdAsync(int id);

        Task<string> GetMovieVideos(int id);

    }
}
