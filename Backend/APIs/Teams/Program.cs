using Microsoft.EntityFrameworkCore;
using Teams.Context;
using Teams.Data;
using Teams.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//var teamsDbConnectionString = "Host=pslfantasy;Port=5432;Database=TeamDb;User Id=Teboho;Password=Teboho#@123";

//register db
builder.Services.AddDbContext<TeamContext>(options =>
                options.UseInMemoryDatabase("TeamDb"));

//register services
builder.Services.AddScoped<ITeamRepository, TeamRepository>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
app.MigrateDatabase<TeamContext>();
