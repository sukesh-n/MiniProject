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
        public ICollection<Category> categories { get; set; }
        public ICollection<Question> questions { get; set; }


    }
}
