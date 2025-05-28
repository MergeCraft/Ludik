using System;
using Moq;
using Xunit;
using Dominio;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.ProfesorDTOs;
using LogicaAplicacion.ImplementacionCasosUsos.Profesores;
using LogicaNegocio.ValueObjects;
using LogicaNegocio.Excepciones;

namespace PruebasUnitarias.PruebasLogicaAplicacion
{
    public class PruebasAltaProfesor
    {
        private readonly Mock<IRepositorioProfesores> _repoMock;
        private readonly AltaProfesor _service;

        public PruebasAltaProfesor()
        {
            _repoMock = new Mock<IRepositorioProfesores>();
            _service = new AltaProfesor(_repoMock.Object);
        }

        [Fact]
        public async Task EjecutarAsync_DtoNulo_LanzaArgumentNullException()
        {
            // Arrange
            ProfesorAltaDto dto = null!;

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(
                () => _service.EjecutarAsync(dto)
            );
            Assert.Contains("profesorAltaDto", ex.ParamName);
        }

        [Fact]
        public async Task EjecutarAsync_NombreUsuarioExiste_LanzaException()
        {
            // Arrange
            var dto = new ProfesorAltaDto
            {
                Email = "pepito@dominio.com",
                NombreUsuario = "pepito123",
                Nombre = "Pepito",
                Apellido = "González",
                Contrasenia = "Abc12345!"
            };

            _repoMock
                .Setup(r => r.ExisteNombreUsuarioAsync("pepito123"))
                .ReturnsAsync(true);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<UsuarioNoValidoException>(
                () => _service.EjecutarAsync(dto)
            );
            Assert.Equal("El nombre de usuario ya está en uso.", ex.Message);
        }

        [Fact]
        public async Task EjecutarAsync_EmailExiste_LanzaException()
        {
            // Arrange
            var dto = new ProfesorAltaDto
            {
                Email = "pepito@dominio.com",
                NombreUsuario = "pepito123",
                Nombre = "Pepito",
                Apellido = "González",
                Contrasenia = "Abc12345!"
            };

            _repoMock
                .Setup(r => r.ExisteNombreUsuarioAsync("pepito123"))
                .ReturnsAsync(false);
            _repoMock
                .Setup(r => r.ExisiteMailProfesorAsync("pepito@dominio.com"))
                .ReturnsAsync(true);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<UsuarioNoValidoException>(
                () => _service.EjecutarAsync(dto)
            );
            Assert.Equal("El mail de profesor ya está en uso.", ex.Message);
        }

        [Fact]
        public async Task EjecutarAsync_DatosValidos_AgregaProfesorConContraseniaHasheada()
        {
            // Arrange
            var dto = new ProfesorAltaDto
            {
                Email = "ana@ejemplo.com",
                NombreUsuario = "anaprof",
                Nombre = "Ana",
                Apellido = "López",
                Contrasenia = "Secret#123"
            };

            _repoMock.Setup(r => r.ExisteNombreUsuarioAsync("anaprof"))
                     .ReturnsAsync(false);
            _repoMock.Setup(r => r.ExisiteMailProfesorAsync("ana@ejemplo.com"))
                     .ReturnsAsync(false);

            Profesor capturado = null!;
            _repoMock.Setup(r => r.AddAsync(It.IsAny<Profesor>()))
                     .Callback<Profesor>(p => capturado = p)
                     .Returns(Task.CompletedTask);

            // Act
            await _service.EjecutarAsync(dto);

            // Assert
            _repoMock.Verify(r => r.AddAsync(It.IsAny<Profesor>()), Times.Once);
            Assert.NotNull(capturado);
            Assert.NotEqual("Secret#123", capturado.Contrasenia.Valor);
            Assert.True(BCrypt.Net.BCrypt.Verify("Secret#123", capturado.Contrasenia.Valor));
            Assert.Equal("ana@ejemplo.com", capturado.email.Valor);
            Assert.Equal("anaprof", capturado.NombreUsuario.Valor);
        }
    }
}
