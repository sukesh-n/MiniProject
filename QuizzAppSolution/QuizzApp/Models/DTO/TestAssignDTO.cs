using Microsoft.AspNetCore.Identity;
using QuizzApp.Models.DTO.Test;

namespace QuizzApp.Models.DTO
{
    public class TestAssignDTO : AssignedTest
    {
        public List<string> CandidateEmails {  get; set; } = new List<string>();
        
    }
}
