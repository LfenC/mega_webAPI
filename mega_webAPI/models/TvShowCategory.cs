using System.ComponentModel.DataAnnotations.Schema;

namespace mega_webAPI.models
{
    public class TvShowCategory
    {
        [ForeignKey("TvShow")]
        public int TvShowId { get; set; }
        public Tvshow Tvshow { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
