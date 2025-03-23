using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Service.Implementations.BookingRepository;
using Service.Implementations.HotelRepository;
using Service.Implementations.RoomRepository;
using Service.Implementations.UserRepository;
using Service.Interfaces.BookingInterfaces;
using Service.Interfaces.HotelInterfaces;
using Service.Interfaces.RoomInterFaces;
using Service.Interfaces.UserInterfaces;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
DotNetEnv.Env.Load();
var connection = Environment.GetEnvironmentVariable("connection");
var key = Environment.GetEnvironmentVariable("Key");
if (string.IsNullOrEmpty(key))
{
    throw new Exception("JWT secret key is not set in the environment variables.");
}
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connection);
});

builder.Services.AddScoped<IHotel, HotelRepo>();
builder.Services.AddScoped<IRoom, RoomRepo>();
builder.Services.AddScoped<IBooking, BookingRepo>();
builder.Services.AddScoped<IUser, UserRepo>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });
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
