using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizzApp.Models
{
    public class Solution
    {
        [Key]
        public int SolutionId { get; set; }
        public int QuestionId { get; set; }
        public int? OptionId { get; set; }
        public double? NumericalAnswer { get; set; } 
        public bool? TrueFalseAnswer { get; set; }
        [ForeignKey("QuestionId")]
        public Question Question { get; set; }

        [ForeignKey("OptionId")]
        public Option Option { get; set; }



    }
}
