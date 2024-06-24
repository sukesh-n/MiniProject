using Microsoft.Extensions.Configuration;
using Moq;
using QuizzApp.Interfaces;
using QuizzApp.Models;
using QuizzApp.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizzAppTest.Services
{
    internal class TokenServiceTest
    {
        [SetUp]
        public void Setup()
        {

        }
        [Test]
        public void TokenServiceTest1()
        {
            // Assign
            Mock<IConfigurationSection> mockConfigJWTSection = new Mock<IConfigurationSection>();
            mockConfigJWTSection.SetupGet(m => m.Value).Returns("JWT Key for token authentication_");

            Mock<IConfigurationSection> mockTokenService = new Mock<IConfigurationSection>();
            mockTokenService.Setup(m => m.GetSection("JWT")).Returns(mockConfigJWTSection.Object);

            Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(m => m.GetSection("TokenKey")).Returns(mockTokenService.Object);

            ITokenService tokenService = new TokenService(mockConfig.Object);
            // Act
            var result = tokenService.GenerateToken(new User { UserId = 1, UserName = "Name", UserEmail = "name@quizzo.qz", Role = "admin" });
            // Assert
            Assert.IsNotNull(result);
        }

    }
}
