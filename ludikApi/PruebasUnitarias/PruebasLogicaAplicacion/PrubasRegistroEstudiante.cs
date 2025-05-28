using System;
using Moq;
using Xunit;
using Dominio;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.UsuarioDTOs;
using LogicaAplicacion.ImplementacionCasosUsos.Estudiantes;
using LogicaNegocio.ValueObjects;
using LogicaNegocio.Excepciones;

namespace PruebasUnitarias.PruebasLogicaAplicacion
{
    public class PruebasAltaEstudiante
    {
        private readonly Mock<IRepositorioEstudiantes> _repoMock;
        private readonly AltaEstudiante _service;

        public PruebasAltaEstudiante()
        {
            _repoMock = new Mock<IRepositorioEstudiantes>();
            _service = new AltaEstudiante(_repoMock.Object);
        }

        [Fact]
        public async Task Ejecutar_DtoNulo_LanzaArgumentNullException()
        {
            // Arrange
            EstudianteAltaDto dto = null!;

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => _service.EjecutarAsync(dto));
            Assert.Contains("estudianteAltaDto", ex.ParamName);
        }

        [Fact]
        public async Task Ejecutar_NombreUsuarioExiste_LanzaUsuarioNoValidoException()
        {
            // Arrange
            var dto = new EstudianteAltaDto
            {
                NombreUsuario = "pedro25",
                Nombre = "Juan",
                Apellido = "Pérez",
                Contrasenia = "Camaracac52."
            };
            _repoMock.Setup(r => r.ExisteNombreUsuarioAsync("pedro25")).ReturnsAsync(true);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<UsuarioNoValidoException>(() => _service.EjecutarAsync(dto));
            Assert.Equal("El nombre de usuario ya está en uso.", ex.Message);
        }

        [Fact]
        public async Task Ejecutar_DatosValidos_AgregaEstudianteConContraseniaHasheada()
        {
            // Arrange
            var dto = new EstudianteAltaDto
            {
                NombreUsuario = "maria99",
                Nombre = "María",
                Apellido = "López",
                Contrasenia = "Misecreto.5"
            };
            _repoMock.Setup(r => r.ExisteNombreUsuarioAsync("maria99")).ReturnsAsync(false);

            Estudiante capturado = null!;
            _repoMock
                .Setup(r => r.AddAsync(It.IsAny<Estudiante>()))
                .Callback<Estudiante>(e => capturado = e)
                .Returns(Task.CompletedTask);

            // Act
            await _service.EjecutarAsync(dto);

            // Assert
            _repoMock.Verify(r => r.AddAsync(It.IsAny<Estudiante>()), Times.Once);
            Assert.NotNull(capturado);
            Assert.NotEqual("misecreto.5", capturado.Contrasenia.Valor);
            Assert.True(BCrypt.Net.BCrypt.Verify("Misecreto.5", capturado.Contrasenia.Valor));
            Assert.Equal("maria99", capturado.NombreUsuario.Valor);
            Assert.Equal("María", capturado.NombreCompleto.Nombre);
            Assert.Equal("López", capturado.NombreCompleto.Apellido);
        }
    }
}
