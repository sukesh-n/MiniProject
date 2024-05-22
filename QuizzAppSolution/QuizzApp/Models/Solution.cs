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
        public string CorrectAnswer { get; set;}
        
        

    }
}
