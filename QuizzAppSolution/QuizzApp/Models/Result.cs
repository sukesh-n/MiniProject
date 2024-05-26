using System.ComponentModel.DataAnnotations;

namespace QuizzApp.Models
{
    public class Result
    {
        [Key]
        public int ResultId { get; set; }
        public int TestId { get; set; }
        public double? score { get; set; }

        public Result(double? score)
        {
            this.score = score;
        }
    }
}
