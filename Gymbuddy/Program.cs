using Gymbuddy;
using Gymbuddy.Hubs;
using Gymbuddy.Middleware;
using Gymbuddy.Utilities;
using GymBuddy.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
// Add services to the container.
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthorization();
builder.Services.AddScoped<FileManager>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
    o.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];

            // If the request is for our hub...
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) &&
                (path.StartsWithSegments("/hubs/chat")))
            {
                // Read the token out of the query string
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<DB>(options => options.UseSqlServer(
"Server=tcp:gymbuddy-buli.database.windows.net,1433;Initial Catalog=Gymbuddy;Persist Security Info=False;User ID=gymbuddy;Password=Test123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));
var app = builder.Build();
app.UseMiddleware<WebSocketsMiddleWare>();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors(builder =>
{
    builder.SetIsOriginAllowed(x => true)
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials();
});
    app.UseSwagger();
    app.UseSwaggerUI();
app.UseWebSockets();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthorization();
app.MapHub<ChatHub>("/hub/chatHub");

app.MapControllers();

app.Run();
