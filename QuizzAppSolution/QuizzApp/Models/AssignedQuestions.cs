using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizzApp.Models
{
    [Keyless]
    public class AssignedQuestions
    {
        [ForeignKey("Test")]
        public int TestId { get; set; }

        [ForeignKey("Question")]
        public List<int> QuestionId { get; set; } = new List<int>();
    }
}
