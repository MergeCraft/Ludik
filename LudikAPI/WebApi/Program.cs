using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
var ruta = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebApi.xml");
builder.Services.AddSwaggerGen(opciones =>
    {
        opciones.IncludeXmlComments(ruta);
        opciones.SwaggerDoc("v0", new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "API de Ludik",
            Version = "v0",
            Description = "Bitacora digital de logros de aprendizaje.",
            Contact = new OpenApiContact { Email = "renatoriosx@gmail.com" }
        });
    }
);

//Servicios necesarios para autenticacion
var claveDificil = "UnaContraseniaSeguraEsLargaTiene:0123,caracteresEspeciales;*#seguridad";
var claveDificilEncriptada = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claveDificil));

//Registro de servicios JWT 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            //Definir la verificaciones  a realizar
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = claveDificilEncriptada
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();// agregamos este para el uso de la token , van en orden(es importante)
app.UseAuthorization();


app.MapControllers();

app.Run();
