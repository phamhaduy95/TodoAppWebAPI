using BussinessLayer.Service.Implement;
using BussinessLayer.Service.Interface;
using DataAccessLayer.EF;
using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using ToDoAPI.Authorization;
using ToDoAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Host.ConfigureAppConfiguration((configBuilder) =>
{
    var envName = builder.Environment.EnvironmentName;
    configBuilder.AddJsonFile($"appsettings.{envName}.json", true,true);
    configBuilder.AddEnvironmentVariables();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,
                        },
                        new List<string>()
                      }
                    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
  ));

// Add a DbContext to store your Database Keys
builder.Services.AddDbContext<AppDataProtectionKeyContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDataProtection()
     .PersistKeysToDbContext<AppDataProtectionKeyContext>();

builder.Services.AddIdentity<UserEntity, IdentityRole<Guid>>(builder =>
     {
         builder.User.RequireUniqueEmail = true;
         builder.Password.RequireUppercase = true;
         builder.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/ "; // include space
         builder.Password.RequiredLength = 8;
         builder.Lockout.AllowedForNewUsers = true;
         builder.Lockout.MaxFailedAccessAttempts = 5;
     }  
).AddEntityFrameworkStores<AppDbContext>();



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer((options) =>
{
    options.RequireHttpsMetadata = true;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ClockSkew = TimeSpan.Zero,
    };
});

builder.Services.AddSingleton<IAuthorizationHandler, AllowChangeDataHandler>();
builder.Services.AddScoped<IAuthorizationHandler, ValidTokenHandler>();
builder.Services.AddScoped<IAuthorizationHandler, CheckTaskOwnershipHandler>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SecurityTokenCheck", builder =>
    {
        builder.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        builder.RequireClaim("userId");
        builder.AddRequirements(new ValidTokenRequirement());
    });
    options.AddPolicy("UserOwnsTaskCheck", builder =>
     {
         builder.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
         builder.RequireClaim("userId");
         builder.AddRequirements(new CheckTaskOwnershipRequirement());
     }
    );
});

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ITokenGenerateService, TokenGenerateService>();
Console.WriteLine(builder.Configuration["ConnectionStrings:DefaultConnection"]);
Console.WriteLine(builder.Configuration["CrossOrigins"]);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "test-origin",
                    policy =>
                    {
                        policy.WithOrigins(builder.Configuration["CrossOrigins"]).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                    });
});

builder.WebHost.UseKestrel();
builder.WebHost.UseIIS();
builder.WebHost.UseIISIntegration();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseMiddleware<TokenDecryptMiddleware>();
app.UseCors("test-origin");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();