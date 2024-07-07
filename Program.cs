using ControlboxLibreriaAPI.Authentication;
using ControlboxLibreriaAPI.Modelo;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Database") ?? "Data Source=Database.db";
// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });
builder.Services.AddSingleton(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var firebaseConfigPath = configuration["GOOGLE_APPLICATION_CREDENTIALS"];

    if (string.IsNullOrEmpty(firebaseConfigPath))
    {
        throw new InvalidOperationException("FirebaseConfigPath is not set in configuration.");
    }

    if (!File.Exists(firebaseConfigPath))
    {
        throw new FileNotFoundException($"Firebase configuration file not found at {firebaseConfigPath}");
    }

    return FirebaseApp.Create(new AppOptions
    {
        Credential = GoogleCredential.FromFile(firebaseConfigPath)
    });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddScheme<AuthenticationSchemeOptions, FirebaseAuthHandler>(JwtBearerDefaults.AuthenticationScheme, (o)=> { });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FiloBookContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddCors();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(builder => builder
.WithOrigins("http://localhost:5173/")
    .AllowAnyMethod()
        .AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();

app.Run();
