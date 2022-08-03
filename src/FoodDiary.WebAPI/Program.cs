using Microsoft.EntityFrameworkCore;
using FoodDiary.WebAPI.DatabaseContext;
using FoodDiary.WebAPI.DAL.Interfaces;
using FoodDiary.WebAPI.DAL.EFCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<FoodDiaryContext>(options =>
    options.UseInMemoryDatabase("FoodDiary"));
builder.Services.AddTransient<IMealDAO, MealDAO>();
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
