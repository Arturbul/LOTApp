using FluentValidation;
using LOTApp.Business;
using LOTApp.Business.Mappers;
using LOTApp.Core.Authentication;
using LOTApp.Core.ViewModels;
using LOTApp.DataAccess.Data;
using LOTApp.WebAPI.Mappers;
using LOTApp.WebAPI.RequestModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //Logger
        using var loggerFactory = LoggerFactory.Create(b => b.SetMinimumLevel(LogLevel.Trace).AddConsole());

        //JWT
        builder.Services.AddIdentity<AppUser, IdentityRole>()
                   .AddEntityFrameworkStores<LOTContext>()
                   .AddDefaultTokenProviders();

        var secret = builder.Configuration["JWT:Secret"] ?? throw new InvalidOperationException("Secret not configured");
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                ValidAudience = builder.Configuration["JWT:ValidAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                ClockSkew = TimeSpan.FromSeconds(5)
            };
            options.Events = new JwtBearerEvents
            {
                OnChallenge = ctx => LogAttempt(ctx.Request.Headers, "OnChallenge"),
                OnTokenValidated = ctx => LogAttempt(ctx.Request.Headers, "OnTokenValidated")
            };
        });
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy(IdentityData.AdminUserPolicyName, p =>
                p.RequireClaim(IdentityData.AdminUserClaimName, "true"));
        });


        //Controllers
        builder.Services.AddControllersWithViews();

        //DbContext
        builder.Services.AddDbContextPool<LOTContext>(options =>
                        options.UseSqlServer(builder.Configuration.GetConnectionString("MsSQLLocalDB")));

        //Swagger
        builder.Services.AddSwaggerGen(options =>
        {
            List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
            xmlFiles.ForEach(xmlFile => options.IncludeXmlComments(xmlFile));

            //Swagger JWT
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Please enter only '[jwt]'",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Name = "Authorization",
                Scheme = JwtBearerDefaults.AuthenticationScheme
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id=JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        Array.Empty<string>()
                    }
                });
        });

        //DI
        Dependencies.Register(builder.Services);


        //Automapper
        builder.Services.AddAutoMapper(
            typeof(FlightWebAPIProfile),
            typeof(FlightServiceProfile)
            );

        //FluentValidation
        builder.Services.AddScoped<IValidator<FlightViewModel>, FlightViewModelValidator>();
        builder.Services.AddScoped<IValidator<CreateFlightRequest>, CreateFlightRequestValidator>();
        builder.Services.AddScoped<IValidator<UpdateFlightRequest>, UpdateFlightRequestValidator>();

        //Build
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
        }
        else
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
        }
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();


        //Methods
        Task LogAttempt(IHeaderDictionary headers, string eventType)
        {
            var logger = loggerFactory.CreateLogger<Program>();

            var authorizationHeader = headers["Authorization"].FirstOrDefault();

            if (authorizationHeader is null)
                logger.LogInformation($"{eventType}. JWT not present");
            else
            {
                string jwtString = authorizationHeader.Substring("Bearer ".Length);

                var jwt = new JwtSecurityToken(jwtString);

                logger.LogInformation($"{eventType}. Expiration: {jwt.ValidTo.ToLongTimeString()}. System time: {DateTime.UtcNow.ToLongTimeString()}");
            }
            return Task.CompletedTask;
        }
    }
}