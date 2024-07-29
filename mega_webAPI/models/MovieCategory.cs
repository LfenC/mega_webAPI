using System.ComponentModel.DataAnnotations.Schema;

namespace mega_webAPI.models
{
    public class MovieCategory
    {
        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
