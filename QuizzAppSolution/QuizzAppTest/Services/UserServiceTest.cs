using Microsoft.EntityFrameworkCore;
using QuizzApp.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizzAppTest.Services
{
    public class UserServiceTest
    {
        QuizzAppContext context;
        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                .UseInMemoryDatabase("DummyDB");
            
            context = new QuizzAppContext(optionsBuilder.Options);
        }
        [Test]
        public void UserService_CandidateLogin_Test()
        {

        }
    }
}