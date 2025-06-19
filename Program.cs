using System.Text;
using BloodDonationSupportSystem.Data;
using BloodDonationSupportSystem.Services;
using BloodDonationSupportSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BloodDonationSupportSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var config = builder.Configuration;
            var services = builder.Services;

            builder.WebHost.UseUrls("http://0.0.0.0:80");

            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(
                    config.GetConnectionString("DefaultConnection"),
                    ServerVersion.AutoDetect(config.GetConnectionString("DefaultConnection"))
                ));

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBloodService, BloodService>();

            services.AddSingleton<JwtService>();


            var jwtKey = config["Jwt:Key"];
            Console.WriteLine($"JWT Key: {jwtKey}");
            var keyBytes = Encoding.ASCII.GetBytes(jwtKey); 
            Console.WriteLine($"Key Bytes Length: {keyBytes.Length}");
            if (keyBytes.Length < 32)
                throw new Exception("JWT key must be at least 256 bits (32 bytes)");

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = config["Jwt:Issuer"],
                        ValidAudience = config["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                        RoleClaimType = "Role"
                    };
                });





            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
