using System;
using Moq;
using Xunit;
using Dominio;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.UsuarioDTOs;
using LogicaAplicacion.ImplementacionCasosUsos.Estudiantes;
using LogicaNegocio.ValueObjects;

namespace PruebasUnitarias.PruebasLogicaAplicacion
{
    public class PruebasAltaEstudiante
    {
        private readonly Mock<IRepositorioEstudiantes> _repoMock;
        private readonly AltaEstudiante _service;
        /*
        public PruebasAltaEstudiante()
        {
            _repoMock = new Mock<IRepositorioEstudiantes>();
            _service = new AltaEstudiante(_repoMock.Object);
        }

        [Fact]
        public void Ejecutar_DtoNulo_LanzaArgumentNullException()
        {
            // Arrange
            EstudianteAltaDto dto = null!;

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(
                () => _service.Ejecutar(dto)
            );
            Assert.Contains("estudianteAltaDto", ex.ParamName);
        }

        [Fact]
        public void Ejecutar_NombreUsuarioExiste_LanzaException()
        {
            // Arrange
            var dto = new EstudianteAltaDto
            {
                NombreUsuario = "Pedro25",
                Nombre = "Juan",
                Apellido = "Pérez",
                Contrasenia = "camaracaC52."
            };
            _repoMock.Setup(r => r.ExisteNombreUsuario("Pedro25")).Returns(true);

            // Act & Assert
            var ex = Assert.Throws<Exception>(
                () => _service.Ejecutar(dto)
            );
            Assert.Equal("El nombre de usuario ya está en uso.", ex.Message);
        }

        [Fact]
        public void Ejecutar_DatosValidos_AgregaEstudianteConContraseniaHasheada()
        {
            // Arrange
            var dto = new EstudianteAltaDto
            {
                NombreUsuario = "maria99",
                Nombre = "María",
                Apellido = "López",
                Contrasenia = "miSecreto.5"
            };
            _repoMock.Setup(r => r.ExisteNombreUsuario("maria99")).Returns(false);

            Estudiante capturado = null!;
            _repoMock
                .Setup(r => r.Add(It.IsAny<Estudiante>()))
                .Callback<Estudiante>(e => capturado = e);

            // Act
            _service.Ejecutar(dto);

            // Assert
            _repoMock.Verify(r => r.Add(It.IsAny<Estudiante>()), Times.Once);

            Assert.NotNull(capturado);
            // Verificar que la contraseña se haya hasheado
            Assert.NotEqual("miSecreto.5", capturado.Contrasenia.Valor);
            Assert.True(BCrypt.Net.BCrypt.Verify("miSecreto.5", capturado.Contrasenia.Valor));

            // Verificar mapeo correcto del DTO
            Assert.Equal("maria99", capturado.NombreUsuario.Valor);
            Assert.Equal("María", capturado.NombreCompleto.Nombre);
            Assert.Equal("López", capturado.NombreCompleto.Apellido);
        }
        */
    }
}
