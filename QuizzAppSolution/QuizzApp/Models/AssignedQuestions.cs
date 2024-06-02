using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizzApp.Models
{
    public class AssignedQuestions
    {

        [Key]
        [ForeignKey("AssignedTest")]
        public int AssignmentNumber { get; set; }
        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public AssignedTest AssignedTest { get; set; }
    }
}
