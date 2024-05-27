using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizzApp.Models
{
    public class Solution
    {
        [Key]
        public int SolutionId { get; set; }
        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        public int? OptionId { get; set; }
        public double? NumericalAnswer { get; set; } 
        public bool? TrueFalseAnswer { get; set; }

        public Option Option { get; set; }
        public Question Question { get; set; }


    }
}
