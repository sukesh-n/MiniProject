namespace QuizzApp.Models.DTO.UserServicesDTO
{
    public class LoginReturnDTO
    {
        public string UserEmail { get; set; } = string.Empty;
        public int UserID { get; set; }
        public string Token { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
