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
        private readonly IUserRepository _userRepository;
        private readonly IAssignedTestEmailRepository _assignedTestEmailRepository;

        public NotificationService(IAssignedTestRepository assignedTestRepository, IRepository<int, Test> testRepository, IUserRepository userRepository, IAssignedTestEmailRepository assignedTestEmailRepository)
        {
            _assignedTestRepository = assignedTestRepository;
            _testRepository = testRepository;
            _userRepository = userRepository;
            _assignedTestEmailRepository = assignedTestEmailRepository;
        }

        public async Task<NotificationDTO> CheckNotification(string email, string role)
        {
            try
            {
                var getEmail = await _assignedTestEmailRepository.GetByUserEmailAsync(email);
                var CheckUser = await _assignedTestRepository.GetUserByEmailAsync(email);

                var getUser = await _userRepository.GetUserByEmailAsync(email);

                var 
                if (getUser == null)
                {
                    throw new Exception("User Not Found");
                }
                var get
                var getTest = await _testRepository.GetByUserIdAndAssignemntNoAsync(CheckUser.AssignmentNo)

                if (getTest == null)
                {
                    throw new Exception("Test Not Found");
                }
                NotificationDTO notificationDTO = new NotificationDTO();
                {
                    notificationDTO.TestName = CheckUser.TestName;
                    notificationDTO.AssignmentNumber=CheckUser.AssignmentNo;
                   // notificationDTO.QuestionCount=CheckUs
                    notificationDTO.TestWindowOpen=CheckUser.StartTimeWindow;
                    notificationDTO.TestWindowClose=CheckUser.EndTimeWindow;
                    notificationDTO.TestDuration=CheckUser.TestDuration;
                    //notificationDTO.UserEmail=CheckUser.;
                    //notificationDTO.Role=role;
                }
                return notificationDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
