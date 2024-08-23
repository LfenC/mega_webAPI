using System.ComponentModel.DataAnnotations.Schema;

namespace mega_webAPI.Data.models
{
    public class TvShowGenre
    {
        [ForeignKey("TvShow")]
        [Column("tvshow_id")]
        public int tvShowId { get; set; }
        public Tvshow Tvshow { get; set; }

        [ForeignKey("Genre")]
        [Column("genre_id")]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
