﻿namespace QuizzApp.Models.DTO.Test
{
    public class NotificationDTO
    {
        public int UserEmail { get; set; }
        public string TestAssignedBy { get; set; } = string.Empty;
        public bool IsUserAlready { get; set; } = false;
        public int TestID { get; set; }
        public string TestName { get; set; } = string.Empty;
        public int QuestionCount { get; set; }
        public int TestDuration { get; set; }
        public DateTime TestWindowOpen { get; set; }
        public DateTime TestWindowClose { get; set; }
    }
}