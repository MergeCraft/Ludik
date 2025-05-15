using System.Text;
using AccesoDatos.RepositoriosEF;
using InterfacesRepositorio;
using LogicaAplicacion.ImplementacionCasosUsos.Usuarios;
using LogicaAplicacion.InterfacesCasosUsos.Usuario;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configurar Swagger
var ruta = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebApi.xml");
builder.Services.AddSwaggerGen(opciones =>
{
	opciones.IncludeXmlComments(ruta);
	opciones.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "API de Ludik",
		Version = "v1",
		Description = "Bitácora digital de logros de aprendizaje.",
		Contact = new OpenApiContact { Email = "renatoriosx@gmail.com" }
	});
});

// Inyectar repositorios y casos de uso
builder.Services.AddScoped<IRepositorioUsuarios, RepositorioUsuariosEF>();
builder.Services.AddScoped<ILogin, LoginPrueba>();

// Configurar autenticación JWT
var claveDificil = "UnaContraseniaSeguraEsLargaTiene:0123,caracteresEspeciales;*#seguridad";
var claveDificilEncriptada = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claveDificil));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(opt =>
	{
		opt.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = false,
			ValidateAudience = false,
			ValidateLifetime = false,
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = claveDificilEncriptada
		};
	});

// Configurar CORS
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowReactApp",
		policy =>
		{
			policy.WithOrigins("http://localhost:3000")
				  .AllowAnyHeader()
				  .AllowAnyMethod();
		});
});

var app = builder.Build();

// Configurar el pipeline de la aplicación
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Aplicar la política CORS
app.UseCors("AllowReactApp");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
