using EventsApi.Context;
using EventsApi.Models;
using EventsApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "Data Source=./skillsAssessmentEvents.db;";

// Add services to the container.
builder.Services.AddDbContext<EventsContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IEventService, EventService>();
builder.Services.AddTransient<ITicketService, TicketService>();
builder.Services.AddTransient<IEventRepository, EventRepository>();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("corsPolicy", builder => builder
        .WithOrigins(
            "https://localhost:57215",
            "http://localhost:57215" 
        )
        .AllowCredentials()
        .AllowAnyHeader()
        .AllowAnyMethod()
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("corsPolicy"); 

app.UseAuthorization();

app.MapControllers();

app.Run();