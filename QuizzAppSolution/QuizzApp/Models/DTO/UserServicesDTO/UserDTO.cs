namespace QuizzApp.Models.DTO.UserServicesDTO
{
    public class UserDTO : User
    {
        public byte[] Password { get; set; }
        public byte[] PasswordhashKey { get; set; }
    }
}
