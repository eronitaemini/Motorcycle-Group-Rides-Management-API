using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Motorcycle_Group_Rides_Management_API.Data;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Repository;
//<<<<<<< HEAD
using MySqlConnector;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Motorcycle_Group_Rides_Management_API.Services;
//=======
using Motorcycle_Group_Rides_Management_API.IncidentReportProfile;
using Motorcycle_Group_Rides_Management_API.Profiles;
using Motorcycle_Group_Rides_Management_API.Services;
using Umbraco.Core.Composing.CompositionExtensions;
using Umbraco.Core.Services;

//>>>>>>> 1a3bb258d5330293170811db8d51acc71641a842


var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//<<<<<<< HEAD
builder.Services.AddTransient<IMotorcycleRepository, MotorcycleRepository>();
builder.Services.AddTransient<IGroupRepository, GroupRepository>();
builder.Services.AddTransient<IFeedbackRepository, FeedbackRepository>();
builder.Services.AddTransient<ICompatibilityRepository, CompatibilityRepository>();
//builder.Services.AddTransient<ICompatibilityService, CompatibilityService>();




builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


//=======
//  DbContext to use MySQL
//>>>>>>> 1a3bb258d5330293170811db8d51acc71641a842
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<GroupRidesContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 32)))); // Use your MySQL version

//  Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<GroupRidesContext>()
    .AddDefaultTokenProviders();

//  Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});



builder.Services.AddScoped<IIncidentReportRepository, IncidentReportRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAutoMapper(typeof(IncidentReportProfile));
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IGroupRideRepository, GroupRideRepository>();
builder.Services.AddAutoMapper(typeof(GroupRideProfile));

builder.Services.AddScoped<IIncidentReportService, IncidentReportService>();
builder.Services.AddScoped<IGroupRideService, GroupRideService>();
builder.Services.AddScoped<Motorcycle_Group_Rides_Management_API.Services.IUserService, UserService>();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
