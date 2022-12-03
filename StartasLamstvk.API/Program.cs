using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StartasLamstvk.API.Entities;
using StartasLamstvk.API.Services;
using StartasLamstvk.Shared;
using StartasLamstvk.Shared.Models.Enum;
using System.Text;
using Microsoft.OpenApi.Models;

namespace StartasLamstvk.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            // Add services to the container.

            // For Entity Framework
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Default")));

            // For Identity
            builder.Services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Adding Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new ()
                {
                    ValidateIssuer = true, ValidateAudience = true, ValidAudience = configuration["JWT:ValidAudience"],
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                };
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition(
                    "Bearer",
                    new()
                    {
                        Description = @"JWT Authorization header using the Bearer scheme. <BR/><BR/> 
                      Enter your token in the text input below.
                      <BR/><BR/>Example: '12345abcdef'",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Scheme = "Bearer"
                    });
            });
            builder.Services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IPreferenceService, PreferenceService>();
            builder.Services.AddTransient<IEventService, EventService>();
            builder.Services.AddTransient<IRaceOfficialService, RaceOfficialService>();
            builder.Services.AddTransient<IWageService, WageService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            InitializeDatabase(app).Wait();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.UseExceptionHandler(Routes.Errors.Endpoint);

            app.Run();
        }

        private static async Task InitializeDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();

            var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();

            await context.Database.MigrateAsync();
            if (!context.Roles.Any())
            {
                await roleManager.CreateAsync(new (EnumRole.Admin.ToString())
                {
                    RoleTranslations = new ()
                    {
                        new () { LanguageCode = Languages.Lt, Text = "Administratorius" },
                        new () { LanguageCode = Languages.En, Text = "Administrator" }
                    }
                });
                await roleManager.CreateAsync(new (EnumRole.Director.ToString())
                {
                    RoleTranslations = new ()
                    {
                        new () { LanguageCode = Languages.Lt, Text = "Varžybų vadovas" },
                        new () { LanguageCode = Languages.En, Text = "Race Director" }
                    }
                });
                await roleManager.CreateAsync(new (EnumRole.Marshal.ToString())
                {
                    RoleTranslations = new ()
                    {
                        new () { LanguageCode = Languages.Lt, Text = "Teisėjas" },
                        new () { LanguageCode = Languages.En, Text = "Marshal" }
                    }
                });
            }

            if (!context.Users.Any())
            {
                var admin = new User
                {
                    Email = "admin@teisejai.lt",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "admin@teisejai.lt",
                    Name = "Startas",
                    Surname = "Admin",
                    EmailConfirmed = true,
                    RoleId = (int)EnumRole.Admin
                };
                await userManager.CreateAsync(admin, "Teisejai123.");
                await userManager.AddToRoleAsync(admin, EnumRole.Admin.ToString());
            }

            await context.SaveChangesAsync();
        }
    }
}