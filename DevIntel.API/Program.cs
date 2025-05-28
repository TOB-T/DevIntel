using DevIntel.API.Swagger;
using DevIntel.Application.DTO;
using DevIntel.Application.Validators;
using DevIntel.Infrastructure.Configurations;
using DevIntel.Infrastructure.Extensions;
using DevIntel.Infrastructure.Persistence.Seeders;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // 1) Still map IFormFile ? file input
    c.MapType<IFormFile>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "binary"
    });

    // 2) Add your custom operation filter
    c.OperationFilter<FileUploadOperationFilter>();

    // (any other config, e.g. SwaggerDoc)
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DevIntel API", Version = "v1" });
});


builder.Services.AddInfrastructure(builder.Configuration);

//builder.Services.AddTransient<IValidator<UserRegisterDto>, RegisterDtoValidator>();

builder.Services.AddValidatorsFromAssemblyContaining<RegisterDtoValidator>();

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
        };
    });



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await SeedData.SeedAdminAsync(app.Services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles(); // Serve files from wwwroot (e.g. /uploads/…)

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
