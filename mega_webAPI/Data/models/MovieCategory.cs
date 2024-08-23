using System.ComponentModel.DataAnnotations.Schema;
using mega_webAPI.Data.models;

namespace mega_webAPI.Data.models
{
    public class MovieCategory
    {
        [ForeignKey("Movie")]
        public int Id { get; set; }
        public Movie Movie { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
