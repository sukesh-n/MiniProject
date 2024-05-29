using Microsoft.EntityFrameworkCore;
using QuizzApp.Models;

namespace QuizzApp.Context
{
    public class QuizzAppContext : DbContext
    {
        public QuizzAppContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> users { get; set; }
        public DbSet<Question> questions { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Solution> solutions { get; set; }
        public DbSet<Security> security { get; set; }
        public DbSet<Option> options { get; set; }
        public DbSet<Test> tests { get; set; }
        public DbSet<Result> results { get; set; }
        public DbSet<AssignedQuestions> assignedQuestions { get; set; }
        public DbSet<AssignedTest> assignedTests { get; set; }
        public DbSet<AssignedTestEmail> assignedTestEmails { get; set; }


    }
}
