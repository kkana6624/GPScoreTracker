using GPScoreTracker.Domain.Repositories;
using GPScoreTracker.Server.WebApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register repositories
builder.Services.AddSingleton<ISongRepository, InMemorySongRepository>();
builder.Services.AddSingleton<IChartRepository, InMemoryChartRepository>();
builder.Services.AddSingleton<IScoreRecordRepository, InMemoryScoreRecordRepository>();

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
