using FluentValidation;
using LoginComponent.Helpers;
using LoginComponent.DataBase;
using LoginComponent.Validations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using LoginComponent.Repositories.Auth;
using LoginComponent.Service.Auth;
using LoginComponent.Interface.IRepositories.Auth;
using LoginComponent.Interface.IServices.Auth;
using LoginComponent.Interface.IServices.Admin;
using LoginComponent.Service.Admin;
using LoginComponent.Interface.IRepositories.Admin;
using LoginComponent.Repositories.Admin;
using LoginComponent.Interface.IServices.Customer;
using LoginComponent.Service.Customer;
using LoginComponent.Interface.IRepositories.Customer;
using LoginComponent.Repositories.Customer;
using LoginComponent.Models.Request.Auth;

namespace LoginComponent
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder
                .Configuration
                .AddUserSecrets<AplicationContext>()
                .Build();

            builder.Services.AddDbContext<AplicationContext>(options =>
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
            builder.Services.AddTransient<IAuthService, AuthService>();
            builder.Services.AddTransient<IAuthRepositories, AuthRepositories>();

            builder.Services.AddTransient<IAdminService, AdminService>();
            builder.Services.AddTransient<IAdminRepository, AdminRepository>();

            builder.Services.AddTransient<ICustomerService, CustomerService>();
            builder.Services.AddTransient<ICustomerRepsitory, CustomerRepository>();

            builder.Services.AddTransient<IValidator<SingUpRequest>, SingUpRequestValidation>();
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