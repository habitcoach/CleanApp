using Clean.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Clean.Infra.IoC;
using Clean.WebApi.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("RestaurantDBContext");
builder.Services.AddDbContext<RestaurantDBContext>(options =>
    options.UseSqlServer(connectionString));  //For product db

var connectionString02 = builder.Configuration.GetConnectionString("RestaurantAuthDBContext");
builder.Services.AddDbContext<RestaurantAuthDBContext>(options =>
    options.UseSqlServer(connectionString02)); // for auth db


builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<Program>()); // to register mediatR

builder.Services.RegisterServices();//Add project IoC project reference to the WebApi project if its not resolved
builder.Services.RegisterAutoMapper(); // for automapper

#region Setting up identity

builder.Services.AddIdentityCore<IdentityUser>()  // use for identity
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("Restaurant")
    .AddEntityFrameworkStores<RestaurantAuthDBContext>()
    .AddDefaultTokenProviders();
// pass configuration
builder.Services.Configure<IdentityOptions>(options => {

    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

});


#endregion


#region Setting up authentication
//Setting up authentication01
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 options.TokenValidationParameters = new TokenValidationParameters
 {
     ValidateIssuer = true,
     ValidateAudience = true,
     ValidateIssuerSigningKey = true,
     ValidateLifetime = true,
     ValidIssuer = builder.Configuration["Jwt:Issuer"],
     ValidAudience = builder.Configuration["Jwt:Audience"],
     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
 });
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // to auth
app.UseAuthorization();

app.MapControllers();

app.Run();
