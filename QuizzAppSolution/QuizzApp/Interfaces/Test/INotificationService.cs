using QuizzApp.Models.DTO.Test;

namespace QuizzApp.Interfaces.Test
{
    public interface INotificationService
    {
        Task<NotificationDTO> CheckNotification(string email, string role);

    }
}
