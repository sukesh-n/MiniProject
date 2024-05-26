using System.ComponentModel.DataAnnotations;

namespace QuizzApp.Models
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }
        public string QuestionDescription { get; set; } = string.Empty;
        public string QuestionType { get; set; } = string.Empty;
        public int CategoryId {  get; set; }
        public int Difficultylevel { get; set; }
        public Category Category { get; set; }

    }
}
