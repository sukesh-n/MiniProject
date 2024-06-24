using QuizzApp.Models;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using QuizzApp.Exceptions;
using QuizzApp.Interfaces;
using QuizzApp.Models;
using QuizzApp.Models.DTO;
using QuizzApp.Repositories;
using QuizzApp.Models.DTO.UserServicesDTO;
using QuizzApp.Token;

namespace QuizzApp.Services
{
    public class UserService : ILoginService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRepository<int, Security> _securityRepository;
        private readonly ITokenService _tokenService;

        public UserService(IUserRepository userRepository, IRepository<int, Security> securityRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _securityRepository = securityRepository;
            _tokenService = tokenService;
        }

        public async Task<LoginReturnDTO> AdminLogin(string Email, string Password)
        {
            try
            {
                var getUsers = await _userRepository.GetUserByEmailAsync(Email);
                var getUser = getUsers.FirstOrDefault();
                int userfound = 0;
                if (getUser != null)
                {
                    foreach (var user in getUsers)
                    {
                        if (user.Role.ToLower() == "admin")
                        {

                            getUser = user;
                            userfound = 1;
                            break;
                        }
                    }
                    if (userfound == 0)                    
                        getUser = null;
                }
                if (getUser != null)
                {
                    var getSecurity = await _securityRepository.GetAsync(getUser.UserId);
                    if (getSecurity != null)
                    {
                        var IspasswordSame = ComparePassword(Password, getSecurity.Password, getSecurity.PasswordHashKey);
                        if (IspasswordSame)
                        {
                            if (getUser.Role.ToLower() != "admin")
                            {
                                throw new UnauthorizedAccessException("Youre not admin");
                            }
                            LoginReturnDTO loginReturnDTO = LoginReturn(getUser);
                            return loginReturnDTO;

                        }

                    }
                }
                throw new WrongCredentialsException("Invalid email or password");
            }
            catch (Exception ex)
            {
                throw new WrongCredentialsException("Invalid email or password");
            }
        }
        public LoginReturnDTO LoginReturn(User user)
        {
            LoginReturnDTO loginReturnDTO = new LoginReturnDTO()
            {
                UserEmail = user.UserEmail,
                UserID = user.UserId,
                Role = user.Role,
                Token = _tokenService.GenerateToken(user)
            }; return loginReturnDTO;
        }

        public async Task<LoginReturnDTO> CandidateLogin(string Email, string Password)
        {
            try
            {
                var getUsers = await _userRepository.GetUserByEmailAsync(Email);
                var getUser = getUsers.FirstOrDefault();
                int userfound = 0;
                if (getUser != null)
                {
                    foreach (var user in getUsers)
                    {
                        if (user.Role.ToLower() == "candidate")
                        {
                            getUser = user;
                            userfound = 1;
                            break;
                        }
                    }
                    if (userfound == 0)
                        getUser = null;
                
                }
                if (getUser != null)
                {
                    var getSecurity = await _securityRepository.GetAsync(getUser.UserId);
                    if (getSecurity != null)
                    {
                        var IspasswordSame = ComparePassword(Password, getSecurity.Password, getSecurity.PasswordHashKey);
                        if (IspasswordSame)
                        {
                            if (getUser.Role.ToLower() != "Candidate".ToLower())
                            {
                                throw new UnauthorizedAccessException("Youre not admin");
                            }
                            LoginReturnDTO loginReturnDTO = LoginReturn(getUser);
                            return loginReturnDTO;

                        }

                    }
                }
                throw new WrongCredentialsException("Invalid email or password");
            }
            catch (Exception ex)
            {
                throw new WrongCredentialsException("Invalid email or password");
            }
        }

        public async Task<LoginReturnDTO> OrganizerLogin(string Email, string Password)
        {
            try
            {
                var getUsers = await _userRepository.GetUserByEmailAsync(Email);
                var getUser = new User();
                int userfound =0;
                if (getUser != null)
                {
                    foreach (var user in getUsers)
                    {
                        if (user.Role.ToLower() == "organizer")
                        {
                            getUser = user;
                            userfound = 1;
                            break;
                        }
                    }
                    if(userfound==0)
                        getUser=null;
                }
                if (getUser != null)
                {
                    var getSecurity = await _securityRepository.GetAsync(getUser.UserId);
                    if (getSecurity != null)
                    {
                        var IspasswordSame = ComparePassword(Password, getSecurity.Password, getSecurity.PasswordHashKey);
                        if (IspasswordSame)
                        {
                            if (getUser.Role.ToLower() != "organizer")
                            {
                                throw new UnauthorizedAccessException("Youre not admin");
                            }
                            LoginReturnDTO loginReturnDTO = LoginReturn(getUser);
                            return loginReturnDTO;

                        }

                    }
                }
                throw new WrongCredentialsException("Invalid email or password");
            }
            catch (Exception ex)
            {
                throw new WrongCredentialsException("Invalid email or password");
            }
        }

