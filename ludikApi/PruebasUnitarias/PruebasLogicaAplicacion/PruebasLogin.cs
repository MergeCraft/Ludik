using Dominio;
using InterfacesRepositorio;
using LogicaAplicacion.ImplementacionCasosUsos.Usuarios;
using LogicaNegocio.ValueObjects;
using Moq;


namespace PruebasUnitarias.PruebasLogicaAplicacion
{
    public class PruebasLogin
    {
        private readonly Mock<IRepositorioUsuarios> _repoMock;
        private readonly LoginPrueba _service;

        public PruebasLogin()
        {
            _repoMock = new Mock<IRepositorioUsuarios>();
            _service = new LoginPrueba(_repoMock.Object);
        }

        [Fact]
        public void Ejecutar_UsuarioExistenteYContraseniaValida_DevuelveDto()
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
                .Returns(usuario);


            // Act
            var result = _service.Ejecutar("Pedro25", plainPwd);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Pedro25", result.NombreUsuario);
        }

        [Fact]
        public void Ejecutar_UsuarioNoExiste_DevuelveNull()
        {
            // Arrange
            _repoMock
                .Setup(r => r.loginUsuario("invitado"))
                .Returns((Usuario)null);

            // Act
            var result = _service.Ejecutar("invitado", "cualquier");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Ejecutar_ContraseniaIncorrecta_DevuelveNull()
        {
            // Arrange
            // Hasheamos la contraseña “correcta”:
            var hashCorrecto = BCrypt.Net.BCrypt.HashPassword("4732Mmsi.");


            var usuarioReal = new Profesor
            {
                NombreUsuario = new NombreUsuario("JulioProfe"),
                Contrasenia = new Contrasenia(hashCorrecto)
            };

            _repoMock
                .Setup(r => r.loginUsuario("JulioProfe"))
                .Returns(usuarioReal);

            // Act
            // Pasamos “incorrecta” como plain-text, para que no coincida con “4732Mmsi.”
            var result = _service.Ejecutar("JulioProfe", "incorrecta");

            // Assert
            Assert.Null(result);
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
        public void Ejecutar_ContraseniaNula_LanzaArgumentNullException()
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
                .Returns(usuario);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(
                () => _service.Ejecutar("test", null!));
        }
    }
}
