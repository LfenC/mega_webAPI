using System.ComponentModel.DataAnnotations.Schema;

namespace mega_webAPI.Data.models
{
    public class MovieGenre
    {
        [ForeignKey("Movie")]
        [Column("movie_id")]
        public int Id { get; set; }
        public Movie Movie { get; set; }

        [ForeignKey("Genre")]
        [Column("genre_id")]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
