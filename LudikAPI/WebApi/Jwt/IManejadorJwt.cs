namespace WebApi.Jwt
{
    public interface IManejadorJwt
    {
        public string GenerarToken(string usuarioId, string nombreUsuario, string rol);
    }
}
