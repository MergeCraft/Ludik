using Dominio;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.UsuarioDTOs;
using LogicaAplicacion.ImplementacionCasosUsos.Usuarios;
using LogicaNegocio.Excepciones;
using LogicaNegocio.ValueObjects;
using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebasUnitarias.PruebasLogicaAplicacion
{
    public class PruebasLogin
    {
        private readonly Mock<IRepositorioUsuarios> _repoMock;
        private readonly Login _service;

        public PruebasLogin()
        {
            _repoMock = new Mock<IRepositorioUsuarios>();
            _service = new Login(_repoMock.Object);
        }

        [Fact]
        public async Task Ejecutar_UsuarioExistenteYContraseniaValida_DevuelveDto()
        {
            // Arrange
            var plainPwd = "4732mmsi.";
            var usuario = new Profesor
            {
                Id = "1",
                UserName = "pedro25"
            };

            _repoMock.Setup(r => r.GetUsuarioPorNombreAsync("pedro25"))
                .ReturnsAsync(usuario);

            _repoMock.Setup(r => r.VerificarContrasenaAsync(usuario, plainPwd))
                .ReturnsAsync(true);

            _repoMock.Setup(r => r.GetRolesAsync(usuario))
                .ReturnsAsync(new List<string> { "Profesor" });

            // Act
            var result = await _service.Ejecutar("pedro25", plainPwd);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("pedro25", result.NombreUsuario);
            Assert.Equal("1", result.Id);
            Assert.Equal("Profesor", result.Rol);
        }

        [Fact]
        public async Task Ejecutar_UsuarioNoExiste_LanzaUsuarioNoValidoException()
        {
            // Arrange
            _repoMock.Setup(r => r.GetUsuarioPorNombreAsync("invitado"))
                .ReturnsAsync((Usuario)null!);

            // Act & Assert
            await Assert.ThrowsAsync<UsuarioNoValidoException>(
                () => _service.Ejecutar("invitado", "cualquier"));
        }

        [Fact]
        public async Task Ejecutar_ContraseniaIncorrecta_LanzaContraseniaNoValidaException()
        {
            // Arrange
            var usuario = new Profesor
            {
                Id = "2",
                UserName = "julioprofe"
            };

            _repoMock.Setup(r => r.GetUsuarioPorNombreAsync("julioprofe"))
                .ReturnsAsync(usuario);

            _repoMock.Setup(r => r.VerificarContrasenaAsync(usuario, "incorrecta"))
                .ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<ContraseniaNoValidaException>(
                () => _service.Ejecutar("julioprofe", "incorrecta"));
        }

        [Fact]
        public async Task Ejecutar_UsuarioSinRol_LanzaExcepcion()
        {
            // Arrange
            var usuario = new Estudiante
            {
                Id = "3",
                UserName = "sinrol"
            };

            _repoMock.Setup(r => r.GetUsuarioPorNombreAsync("sinrol"))
                .ReturnsAsync(usuario);

            _repoMock.Setup(r => r.VerificarContrasenaAsync(usuario, "pass123"))
                .ReturnsAsync(true);

            _repoMock.Setup(r => r.GetRolesAsync(usuario))
                .ReturnsAsync(new List<string>()); // sin roles

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(
                () => _service.Ejecutar("sinrol", "pass123"));

            Assert.Equal("El usuario no tiene un rol asignado.", ex.Message);
        }

        [Fact]
        public async Task Ejecutar_ContraseniaNula_LanzaArgumentNullException()
        {
            // Arrange
            var usuario = new Estudiante
            {
                Id = "4",
                UserName = "test"
            };

            _repoMock.Setup(r => r.GetUsuarioPorNombreAsync("test"))
                .ReturnsAsync(usuario);

            // No hace falta configurar VerificarContrasenaAsync porque lanzará antes

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(
                () => _service.Ejecutar("test", null!));
        }
    }
}
