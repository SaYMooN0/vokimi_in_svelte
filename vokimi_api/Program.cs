using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Amazon.Runtime;
using Amazon.S3;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using vokimi_api.Src.db_related;

namespace vokimi_api
{
    public class Program
    {
        public static async Task Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    policy => {
                        policy.WithOrigins("https://localhost:5173")
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials();
                    });
            });

            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();

            app.UseCors("AllowSpecificOrigin");

            using (var scope = app.Services.CreateScope()) {
                var services = scope.ServiceProvider;
                try {
                    var appDbContext = services.GetRequiredService<AppDbContext>();
                    await DbInitializer.InitializeDbAsync(appDbContext);
                } catch (Exception ex) {
                    app.Logger.LogError(ex, "An error occurred while initializing the database.");
                    throw; // Re-throw the exception to stop the application start-up
                }
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            //app.MapPost("/logout", async (SignInManager<ApplicationUser> signInManager) => {

            //    await signInManager.SignOutAsync();
            //    return Results.Ok();

            //}).RequireAuthorization();


            //app.MapGet("/pingauth", (ClaimsPrincipal user) => {
            //    var email = user.FindFirstValue(ClaimTypes.Email); 
            //    return Results.Json(new { Email = email }); //return id and role instead 
            //}).RequireAuthorization();

            app.MapPost("/login", async (HttpContext context) => {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, "email@example.com")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties {
                };

                await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return Results.Ok("User logged in");
            });



            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<AppDbContext>(options =>
           options.UseNpgsql(configuration.GetConnectionString("VokimiDb")));

            // Yandex s3 configuration
            var creds = new BasicAWSCredentials(configuration["AWS:AccessKey"], configuration["AWS:SecretKey"]);
            var config = new AmazonS3Config { ServiceURL = "https://s3.yandexcloud.net" };

            services.AddSingleton<IAmazonS3>(sp => new AmazonS3Client(creds, config));

            string bucketName = configuration["AWS:BucketName"];
            //services.AddSingleton(sp => new VokimiStorageService(sp.GetRequiredService<IAmazonS3>(), bucketName));


            //services.Configure<SmtpSettings>(configuration.GetSection("Smtp"));
            //services.AddTransient<EmailService>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => {
                    options.LoginPath = "/login";
                    options.AccessDeniedPath = "/denied";
                });
            services.AddAuthorization();


            services.AddControllers();

            // configuring Swagger/OpenAPI 
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }


}
