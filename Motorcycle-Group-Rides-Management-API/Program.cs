using Microsoft.EntityFrameworkCore;
using Motorcycle_Group_Rides_Management_API.Data;
using Motorcycle_Group_Rides_Management_API.Interfaces;
using Motorcycle_Group_Rides_Management_API.Repository;
using MySqlConnector;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Motorcycle_Group_Rides_Management_API.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IMotorcycleRepository, MotorcycleRepository>();
builder.Services.AddTransient<IGroupRepository, GroupRepository>();
builder.Services.AddTransient<IFeedbackRepository, FeedbackRepository>();
builder.Services.AddTransient<ICompatibilityRepository, CompatibilityRepository>();
//builder.Services.AddTransient<ICompatibilityService, CompatibilityService>();




builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<GroupRidesContext>(options=>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

