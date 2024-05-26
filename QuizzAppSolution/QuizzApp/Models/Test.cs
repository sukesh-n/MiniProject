using System.ComponentModel.DataAnnotations;

namespace QuizzApp.Models
{
    public class Test
    {
        [Key]
        public int TestId { get; set; }
        public DateTime TestStartDate { get; set; }
        public DateTime TestEndDate { get; set; }
        public int UserId { get; set; }
        public int QuestionsCount { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Question> Questions { get; set; }

        public Test(ICollection<Category> categories, ICollection<Question> questions)
        {
            Categories = new List<Category>();
            Questions = new List<Question>();
        }
    }
}
