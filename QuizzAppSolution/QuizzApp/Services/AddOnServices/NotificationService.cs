using QuizzApp.Interfaces;
using QuizzApp.Interfaces.Test;
using QuizzApp.Models;
using QuizzApp.Models.DTO.Test;

namespace QuizzApp.Services.AddOnServices
{
    public class NotificationService : INotificationService
    {

        private readonly IAssignedTestRepository _assignedTestRepository;
        private readonly IRepository<int, Test> _testRepository;

        public NotificationService(IAssignedTestRepository assignedTestRepository, IRepository<int, Test> testRepository)
        {
            _assignedTestRepository = assignedTestRepository;
            _testRepository = testRepository;
        }

        public async Task<NotificationDTO> CheckNotification(string email, string role)
        {
            try
            {
                var CheckUser = await _assignedTestRepository.GetUserByEmailAsync(email);

                NotificationDTO notificationDTO = new NotificationDTO()
                {

                };
                return notificationDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
