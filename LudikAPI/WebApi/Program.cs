using System.Text;
using AccesoDatos.RepositoriosEF;
using Dominio;
using InterfacesRepositorio;
using LogicaAplicacion.ImplementacionCasosUsos.Estudiantes;
using LogicaAplicacion.ImplementacionCasosUsos.Grupos;
using LogicaAplicacion.ImplementacionCasosUsos.Medallas;
using LogicaAplicacion.ImplementacionCasosUsos.Profesores;
using LogicaAplicacion.InterfacesCasosUsos.Estudiante;
using LogicaAplicacion.InterfacesCasosUsos.Grupo;
using LogicaAplicacion.InterfacesCasosUsos.Medalla;
using LogicaAplicacion.InterfacesCasosUsos.Profesor;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebApi.Jwt;

var builder = WebApplication.CreateBuilder(args);

var cadenaDeConexionBD = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ContextoDb>(options => options.UseSqlServer(cadenaDeConexionBD));
// -------------------------------
// Configura Identity y sus servicios
// -------------------------------
builder.Services.AddAuthorization();


builder.Services.AddIdentity<Usuario, IdentityRole>(opciones =>
{
	// ===== Validaciones de Contraseña =====
	opciones.Password.RequireDigit = true;                   // Al menos un dígito [0-9]
	opciones.Password.RequireLowercase = true;               // Al menos una minúscula [a-z]
	opciones.Password.RequireUppercase = true;               // Al menos una mayúscula [A-Z]
	opciones.Password.RequireNonAlphanumeric = true;         // Al menos un carácter no alfanumérico (por ejemplo, !, @, #)
	opciones.Password.RequiredLength = 8;                    // Longitud mínima de 8 caracteres

	// ===== Validaciones de Usuario =====
	opciones.User.RequireUniqueEmail = false;                  // El email debe ser único
	opciones.User.AllowedUserNameCharacters =
		"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

	// ===== Bloqueo de usuario =====
	opciones.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
	opciones.Lockout.MaxFailedAccessAttempts = 5;
	opciones.Lockout.AllowedForNewUsers = true;

})
	.AddEntityFrameworkStores<ContextoDb>()
	.AddDefaultTokenProviders();



// -------------------------------
// Configura JWT Authentication
// -------------------------------
builder.Services.AddSingleton<IManejadorJwt, ManejadorJwt>();

// Lee configuración JWT desde appsettings.json
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var claveDificil = jwtSettings.GetValue<string>("Key");
var issuer = jwtSettings.GetValue<string>("Issuer");
var audience = jwtSettings.GetValue<string>("Audience");

var claveDificilEncriptada = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claveDificil));
builder.Services.AddAuthentication(options =>
	{
		options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	})
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = issuer,
			ValidAudience = audience,
			IssuerSigningKey = claveDificilEncriptada,
			ClockSkew = TimeSpan.Zero
		};
	});

// -------------------------------
// Configura Authorization (políticas/roles)
// -------------------------------

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("EsAdministrador", policy => policy.RequireRole("Administrador"));
	options.AddPolicy("EsProfesor", policy => policy.RequireRole("Profesor"));
	options.AddPolicy("EsEstudiante", policy => policy.RequireRole("Estudiante"));
});



// Inyeccion de dependencias repositorios
builder.Services.AddScoped<IRepositorioUsuarios, RepositorioUsuariosEF>();
builder.Services.AddScoped<IRepositorioEstudiantes, RepositorioEstudiantesEF>();
builder.Services.AddScoped<IRepositorioProfesores, RepositorioProfesoresEF>();
builder.Services.AddScoped<IRepositorioGrupos, RepositorioGruposEF>();
builder.Services.AddScoped<IRepositorioTablasEquivalencia, RepositorioTablasEquivalenciaEF>();
builder.Services.AddScoped<IRepositorioMedallas, RepositorioMedallasEF>();

//Inyeccion de dependencias casos de uso

builder.Services.AddScoped<IAltaEstudiante, AltaEstudiante>();
builder.Services.AddScoped<IAltaProfesor, AltaProfesor>();
builder.Services.AddScoped<IAltaGrupo, AltaGrupo>();
builder.Services.AddScoped<IEditarGrupo, EditarGrupo>();
builder.Services.AddScoped<IBajaGrupo, BajaGrupo>();
builder.Services.AddScoped<IAltaMedalla, AltaMedalla>();
builder.Services.AddScoped<IObtenerGruposDeEstudiante, ObtenerGruposDeEstudiante>();
builder.Services.AddScoped<IObtenerGruposDeProfesor, ObtenerGruposDeProfesor>();


// -------------------------------
//      Swagger y CORS
// -------------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

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

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll",
		policy => policy.AllowAnyOrigin()
						.AllowAnyHeader()
						.AllowAnyMethod()
						);
});


var app = builder.Build();

// -------------------------------
// Seed Roles al iniciar la app
// -------------------------------

using (var scope = app.Services.CreateScope())
{
	var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
	var roles = new[] { "Administrador", "Profesor", "Estudiante" };

	foreach (var rolNombre in roles)
	{
		var existe = await roleManager.RoleExistsAsync(rolNombre);
		if (!existe)
		{
			await roleManager.CreateAsync(new IdentityRole(rolNombre));
		}
	}
}


if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS debe ir antes de autenticación/autorización
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }