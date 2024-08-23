using mega_webAPI.Data.models;

namespace mega_webAPI.DTOs
{
    public class MovieDto
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
        public List<MovieCategoryDto> MovieCategories { get; set; } = new List<MovieCategoryDto>();
        //public List<MovieActorDto> MovieActors { get; set; }
        public List<MovieGenreDto> Genres { get; set; } = new List<MovieGenreDto>();


    }
}
