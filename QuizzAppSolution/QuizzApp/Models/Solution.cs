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
        public string CorrectOptionAnswer { get; set; } = string.Empty;
        public double? NumericalAnswer { get; set; } = null;
        public bool? TrueFalseAnswer { get; set; } = null;
        public Option? Option { get; set; }
        public Question Question { get; set; }


    }
}