        public async Task<User> RegisterUser(UserDTO userDTO)
        {
            try
            {
                var user = new User()
                {
                    UserName = userDTO.UserName,
                    UserEmail = userDTO.UserEmail,
                    Role = userDTO.Role,
                    JoiningDate = DateTime.Now
                };

                var UserAdd = await _userRepository.AddAsync(user);


                var (PasswordByte, PasswordHashKey_) = PasswordHashing(userDTO.Password);
                var AddingSecurity = new Security()
                {
                    UserId = UserAdd.UserId,
                    //HMACSHA512
                    Password = PasswordByte,
                    PasswordHashKey = PasswordHashKey_,
                    User = UserAdd

                };

                var result = await _securityRepository.AddAsync(AddingSecurity);
                if (result == null)
                {
                    await _userRepository.DeleteAsync(UserAdd.UserId);
                    return null;
                }
                return UserAdd;
            }
            catch
            {

                throw new UnableToAddUserException();
            }
        }
        public (byte[] PasswordByte, byte[] PasswordHashKey_) PasswordHashing(string Password)
        {
            HMACSHA512 hMACSHA512 = new HMACSHA512();
            var PasswordBytesHash = hMACSHA512.ComputeHash(ConvertToByte(Password));
            var key = hMACSHA512.Key;
            return (PasswordBytesHash, key);
        }
        public byte[] ConvertToByte(string Password)
        {
            return Encoding.UTF8.GetBytes(Password);
        }
        private bool ComparePassword(string password, byte[] storedPassword, byte[] key)
        {
            using (var hmac = new HMACSHA512(key))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedPassword[i]) return false;
                }
            }
            return true;
        }

        public async Task<bool> DeleteUser(string? email, int? userId, string? role)
        {
            try
            {
                if (role.ToLower() == "admin")
                {
                    throw new UnauthorizedException("Admin cannot be removed");
                }
                if (email == null)
                {
                    if (userId == null && role != null)
                    {
                        throw new InvalidFormatException("Only role cannot be defined");
                    }
                    if (role != null)
                    {
                        throw new InvalidFormatException("Only role cannot be defined");
                    }
                }
                if (email != null && role == null)
                {
                    throw new InvalidFormatException("Role is mandatory in case of email method deletion");
                }
                var DeleteUser = await _userRepository.DeleteUserAsync(email, userId, role);
                if (DeleteUser == null)
                {
                    throw new UnableToDeleteException("No user exists");
                }
                var DeleteSecurity = await _securityRepository.DeleteAsync(DeleteUser.UserId);
                if (DeleteUser != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new UnableToDeleteException();
            }
        }

        public async Task<User> UpdateUser(UserDTO userDTO)
        {
            try
            {
                var getUsers = await _userRepository.GetUserByEmailAsync(userDTO.UserEmail);
                var getUser = getUsers.FirstOrDefault();
                int userfound = 0;
                if (getUser != null)
                {
                    foreach (var user in getUsers)
                    {
                        if (user.Role.ToLower() == userDTO.Role.ToLower())
                        {
                            getUser = user;
                            userfound = 1;
                            break;
                        }

                    }
                    if (userfound == 0)
                    {
                        getUser = null;
                    }
                }
                if(getUser==null)
                {
                    throw new UnableToUpdateException("User does not exist");
                }
                var security = await _securityRepository.GetAsync(getUser.UserId);
                if (security == null)
                {
                    throw new UnableToUpdateException("User does not exist");
                }

                // Update the password
                var (passwordByte, passwordHashKey) = PasswordHashing(userDTO.Password);
                security.Password = passwordByte;
                security.PasswordHashKey = passwordHashKey;

                // Save the updated security
                var result = await _securityRepository.UpdateAsync(security);
                if (result == null)
                {
                    throw new UnableToUpdateException("User does not exist");
                }

                // Return the updated user
                return security.User;
            }
            catch
            {
                throw new UnableToUpdateException("User does not exist");
            }
        }
    }
}
