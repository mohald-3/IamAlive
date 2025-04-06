using IamAlive.Data;
using IamAlive.Services.Implementations;
using IamAlive.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using IamAlive.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using IamAlive.Helpers;
using FluentValidation;
using FluentValidation.AspNetCore;



namespace IamAlive
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //Token service

            builder.Services.ConfigureJwt(builder.Configuration);
            builder.Services.AddScoped<ITokenService, TokenService>();

            //DB service
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //Functionality services
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ICheckInService, CheckInService>();
            builder.Services.AddScoped<IFriendshipService, FriendshipService>();

            //Validator service

            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();

            //Automapper
            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.ConfigureSwagger(builder.Configuration);

            var app = builder.Build();

            //Fake data seeder
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                FakeDataSeeder.SeedAsync(dbContext).GetAwaiter().GetResult();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();



        }
    }
}
