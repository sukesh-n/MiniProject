using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizzApp.Models
{
    public class Security
    {
        [Key]
        public int SecurityId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public byte[]? Password { get; set; }
        public byte[]? PasswordHashKey { get; set; }
        public DateTime LastLogin { get; set; } = DateTime.Now;
        public User User { get; set; }
    }
}
