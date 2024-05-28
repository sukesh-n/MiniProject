using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QuizzApp.Context;
using QuizzApp.Interfaces;
using QuizzApp.Models;
using QuizzApp.Repositories;
using QuizzApp.Services;
using QuizzApp.Token;
using System.Security.Claims;
using System.Text;

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
            builder.Services.AddSwaggerGen(option =>
            {
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
            });
            //Debug.WriteLine(builder.Configuration["TokenKey:JWT"]);
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey:JWT"])),
                        RoleClaimType = ClaimTypes.Role
                    };

                });


            #region context
            builder.Services.AddDbContext<QuizzAppContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("defaultString")));
            #endregion
            #region Method
            builder.Services.AddScoped<IUserRepository, UserRepository>();
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
            builder.Services.AddScoped<ITokenService, TokenService>();
            #endregion



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
