using JwtAspNet;
using JwtAspNet.Extensions;
using JwtAspNet.Models;
using JwtAspNet.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<TokenService>();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.PrivateKey)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("admin", p => p.RequireRole("admin"));
});

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/login", (TokenService service) =>
{
    var roles = new string[]
    {
        "user",
        "major"
    };
    var user = new User(1, "Lucas Andrade", "lucasilva108@gmail.com", "https://profile-image.com/", "12341234", roles);
    return service.Create(user);
});

app.MapGet("/restrito", (ClaimsPrincipal user) =>
{
    return new
    {
        Id = user.Id(),
        Name = user.Name(),
        Email = user.Email(),
        GivenName = user.GivenName(),
        Image = user.Image(),
    };
}).RequireAuthorization();
app.MapGet("/admin", () => "Você tem acesso admin").RequireAuthorization("admin");

app.Run();
