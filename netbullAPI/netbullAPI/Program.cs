using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using netbullAPI.Interfaces;
using netbullAPI.Negocio;
using netbullAPI.Persistencia;
using netbullAPI.Security.Negocio;
using netbullAPI.Security.Persistencia;
using netbullAPI.Security.Service;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("TokenConfigurations").GetSection("JwtKey").Value);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo { Title = "NetBullAPI", Version = "v1" });

    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = @"Cabeçalho de autorização JWT usando o esquema Bearer.
                        Digite 'Bearer' [espaço] e então seu token na entrada de texto abaixo.
                        Exemplo:'Bearer 12345abcdef' "
    });
    s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });
});

builder.Services.AddDbContext<netbullDBContext>(opts =>
{
    opts.UseNpgsql(builder.Configuration.GetConnectionString("NetBullConnection"));
});

builder.Services.AddAuthentication(authOptions =>
{
    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(bearerOptions =>
{
    bearerOptions.TokenValidationParameters = new TokenValidationParameters
    {     
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false
    };
});

builder.Services.AddScoped<NE_User>();
builder.Services.AddScoped<NE_Telefone>();
builder.Services.AddScoped<INotificador, Notificador>(); // Por Requisição

builder.Services.AddTransient<UserDAO>();
builder.Services.AddTransient<DAOTelefone>();
builder.Services.AddTransient<TokenService>(); // Por método

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