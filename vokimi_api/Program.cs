using Microsoft.EntityFrameworkCore;
using Amazon.Runtime;
using Amazon.S3;
using Microsoft.AspNetCore.Authentication.Cookies;
using vokimi_api.Src.db_related;
using vokimi_api.Services;
using vokimi_api.Endpoints;
using Amazon.S3.Model;
using vokimi_api.Src.enums;
using vokimi_api.Src.dtos.responses;
using vokimi_api.Endpoints.tests_operations;
using vokimi_api.Endpoints.tests_operations.test_creation;

namespace vokimi_api
{
    public class Program
    {
        public static async Task Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options => {
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

            app.MapControllers();

            app.UseAntiforgery();
            MapEndpoints(app);

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
        public static void MapEndpoints(WebApplication app) {
            app.MapGet("/pingauth", AuthEndpoints.PingAuth);
            app.MapGet("/getUsernameWithProfilePicture", AuthEndpoints.GetUsernameWithProfilePicture);
            app.MapPost("/signup", AuthEndpoints.Signup);
            app.MapPost("/confirmRegistration", AuthEndpoints.ConfirmRegistration);
            app.MapPost("/login", AuthEndpoints.Login);
            app.MapPost("/logout", AuthEndpoints.Logout);

            app.MapGet("/usersDraftTestsFirstPackage", DraftTestEndpoints.GetUsersDraftTestsVms);
            app.MapPost("/getDraftTestOverviewInfo", DraftTestEndpoints.GetDraftTestOverviewInfo);

            app.MapGet("/usersPublishedTestFirstPackage", async (_) => new List<UsersTestsVm>() { });
            app.MapPost("/createNewTest/{template}",
                async (HttpContext httpContext, IDbContextFactory<AppDbContext> dbFactory, string template) => {
                    TestTemplate? parsedTemplate = TestTemplateExtensions.FromId(template);
                    if (parsedTemplate is null) {
                        return Results.BadRequest("Invalid template specified.");
                    }
                    return await TestCreationSharedEndpoints.CreateNewTest(httpContext, dbFactory, parsedTemplate.Value);

                });

            app.MapGet("/testCreation/getDraftTestMainInfoData/{testId}",
                TestCreationSharedEndpoints.GetDraftTestMainInfoData);
            app.MapPost("/testCreation/updateDraftTestMainInfoData",
                TestCreationSharedEndpoints.UpdateDraftTestMainInfo);
            app.MapPost("/testCreation/setDraftTestCoverToDefault/{testId}",
                TestCreationSharedEndpoints.SetDraftTestCoverToDefault);

            app.MapGet("/testCreation/general/getGeneralDraftTestQuestionsData/{testId}",
                GeneralTestCreationEndpoints.GetGeneralDraftTestQuestionsData);
            app.MapPost("/testCreation/general/createGeneralTestQuestion",
                GeneralTestCreationEndpoints.CreateGeneralTestQuestion);
            app.MapGet("/testCreation/general/getDraftGeneralTestQuestionDataToEdit/{questionId}",
                GeneralTestCreationEndpoints.GetDraftGeneralTestQuestionDataToEdit);
            app.MapPost("/testCreation/general/updateDraftGeneralTestQuestionData",
                GeneralTestCreationEndpoints.UpdateDraftGeneralTestQuestionData);
            app.MapGet("/testCreation/general/getResultsIdNameDictionary/{testId}",
                GeneralTestCreationEndpoints.GetResultsIdNameDictionary);
            app.MapPost("/testCreation/general/createNewResult", GeneralTestCreationEndpoints.CreateNewResultForTest);
            
            app.MapGet("/testCreation/general/getTestResultsDataToEdit",
                GeneralTestCreationEndpoints.GetResultsDataToEdit);
            app.MapDelete("/testCreation/general/deleteGeneralDraftTestQuestion/{questionId}",
                GeneralTestCreationEndpoints.DeleteGeneralDraftTestQuestion);

            app.MapGet("/vokimiimgs/{*fileKey}", ImgOperationsEndpoints.GetImgFromStorage);
            app.MapPost("/testCreation/updateDraftTestQuestionCover/{testId}",
                ImgOperationsEndpoints.UpdateDraftTestQuestionCover).DisableAntiforgery();
            app.MapPost("/saveimg/saveDraftGeneralTestAnswerImage/{questionId}",
                ImgOperationsEndpoints.SaveDraftGeneralTestAnswerImage).DisableAntiforgery();
        }
        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration) {


            ConfigureDbContextFactory(services, configuration);
            ConfigureS3(services, configuration);
            ConfigureEmailService(services, configuration);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => {
                    options.LoginPath = "/login";
                    options.AccessDeniedPath = "/denied";
                });
            services.AddAuthorization();


            services.AddControllers();
            services.AddAntiforgery();

            // configuring Swagger/OpenAPI 
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
        private static void ConfigureDbContextFactory(IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContextFactory<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("VokimiDb")));
        private static void ConfigureS3(IServiceCollection services, IConfiguration configuration) {

            string
                accessKey = configuration["AWS:AccessKey"] ?? throw new Exception("AWS:AccessKey is not set"),
                secretKey = configuration["AWS:SecretKey"] ?? throw new Exception("AWS:SecretKey is not set"),
                bucketName = configuration["AWS:BucketName"] ?? throw new Exception("AWS:BucketName is not set");

            var creds = new BasicAWSCredentials(accessKey, secretKey);
            var config = new AmazonS3Config { ServiceURL = "https://s3.yandexcloud.net" };

            services.AddSingleton<IAmazonS3>(sp => new AmazonS3Client(creds, config));
            services.AddSingleton(sp => new VokimiStorageService(sp.GetRequiredService<IAmazonS3>(), bucketName));
        }
        private static void ConfigureEmailService(IServiceCollection services, IConfiguration configuration) {
            string smtpHost = configuration["Smtp:Host"] ?? throw new Exception("Smtp:Host is not set");
            int smtpPort = int.Parse(configuration["Smtp:Port"] ?? throw new Exception("Smtp:Port is not set"));
            string smtpUsername = configuration["Smtp:Username"] ?? throw new Exception("Smtp:Username is not set");
            string smtpPassword = configuration["Smtp:Password"] ?? throw new Exception("Smtp:Password is not set");
            services.AddTransient((_) => new EmailService(smtpHost, smtpPort, smtpUsername, smtpPassword));
        }
    }
}

