using Clean.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Clean.Infra.IoC;
using Clean.WebApi.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Logging 

var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/RestaurantLog.txt",rollingInterval:RollingInterval.Minute)
    .MinimumLevel.Information()
   // .MinimumLevel.Warning()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger); // log provider

#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
#region Swagger Auth Setting
builder.Services.AddSwaggerGen(options => {


   // options.SwaggerDoc("v1", new OpenApiInfo { Title = "RestaurantAPI", Version = "v1" });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {

        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {

            new OpenApiSecurityScheme
            {
            Reference = new OpenApiReference
            {
            Type = ReferenceType.SecurityScheme,
            Id= JwtBearerDefaults.AuthenticationScheme
            },
            Scheme = "Oauth2",
            Name = JwtBearerDefaults.AuthenticationScheme,
            In = ParameterLocation.Header
            },
            new List<string>()
        }
    });

});
//builder.Services.AddSwaggerGen(); //without auth
#endregion


var connectionString = builder.Configuration.GetConnectionString("RestaurantDBContext");
builder.Services.AddDbContext<RestaurantDBContext>(options =>
    options.UseSqlServer(connectionString));  //For product db

var connectionString02 = builder.Configuration.GetConnectionString("RestaurantAuthDBContext");
builder.Services.AddDbContext<RestaurantAuthDBContext>(options =>
    options.UseSqlServer(connectionString02)); // for auth db


builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<Program>()); // to register mediatR

builder.Services.RegisterServices();//Add project IoC project reference to the WebApi project if its not resolved
builder.Services.RegisterAutoMapper(); // for automapper

#region Versioning

builder.Services.AddApiVersioning(options => {

    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    options.ReportApiVersions = true;

});


#endregion

#region Versioning_swagger

builder.Services.AddVersionedApiExplorer(options =>
{

    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;

});



#endregion

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
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();  //version_swagger
var app = builder.Build();

// Configure the HTTP request pipeline.
var versionDescriptionProvider =
   app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => {

        foreach (var description in versionDescriptionProvider.ApiVersionDescriptions)
        {

            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());

        }


    });
}

app.UseHttpsRedirection();

app.UseAuthentication(); // to auth
app.UseAuthorization();

app.MapControllers();

app.Run();
