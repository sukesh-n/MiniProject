namespace QuizzApp.Models.DTO.Test
{
    public class TestDetailsDTO
    {
        public string CandidateEmails { get; set; } = string.Empty;
        public bool IsCandidate { get; set; } = false;
        public bool IsAdmin { get; set; } = false;
        public bool IsOrganizer { get; set; } = false;

        public string StatusOfTest { get; set; } = "Not Attended";
        public DateTime TestStartTime { get; set; }
        public DateTime TestEndTime { get; set; }

    }
}
