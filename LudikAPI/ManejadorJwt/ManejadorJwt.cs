using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;


namespace WebApi.ManejadorJwt
{
    public class ManejadorJwt
    {
       
        public static string GenerarToken(string email, string rol)
        {
            var claveDificil = "ClaveMuySecrecta1_ClaveMuySecrecta1_ClaveMuySecrecta1_ClaveMuySecrecta1_ClaveMuySecrecta1";
            var claveDificilEncriptada = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claveDificil));
            List<Claim> claims = [
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, rol)
                ];
            var credenciales = new SigningCredentials(claveDificilEncriptada, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: credenciales);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

    }
}
