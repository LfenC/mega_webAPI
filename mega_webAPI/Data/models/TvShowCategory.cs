using mega_webAPI.Data.models;
using System.ComponentModel.DataAnnotations.Schema;

namespace mega_webAPI.Data.models
{
    public class TvShowCategory
    {
        [ForeignKey("TvShow")]
        [Column("tvshow_id")]
        public int tvShowId { get; set; }
        public Tvshow Tvshow { get; set; }

        [ForeignKey("Category")]
        [Column("category_id")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
