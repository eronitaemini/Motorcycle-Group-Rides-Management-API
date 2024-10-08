﻿using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Motorcycle_Group_Rides_Management_API.Data;

using Motorcycle_Group_Rides_Management_API.External;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Repository;
using Motorcycle_Group_Rides_Management_API.Services;
//<<<<<<< HEAD
using MySqlConnector;
using Motorcycle_Group_Rides_Management_API.IncidentReportProfile;
using Motorcycle_Group_Rides_Management_API.Profiles;
using Motorcycle_Group_Rides_Management_API.Services;
using Umbraco.Core.Composing.CompositionExtensions;
using Umbraco.Core.Services;

using Umbraco.Core.Composing.CompositionExtensions;
using Umbraco.Core.Services;
using System.Text.Json.Serialization;




var builder = WebApplication.CreateBuilder(args);



builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//<<<<<<< HEAD
builder.Services.AddTransient<IMotorcycleRepository, MotorcycleRepository>();
builder.Services.AddTransient<IGroupRepository, GroupRepository>();
builder.Services.AddTransient<IRouteRepository, RoutesRepository>();
builder.Services.AddTransient<IFeedbackRepository, FeedbackRepository>();
builder.Services.AddTransient<ICompatibilityRepository, CompatibilityRepository>();
//builder.Services.AddTransient<ICompatibilityService, CompatibilityService>();

builder.Services.AddScoped<ICompatibilityService, CompatibilityService>();
builder.Services.AddScoped<IFeedbackService, FeedbackService>();
builder.Services.AddScoped<IStatisticsRepository, StatisticsRepository>();
builder.Services.AddScoped<IStatisticsService, StatisticsService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IRouteService, RouteService>();
builder.Services.AddScoped<IMotorcycleService, MotorcycleService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IRouteInfo, RouteInfoService>();

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<GroupRidesContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 39)))); // Use your MySQL version

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

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Configure JSON options to serialize enums as strings
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
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
