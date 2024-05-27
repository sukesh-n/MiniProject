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
        public double? score { get; set; }

        public Result(double? score)
        {
            this.score = score;
        }
    }
}
