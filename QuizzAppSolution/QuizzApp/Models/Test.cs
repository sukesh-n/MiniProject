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
        public int QuestionsCount { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Question> Questions { get; set; }
        public User User { get; set; }
        public Test()
        {
            Categories = new List<Category>();
            Questions = new List<Question>();
        }

        // Constructor with parameters
        public Test(ICollection<Category> categories, ICollection<Question> questions)
        {
            Categories = categories ?? new List<Category>();
            Questions = questions ?? new List<Question>();
        }
    }
}
