using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizzApp.Models
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }
        public string QuestionDescription { get; set; } = string.Empty;
        public string QuestionType { get; set; } = string.Empty;
        [ForeignKey("Category")]
        public int CategoryId {  get; set; }
        public int DifficultyLevel { get; set; }
        


    }
}
