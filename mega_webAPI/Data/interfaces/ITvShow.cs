using mega_webAPI.Data.models;

namespace mega_webAPI.Data.interfaces
{
    public interface ITvShow
    {
        Task<IEnumerable<Tvshow>> GetAllTvShows();
        Task<Tvshow?> GetTvShowById(int id);
        Task<Tvshow> AddTvShow(Tvshow tvshow);
        Task<Tvshow> UpdateTvShowById(Tvshow tvshow);

        //method to see if the id in the db is equal to the id given
        Task<bool> TvShowExists(int id);
        Task DeleteTvShow(int id);
        Task<IEnumerable<Tvshow>> GetPopularTvShowsAsync();
        Task<IEnumerable<Tvshow>> GetUpcomingTvShowsAsync();
        Task<IEnumerable<Tvshow>> GetTopRatedTvShowsAsync();

    }
}
