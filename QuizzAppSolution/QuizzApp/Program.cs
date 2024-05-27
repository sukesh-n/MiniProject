using Microsoft.EntityFrameworkCore;
using QuizzApp.Context;
using QuizzApp.Interfaces;
using QuizzApp.Models;
using QuizzApp.Repositories;
using QuizzApp.Services;

namespace QuizzApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region context
            builder.Services.AddDbContext<QuizzAppContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("defaultString")));
            #endregion
            #region Method
            builder.Services.AddScoped<IRepository<int, User>, UserRepository>();
            builder.Services.AddScoped<IRepository<int, Question>, QuestionRepository>();
            builder.Services.AddScoped<IRepository<int, Category>, CategoryRepository>();
            builder.Services.AddScoped<IRepository<int, Solution>, SolutionRepository>();
            builder.Services.AddScoped<IRepository<int, Test>, TestRepository>();
            builder.Services.AddScoped<IRepository<int, Result>, ResultRepository>();
            builder.Services.AddScoped<IRepository<int, Option>, OptionsRepository>();
            builder.Services.AddScoped<IRepository<int, Security>, SecurityRepository>();
            #endregion

            #region Service
            builder.Services.AddScoped<IAdminInterface, AdminService>();
            builder.Services.AddScoped<ICandidateInterface, CandidateService>();
            builder.Services.AddScoped<ILoginInterface, UserService>();
            #endregion



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
