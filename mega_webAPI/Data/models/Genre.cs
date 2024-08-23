using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using mega_webAPI.Data.models;
using System.Text.Json.Serialization;

namespace mega_webAPI.Data.models
{
    public class Genre
    {
        [Key]
        [Column("genre_id")]
        public int GenreId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        //relation many to many 
  
        public ICollection<MovieGenre> MovieGenres { get; set; }
        public ICollection<TvShowGenre> TvShowGenres { get; set; }
    }
}
/*
 Proyección Selectiva:

    En lugar de devolver 
el objeto completo, devuelve solo los datos necesarios en la respuesta, 
evitando incluir propiedades que causen ciclos. es que quiero eso 
pero no sé como hacerlo*/