using System.ComponentModel.DataAnnotations;

namespace QuizzApp.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string MainCategory { get; set; } = string.Empty.ToLower();
        public string SubCategory { get; set; } =  string.Empty.ToLower();

    }
}
