using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using mega_webAPI.Data.models;

namespace mega_webAPI.Data.models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(45)]
        public string Email { get; set; }

        [Required]
        [MaxLength(45)]
        public string Password { get; set; }

        [Column("register_date")]
        public DateTime RegisterDate { get; set; } = DateTime.Now;

        [MaxLength(45)]
        [Column("first_name")]
        public string FirstName { get; set; }

        [MaxLength(45)]
        [Column("last_name")]
        public string LastName { get; set; }
    }
}
