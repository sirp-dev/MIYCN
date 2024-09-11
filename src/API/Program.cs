 
using Domain.Models; 
using Infrastructure.Context; 
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("AppConnection") ?? throw new InvalidOperationException("Connection string 'MovieConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));
builder.Services.AddControllers();
// Configure Identity
builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
 
// Register command and query handlers
//builder.Services.AddScoped<IAddMovieHandler, AddMovieHandler>();
 builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Register Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Omdbapi.com Service API", Version = "v1" });
});
builder.Services.AddCors(options =>
{
options.AddDefaultPolicy(
   policy =>
            {
    policy.AllowAnyOrigin()
          .AllowAnyHeader()
            .AllowAnyMethod();
});
    });


 


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable Swagger UI in development environment
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Omdbapi.com Service API v1"));
}



app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
