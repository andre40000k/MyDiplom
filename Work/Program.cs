using LoginComponent.Helpers;
using LoginComponent.Interface.IRepositories;
using LoginComponent.Interface.IServices;
using LoginComponent.LoginDataBase;
using LoginComponent.Repositories;
using LoginComponent.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LoginComponent
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder
                .Configuration
                .AddUserSecrets<LoginContext>()
                .Build();

            builder.Services.AddDbContext<LoginContext>(options =>
            {
                var conectionString = builder
                .Configuration
                .GetConnectionString("LoginASP");

                options.UseSqlServer(conectionString);
            });
            // Add services to the container.

            builder.Services.AddControllers();


            builder.Services.AddCors(option =>
            {
                option.AddDefaultPolicy(build =>
                {
                    build
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = TokenHelper._issuer,
                    ValidAudience = TokenHelper._audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(TokenHelper._secret)),
                    ClockSkew = TimeSpan.Zero
                };

            });

            builder.Services.AddAuthorization();

            builder.Services.AddTransient<ITokenService, TokenService>();
            builder.Services.AddTransient<ITokenRepositories, TokenRepositories>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IUserRepositories, UserRepositories>();
            //builder.Services.AddTransient<ITaskService, TaskService>();

            var app = builder.Build();

            app.UseCors(build =>
            {
                build
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}