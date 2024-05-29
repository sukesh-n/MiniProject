using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizzApp.Models
{
    public class AssignedTest
    {
        [Key]
        public int AssignmentNo { get; set; }
        [ForeignKey("Users")]
        public int AssignedBy { get; set; }
        public string TestName { get; set; } = string.Empty;
        public IList<AssignedTestEmail> AssignedTo { get; set; } = new List<AssignedTestEmail>();
        public DateTime StartTimeWindow { get; set; } 
        public DateTime EndTimeWindow { get; set; } 
        public TimeSpan TestDurationInMinutes { get; set; } 



    }
}
