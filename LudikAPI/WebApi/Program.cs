using System.Text;
using AccesoDatos.RepositoriosEF;
using Dominio;
using InterfacesRepositorio;
using LogicaAplicacion.ImplementacionCasosUsos.Estudiantes;
using LogicaAplicacion.ImplementacionCasosUsos.Grupos;
using LogicaAplicacion.ImplementacionCasosUsos.Medallas;
using LogicaAplicacion.ImplementacionCasosUsos.Profesores;
using LogicaAplicacion.ImplementacionCasosUsos.Usuarios;
using LogicaAplicacion.InterfacesCasosUsos.Estudiante;
using LogicaAplicacion.InterfacesCasosUsos.Grupo;
using LogicaAplicacion.InterfacesCasosUsos.Medalla;
using LogicaAplicacion.InterfacesCasosUsos.Profesor;
using LogicaAplicacion.InterfacesCasosUsos.Usuario;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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


builder.Services
    .AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<ContextoDb>()
    .AddDefaultTokenProviders();



// -------------------------------
// Configura JWT Authentication
// -------------------------------

// Lee configuración JWT desde appsettings.json
var jwtSettings = builder.Configuration.GetSection("Jwt");
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

builder.Services.AddSingleton<ManejadorJwt>();

// -------------------------------
// Configura Authorization (políticas/roles)
// -------------------------------

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("EsAdministrador", policy => policy.RequireRole("Administrador"));
    options.AddPolicy("EsProfesor", policy => policy.RequireRole("Profesor"));
    options.AddPolicy("EsEstudiante", policy => policy.RequireRole("Estudiante"));
});



// Inyeccion repositorios
builder.Services.AddScoped<IRepositorioUsuarios, RepositorioUsuariosEF>();
builder.Services.AddScoped<IRepositorioEstudiantes, RepositorioEstudiantesEF>();
builder.Services.AddScoped<IRepositorioProfesores, RepositorioProfesoresEF>();
builder.Services.AddScoped<IRepositorioGrupos, RepositorioGruposEF>();
builder.Services.AddScoped<IRepositorioTablasEquivalencia, RepositorioTablasEquivalenciaEF>();
builder.Services.AddScoped<IRepositorioMedallas, RepositorioMedallasEF>();

//Inyeccion casos de uso
builder.Services.AddScoped<ILogin, Login>();
builder.Services.AddScoped<IAltaEstudiante, AltaEstudiante>();
builder.Services.AddScoped<IAltaProfesor, AltaProfesor>();
builder.Services.AddScoped<IAltaGrupo, AltaGrupo>();
builder.Services.AddScoped<IEditarGrupo, EditarGrupo>();
builder.Services.AddScoped<IAltaMedalla, AltaMedalla>();

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
						.AllowAnyMethod());
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
app.MapIdentityApi<Usuario>();

app.Run();

public partial class Program { }