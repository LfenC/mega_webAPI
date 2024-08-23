using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using mega_webAPI.Data.models;

namespace mega_webAPI.Data.models

{
    public class Category
    {
        [Key]
        [Column("category_id")]
        public int CategoryId { get; set; }

       [Column("name")]
        public string Name { get; set; }

        //relation many to many with table Category
        
        public ICollection<MovieCategory> MovieCategories { get; set; }
     
        public ICollection<TvShowCategory> TvShowCategories { get; set; }

    }
}
