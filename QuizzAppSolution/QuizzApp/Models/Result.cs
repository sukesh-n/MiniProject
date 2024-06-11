using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizzApp.Models
{
    public class Result
    {
        [Key]
        public int ResultId { get; set; }
        [ForeignKey("Test")]
        public int TestId { get; set; }
        public float? score { get; set; }
        public Test Test { get; set; }
    }
}
