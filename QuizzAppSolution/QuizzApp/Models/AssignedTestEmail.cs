﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizzApp.Models
{
    public class AssignedTestEmail
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("AssignedTest")]
        public int AssignmentNumber { get; set; }
        public string Email { get; set; } = string.Empty;
        public bool IsAdmin { get; set; } = false;
        public bool IsOrganizer { get; set; } = false;
        public bool IsCandidate { get; set; } = false;
        public AssignedTest AssignedTest { get; set; }
    }
}
