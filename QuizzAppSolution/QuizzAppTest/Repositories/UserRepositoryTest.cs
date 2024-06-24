using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using QuizzApp.Context;
using QuizzApp.Interfaces;
using QuizzApp.Models;
using QuizzApp.Models.DTO.UserServicesDTO;
using QuizzApp.Repositories;
using QuizzApp.Services;
using QuizzApp.Token;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuizzAppTest.Services
{
    public class UserRepositoryTest
    {
        QuizzAppContext context;
        IConfiguration configuration;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<QuizzAppContext> options = new DbContextOptionsBuilder<QuizzAppContext>()
                .UseInMemoryDatabase(databaseName: "DummyDB")
                .Options;

            context = new QuizzAppContext(options);

            var mockConfiguration = new Mock<IConfiguration>();
            var mockConfSection = new Mock<IConfigurationSection>();
            mockConfSection.SetupGet(m => m.Value).Returns("JWT Key for token authentication_");
            mockConfiguration.Setup(a => a.GetSection("TokenKey").GetSection("JWT")).Returns(mockConfSection.Object);
            configuration = mockConfiguration.Object;

        }

        [Test]
        public async Task UserService_AdminLogin_Test()
        {
            //in-memory database
            var userData = await context.users.AddAsync(new User { UserName = "Sukesh", UserEmail = "suk@suk.qz", Role = "admin" });
            await context.SaveChangesAsync();

            var hashedPassword = PasswordHashing("suk@123");
            await context.security.AddAsync(new Security { UserId = userData.Entity.UserId, Password = hashedPassword.PasswordByte, PasswordHashKey = hashedPassword.PasswordHashKey_ });
            await context.SaveChangesAsync();

            // Arrange
            IUserRepository userRepository = new UserRepository(context);

            ITokenService tokenService = new TokenService(configuration);
            IRepository<int, Security> securityRepository = new SecurityRepository(context);
            ILoginService loginService = new UserService(userRepository, securityRepository, tokenService);

            // Act
            var result = await loginService.AdminLogin("suk@suk.qz", "suk@123");

      
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("suk@suk.qz",result.UserEmail);
            Assert.AreEqual("admin", result.Role.ToLower());
        }

        [Test]
        public async Task UserService_CandidateLogin_Test()
        {
            // in-memory database
            var userData = await context.users.AddAsync(new User { UserName = "Sukes", UserEmail = "suk@suk.q", Role = "candidate" });
            await context.SaveChangesAsync();

            var hashedPassword = PasswordHashing("suk@12");
            await context.security.AddAsync(new Security { UserId = userData.Entity.UserId, Password = hashedPassword.PasswordByte, PasswordHashKey = hashedPassword.PasswordHashKey_ });
            await context.SaveChangesAsync();

            // Arrange
            IUserRepository userRepository = new UserRepository(context);

            ITokenService tokenService = new TokenService(configuration);
            IRepository<int, Security> securityRepository = new SecurityRepository(context);
            ILoginService loginService = new UserService(userRepository, securityRepository, tokenService);

            // Act
            var result = await loginService.CandidateLogin("suk@suk.q", "suk@12");


            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual("admin", result.Role.ToLower());
            Assert.AreEqual("suk@suk.q", result.UserEmail);

        }


        [Test]
        public async Task UserService_OrganizerLogin_Test()
        {
            // in-memory database
            var userData = await context.users.AddAsync(new User { UserName = "Organizer", UserEmail = "organizer@org.qz", Role = "organizer" });
            await context.SaveChangesAsync();

            var hashedPassword = PasswordHashing("suk@12");
            await context.security.AddAsync(new Security { UserId = userData.Entity.UserId, Password = hashedPassword.PasswordByte, PasswordHashKey = hashedPassword.PasswordHashKey_ });
            await context.SaveChangesAsync();

            // Arrange
            IUserRepository userRepository = new UserRepository(context);

            ITokenService tokenService = new TokenService(configuration);
            IRepository<int, Security> securityRepository = new SecurityRepository(context);
            ILoginService loginService = new UserService(userRepository, securityRepository, tokenService);

            // Act
            var result = await loginService.OrganizerLogin("organizer@org.qz", "suk@12");


            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("organizer", result.Role.ToLower());
            Assert.AreEqual("organizer@org.qz", result.UserEmail);

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
    }
}
