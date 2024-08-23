using mega_webAPI.Data.models;

namespace mega_webAPI.DTOs
{
    public class TvShowDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double Rating { get; set; }
        public int Votes { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }

        public DateTime AddedDate { get; set; } = DateTime.Now;
        public List<Episode> Episodes { get; set; }
        public List<TvShowCategoryDto> TvShowCategories { get; set; }
        public List<TvShowGenreDto> Genres { get; set; }

    }
}
