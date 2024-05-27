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
    public class UserService : ILoginInterface
    {
        private readonly IUserRepository _userRepository;
        private readonly IRepository<int, Security> _securityRepository;
        private readonly ITokenService _tokenService;

        public UserService(IUserRepository userRepository, IRepository<int, Security> securityRepository,ITokenService tokenService)
        {
            _userRepository = userRepository;
            _securityRepository = securityRepository;
            _tokenService = tokenService;
        }

        public async Task<LoginReturnDTO> AdminLogin(string Email, string Password)
        {
            try
            {
                var getUser = await _userRepository.GetUserByEmailAsync(Email);
                if (getUser != null)
                {
                    var getSecurity = await _securityRepository.GetAsync(getUser.UserId);
                    if (getSecurity != null)
                    {
                        var IspasswordSame = ComparePassword(Password, getSecurity.Password, getSecurity.PasswordHashKey);
                        if (IspasswordSame)
                        {
                            if(getUser.Role.ToLower()!="admin")
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
                var getUser = await _userRepository.GetUserByEmailAsync(Email);
                if (getUser != null)
                {
                    var getSecurity = await _securityRepository.GetAsync(getUser.UserId);
                    if (getSecurity != null)
                    {
                        var IspasswordSame = ComparePassword(Password, getSecurity.Password, getSecurity.PasswordHashKey);
                        if (IspasswordSame)
                        {
                            if (getUser.Role.ToLower() != "Candidate")
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
                var getUser = await _userRepository.GetUserByEmailAsync(Email);
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
                if (result==null)
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
    }
}
