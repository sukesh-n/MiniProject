using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizzApp.Models
{
    public class Option
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        public int Optionid { get; set; }
        public OptionDescriptionType OptionDescription { get; set; }
        public string Value { get; set; } = string.Empty;
        public Question Question { get; set; }
        public enum OptionDescriptionType
        {
            Integer,
            String
        }

    }

    

}
