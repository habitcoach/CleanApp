using Clean.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Clean.Infra.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("RestaurantDBContext");
builder.Services.AddDbContext<RestaurantDBContext>(options =>
    options.UseSqlServer(connectionString));  //For product db

builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<Program>()); // to register mediatR

builder.Services.RegisterServices();//Add project IoC project reference to the WebApi project if its not resolved

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
