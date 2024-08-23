using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using mega_webAPI.Data.models;
using System.Text.Json.Serialization;

namespace mega_webAPI.Data.models
{
    public class Movie
    {
        [Key]
        [Column("movie_id")]
        public int Id { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("release_date")]
        public DateTime ReleaseDate { get; set; }

        [Column("rating")]
        public double Rating { get; set; }

        [Column("votes")]
        public int Votes { get; set; }

        [Column("image_url")]
        public string ImageUrl { get; set; }

        [Column("video_url")]
        public string VideoUrl { get; set; }

        [Column("added_date")]
        public DateTime AddedDate { get; set; } = DateTime.Now;
    
        public ICollection<MovieCategory> MovieCategories { get; set; } = new List<MovieCategory>();
       // public ICollection<MovieActor> MovieActors { get; set; }
     
        public ICollection<MovieGenre> Genres { get; set; } = new List<MovieGenre>();


    }
}
