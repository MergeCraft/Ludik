using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Jwt
{
    public class ManejadorJwt
    {
        private readonly IConfiguration _configuracion;

        public ManejadorJwt(IConfiguration config)
        {
            _configuracion = config;
        }

        public string GenerarToken(string usuarioId, string nombreUsuario, string rol)
        {
            // 1. Leer valores desde appsettings.json (o variables de entorno)
            var jwtConfig = _configuracion.GetSection("JwtSettings");
            string claveSecreta = jwtConfig.GetValue<string>("ClaveSecreta");
            string issuer = jwtConfig.GetValue<string>("Issuer");
            string audience = jwtConfig.GetValue<string>("Audience");
            int expiracionDias = jwtConfig.GetValue<int>("ExpiracionDias");

            // 2. Preparar la clave simétrica
            var keyBytes = Encoding.UTF8.GetBytes(claveSecreta);
            var signingKey = new SymmetricSecurityKey(keyBytes);
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha512Signature);

            // 3. Definir los claims: ID, Username, Rol
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuarioId),          // Subject: ID de usuario
                new Claim(ClaimTypes.Name, nombreUsuario),                             // Nombre de usuario
                new Claim(ClaimTypes.Role, rol),                                        // Rol
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())      // Token ID único
            };

            // 4. Crear el token
            var tokenDescriptor = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(expiracionDias),
                signingCredentials: credentials
            );

            // 5. Retornar el JWT en string
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

    }
}
