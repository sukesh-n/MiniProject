using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizzApp.Models
{
    public class Test
    {
        [Key]
        public int TestId { get; set; }
        public DateTime TestStartDate { get; set; }
        public DateTime TestEndDate { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public string TestType { get; set; } = "custom";
        [ForeignKey("AssignedTest")]
        public int? AssignmentNo {  get; set; } 
        public string StatusOfTest { get; set; } = "Not Attended";
        public int QuestionsCount { get; set; }
        public User User { get; set; }
    }
}
