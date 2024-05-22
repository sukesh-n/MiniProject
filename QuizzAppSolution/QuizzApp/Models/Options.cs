using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizzApp.Models
{
    public class Options
    {
        [ForeignKey("Question")]
        [Required]
        public int QuestionId { get; set; }
        [Required]
        public int OptionId { get; set; }
        [ForeignKey("Solution")]
        public int SolutionId { get; set; }
    }
}
