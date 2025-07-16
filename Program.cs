using System.Text;
using BloodDonationSupportSystem.Data;
using BloodDonationSupportSystem.Repositories.Implementations;
using BloodDonationSupportSystem.Repositories.Interface;
using BloodDonationSupportSystem.Services;
using BloodDonationSupportSystem.Services.Implementations;
using BloodDonationSupportSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using BloodDonationSupportSystem.Helpers;

namespace BloodDonationSupportSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            var config = builder.Configuration;
            var services = builder.Services;

            builder.WebHost.UseUrls("http://0.0.0.0:80");

            // ========= Database =========
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(
                    config.GetConnectionString("DefaultConnection"),
                    ServerVersion.AutoDetect(config.GetConnectionString("DefaultConnection"))
                ));

            // ========= Controllers + Swagger =========
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
                options.JsonSerializerOptions.Converters.Add(new NullableDateOnlyJsonConverter());
                options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
                options.JsonSerializerOptions.Converters.Add(new NullableTimeOnlyJsonConverter());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new() { Title = "Blood Donation API", Version = "v1" });

                // 💥 Add JWT support to Swagger UI
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your token.\nExample: Bearer abc123..."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        Array.Empty<string>()
                    }
                });
            });

            // ========= Dependency Injection =========
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
            builder.Services.AddScoped<IBlogRepository, BlogRepository>();
            builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            builder.Services.AddScoped<IEventRepository, EventRepository>();
            builder.Services.AddScoped<IDonationRepository, DonationRepository>();
            builder.Services.AddScoped<IBloodRequestRepository, BloodRequestRepository>();
            builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
            builder.Services.AddScoped<IFeedbackService, FeedbackService>();
            builder.Services.AddScoped<IEventService, EventService>();
            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IBloodService, BloodService>();
            builder.Services.AddScoped<IBlogService, BlogService>();
            builder.Services.AddScoped<IDashboardService, DashboardService>();
            builder.Services.AddSingleton<JwtService>();

            // ========= CORS =========
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            // ========= JWT Auth =========
            var jwtKey = config["Jwt:Key"];
            var keyBytes = Encoding.ASCII.GetBytes(jwtKey!);
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

            // ========= App build + middleware =========
            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}
