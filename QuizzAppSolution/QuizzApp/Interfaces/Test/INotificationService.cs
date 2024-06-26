using QuizzApp.Models.DTO.Test;

namespace QuizzApp.Interfaces.Test
{
    public interface INotificationService
    {
        Task<List<NotificationDTO>> CheckNotification(string email, string role,string UserId);

    }
}
