using QuizzApp.Interfaces;
using QuizzApp.Interfaces.Test;
using QuizzApp.Models;
using QuizzApp.Models.DTO.Test;
using static System.Net.Mime.MediaTypeNames;

namespace QuizzApp.Services.AddOnServices
{
    public class NotificationService : INotificationService
    {

        private readonly IAssignedTestRepository _assignedTestRepository;
        private readonly ITestRepository _testRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAssignedTestEmailRepository _assignedTestEmailRepository;

        public NotificationService(IAssignedTestRepository assignedTestRepository, ITestRepository testRepository, IUserRepository userRepository, IAssignedTestEmailRepository assignedTestEmailRepository)
        {
            _assignedTestRepository = assignedTestRepository;
            _testRepository = testRepository;
            _userRepository = userRepository;
            _assignedTestEmailRepository = assignedTestEmailRepository;
        }

        public async Task<List<NotificationDTO>> CheckNotification(string email, string role)
        {
            try
            {
                var getAssignments = await _assignedTestEmailRepository.GetByUserEmailAsync(email);




                List<NotificationDTO> notificationDTOList = new List<NotificationDTO>();



                var AllAssignments = new List<AssignedTest>();
                var AllTests = new List<TestDTO>();
                foreach (var assignment in getAssignments)
                {
                    AssignedTest getAllAssignments = await _assignedTestRepository.GetByAssignemntNo(assignment.AssignmentNumber);
                    if (getAllAssignments == null)
                    {
                        throw new Exception("Assignement Not Found");
                    }
                    AllAssignments.Add(getAllAssignments);



                    TestDTO getAllTest = await _testRepository.GetTestByAssignmentNoAsync(assignment.AssignmentNumber);
                    if (getAllTest == null)
                    {
                        continue;
                        //throw new Exception("Test Not Found");
                    }
                    AllTests.Add(getAllTest);

                    NotificationDTO notificationDTO = new NotificationDTO();
                    {
                        notificationDTO.AssignmentNumber = assignment.AssignmentNumber;
                        notificationDTO.UserEmail = assignment.Email;
                        notificationDTO.TestName = getAllAssignments.TestName;
                        notificationDTO.TestWindowOpen = getAllAssignments.StartTimeWindow;
                        notificationDTO.TestWindowClose = getAllAssignments.EndTimeWindow;
                        notificationDTO.TestDuration = getAllAssignments.TestDuration;
                        notificationDTO.QuestionCount = getAllTest.QuestionsCount;
                        notificationDTO.TestID = getAllTest.TestId;
                        var getUser = await _userRepository.GetAsync(getAllAssignments.AssignedBy);
                        notificationDTO.TestAssignedBy = getUser.UserName;

                        notificationDTO.IsUserAlready = assignment.IsAdmin||assignment.IsOrganizer||assignment.IsCandidate;
                    }
                    notificationDTOList.Add(notificationDTO);
                }

                //foreach (var test in AllAssignments)
                //{
                //    TestDTO getAllTest = await _testRepository.GetTestByAssignmentNoAsync(test.AssignmentNo);
                //    if (getAllTest == null)
                //    {
                //        throw new Exception("Test Not Found");
                //    }
                //    AllTests.Add(getAllTest);
                //}
                
                
                //var getUser = await _userRepository.GetUserByEmailAsync(email);


                return notificationDTOList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
