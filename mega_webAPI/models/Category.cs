using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mega_webAPI.models

{
    public class Category
    {
        [Key]
        [Column("category_id")]
        public int CategoryId { get; set; }

       [Column("name")]
        public string Name { get; set; }

        public ICollection<MovieCategory> MovieCategories { get; set; }
        public ICollection<TvShowCategory> TvShowCategories { get; set; }

    }
}
