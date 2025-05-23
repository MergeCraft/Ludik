using Dominio;
using InterfacesRepositorio;
using LogicaAplicacion.ImplementacionCasosUsos.Usuarios;
using LogicaNegocio.Excepciones;
using LogicaNegocio.ValueObjects;
using Moq;


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
        public async void Ejecutar_UsuarioExistenteYContraseniaValida_DevuelveDto()
        {
            // Arrange
            var plainPwd = "4732Mmsi.";
            var hash = BCrypt.Net.BCrypt.HashPassword(plainPwd);

            var usuario = new Profesor
            {
                NombreUsuario = new NombreUsuario("Pedro25"),
                Contrasenia = new Contrasenia(hash)
            };
            _repoMock.Setup(r => r.loginUsuario("Pedro25"))
                .ReturnsAsync(usuario);


            // Act
            var result = await _service.Ejecutar("Pedro25", plainPwd);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Pedro25", result.NombreUsuario);
        }

        [Fact]
        public async void Ejecutar_UsuarioNoExiste_LanzaUsuarioNoValidoException()
        {
            // Arrange
            _repoMock
                .Setup(r => r.loginUsuario("invitado"))
                .ReturnsAsync((Usuario)null);

                // Act & Assert
            await Assert.ThrowsAsync<UsuarioNoValidoException>(
                () => _service.Ejecutar("invitado", "cualquier"));
        }

        [Fact]
        public async void Ejecutar_ContraseniaIncorrecta_LanzaContraseniaNoValidaException()
        {
            // Arrange
            var hashCorrecto = BCrypt.Net.BCrypt.HashPassword("4732Mmsi.");


            var usuarioReal = new Profesor
            {
                NombreUsuario = new NombreUsuario("JulioProfe"),
                Contrasenia = new Contrasenia(hashCorrecto)
            };

            _repoMock
                .Setup(r => r.loginUsuario("JulioProfe"))
                .ReturnsAsync(usuarioReal);

            // Act & Assert
            await Assert.ThrowsAsync<ContraseniaNoValidaException>(
                () => _service.Ejecutar("JulioProfe", "incorrecta"));
        }

        [Fact]
        public void VerificarContrasenia_EmptyPassword_ReturnsTrue()
        {
            // Arrange
            var hashEmpty = BCrypt.Net.BCrypt.HashPassword(string.Empty);

            // Act
            var isValid = _service.VerificarContrasenia(string.Empty, hashEmpty);

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void VerificarContrasenia_PasswordNoCoincide_ReturnsFalse()
        {
            // Arrange
            var hash = BCrypt.Net.BCrypt.HashPassword("hola");

            // Act
            var isValid = _service.VerificarContrasenia("adios", hash);

            // Assert
            Assert.False(isValid);
        }

        [Fact]
        public async Task Ejecutar_ContraseniaNula_LanzaArgumentNullException()
        {
            // Arrange
            var hash = BCrypt.Net.BCrypt.HashPassword("x");

            var usuario = new Estudiante()
            {
                NombreUsuario = new NombreUsuario("test"),
                Contrasenia = new Contrasenia(hash)
            };

            _repoMock
                .Setup(r => r.loginUsuario("test"))
                .ReturnsAsync(usuario);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(
                () => _service.Ejecutar("test", null!));
        }
    }
}
