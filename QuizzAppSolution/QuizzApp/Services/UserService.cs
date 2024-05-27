using QuizzApp.Interfaces;
using QuizzApp.Models;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;

using QuizzApp.Interfaces;
using QuizzApp.Models;
using QuizzApp.Models.DTO;
using QuizzApp.Repositories;

namespace QuizzApp.Services
{
    public class UserService : ILoginInterface
    {
        private IRepository<int, User> _userRepository;
        private IRepository<int, Security> _securityRepository;
        private ITokenService _tokenService;

        public UserService(IRepository<int, User> userRepository, IRepository<int, Security> securityRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _securityRepository = securityRepository;
            _tokenService = tokenService;
        }

        public async Task<User> AddUser(UserDTO userDto)
        {
            try
            {
                var AddingUser = new User()
                {
                    UserEmail = userDto.UserEmail,
                    UserName = userDto.UserName,
                    Role = userDto.Role
                };
                var result = await _userRepository.Add(AddingUser);

                //var UserID = await _userRepository.Get(result.UserId);
                var (PasswordByte, PasswordHashKey_) = PasswordHashing(userDto.Password);
                var AddingSecurity = new Security()
                {
                    UserId = AddingUser.UserId,
                    //HMACSHA512
                    Password = PasswordByte,
                    PasswordHashKey = PasswordHashKey_,
                    User = AddingUser

                };

                await _securityRepository.Add(AddingSecurity);
            }
            catch (Exception ex)
            {
                throw new UnableToAddException("Unable to add");
            }
            return userDto;

        }

        public Task<bool> DeleteUser(int UserId)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUser(int UserId)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
        public byte[] ConvertToByte(string Password)
        {
            return Encoding.UTF8.GetBytes(Password);
        }
        public (byte[] PasswordByte, byte[] PasswordHashKey_) PasswordHashing(string Password)
        {
            HMACSHA512 hMACSHA512 = new HMACSHA512();
            var PasswordBytesHash = hMACSHA512.ComputeHash(ConvertToByte(Password));
            var key = hMACSHA512.Key;
            return (PasswordBytesHash, key);
        }


        public async Task<LoginReturnDTO> CandidateLogin(string Email, string Password)
        {
            try
            {
                var getUser = await _userRepository.GetUserByMail(Email);
                if (getUser != null)
                {
                    var getSecurity = await _securityRepository.Get(getUser.UserId);
                    if (getSecurity != null)
                    {
                        var IspasswordSame = ComparePassword(Password, getSecurity.Password, getSecurity.PasswordHashKey);
                        if (IspasswordSame)
                        {
                            if (!getUser.AcitiveStatus)
                            {
                                throw new NotActiveException("Your account not activated");
                            }
                            else
                            {

                                LoginReturnDTO loginReturnDTO = LoginReturn(getUser);
                                return loginReturnDTO;
                            }

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

        public Task<User> OrganizerLogin(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<User> AdminLogin(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
