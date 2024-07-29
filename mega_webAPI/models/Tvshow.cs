using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace mega_webAPI.models
{
    public class Tvshow
    {
        [Key]
        [Column("tvshow_id")]
        public int TvShowId { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("release_date")]
        public DateTime ReleaseDate { get; set; }

        [Column("rating")]
        public float Rating { get; set; }

        [Column("votes")]
        public int Votes { get; set; }

        [Column("image_url")]
        public string ImageUrl { get; set; }

        [Column("video_url")]
        public string VideoUrl { get; set; }

        [Column("added_date")]
        public DateTime AddedDate { get; set; } = DateTime.Now;

        public ICollection<Episode> Episodes { get; set; }
        public ICollection<TvShowCategory> TvShowCategories { get; set; }

    }
}
