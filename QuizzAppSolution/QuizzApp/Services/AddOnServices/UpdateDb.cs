using QuizzApp.Interfaces;
using QuizzApp.Interfaces.Live;
using QuizzApp.Interfaces.Test;
using QuizzApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizzApp.Services.AddOnServices
{
    public class UpdateDb : IUpdateDb
    {
        private readonly IUserRepository _userRepository;
        private readonly IAssignedTestEmailRepository _assignedTestEmailRepository;

        public UpdateDb(IUserRepository userRepository, IAssignedTestEmailRepository assignedTestEmailRepository)
        {
            _userRepository = userRepository;
            _assignedTestEmailRepository = assignedTestEmailRepository;
        }

        public async Task<bool> UpdateAssignedTestEmail()
        {
            try
            {

                var assignedTestEmailEntities = await _assignedTestEmailRepository.GetAllAsync();

                
                List<string> emails = new List<string>();
                foreach (var email in assignedTestEmailEntities)
                {
                    emails.Add(email.Email);
                }

                
                var userEntities = await _userRepository.GetAllDetailsByEmailsAsync(emails);

                List<AssignedTestEmail> UpdatedssignedTestEmails = new List<AssignedTestEmail>();
                foreach (var assignedTestEmail in assignedTestEmailEntities)
                {
                    foreach (var user in userEntities)
                    {
                        if (assignedTestEmail.Email == user.UserEmail)
                        {
                            if(user.Role.ToLower() == "candidate")
                            {
                                assignedTestEmail.IsCandidate = true;
                            }
                            if(user.Role.ToLower() == "organizer")
                            {

                               assignedTestEmail.IsOrganizer = true;
                            }
                            if(user.Role.ToLower() == "admin")
                            {
                                assignedTestEmail.IsAdmin = true;
                            }
                        }
                    }
                }
                var status = await _assignedTestEmailRepository.UpdateDb(UpdatedssignedTestEmails); 
                return true; 
            }
            catch (Exception ex)
            {
                throw new Exception("Error in updating assigned test emails", ex);
            }
        }
    }
}
