using DataAccess.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Service.AuthToken;
using Service.Implementations.BookingRepository;
using Service.Implementations.HotelRepository;
using Service.Implementations.RoomRepository;
using Service.Implementations.UserRepository;
using Service.Interfaces.BookingInterfaces;
using Service.Interfaces.HotelInterfaces;
using Service.Interfaces.RoomInterFaces;
using Service.Interfaces.TokenInterfaces;
using Service.Interfaces.UserInterfaces;
using System.Text;
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
builder.Services.AddScoped<IToken, TokenLogic>();

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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidateIssuer = true,
            ValidateAudience = true,
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddHttpClient();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
