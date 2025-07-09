using InterfacesRepositorio;
using LogicaAplicacion.DTOs.RestablecerContrasenaDTO;
using LogicaAplicacion.InterfacesCasosUsos.RecuperarContrasena;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using LogicaNegocio.Resultados;
using Microsoft.AspNetCore.Identity;

namespace LogicaAplicacion.ImplementacionCasosUsos.RecuperarContrasena;

public class RestablecerContrasena: IRestablecerContrasena
{
    private readonly IRepositorioUsuarios _repositorioUsuarios;
    private readonly UserManager<Usuario> _userManager;
    private readonly IPasswordHasher<Usuario> _passwordHasher;

    public RestablecerContrasena(
        IRepositorioUsuarios repositorioUsuarios,
        UserManager<Usuario> userManager,
        IPasswordHasher<Usuario> passwordHasher)
    {
        _repositorioUsuarios = repositorioUsuarios;
        _userManager = userManager;
        _passwordHasher = passwordHasher;
    }

    public async Task<Resultado> EjecutarAsync(InformacionParaRestablecerContrasenaDto dto)
    {
        try
        {
            var usuario = await _repositorioUsuarios.GetUsuarioPorNombreAsync(dto.NombreUsuario);

            var estudiante = usuario as Estudiante;
            if (estudiante == null)
                return Resultado.Falla(Error.Validation); 
            

            // Mapear dto a entidades para la verificación
            var respuestasIngresadas = dto.Respuestas.Select(r => new PreguntaRespuestaSeguridad { Id = r.Id, Respuesta = r.Respuesta }).ToList();


            if (!estudiante.CoincidenLasRespuestas(respuestasIngresadas, _passwordHasher))
                return Resultado.Falla(Error.Unauthorized);
            

            var token = await _userManager.GeneratePasswordResetTokenAsync(estudiante);
            var resultadoIdentity = await _userManager.ResetPasswordAsync(estudiante, token, dto.NuevaContrasena);

            if (!resultadoIdentity.Succeeded)
            {
                var errores = resultadoIdentity.Errors.Select(e => new Error("Error.Validation", e.Description));
                return Resultado.Falla(errores);
            }

            return Resultado.Exitoso();
        }
        catch (UsuarioNoValidoException)
        {
            return Resultado.Falla(Error.NotFound);
        }
        catch (Exception ex)
        {
            return Resultado.Falla(new Error("Error.Unexpected", ex.Message));
        }
    }

}