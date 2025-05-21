using System;
using Moq;
using Xunit;
using Dominio;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.ProfesorDTOs;
using LogicaAplicacion.ImplementacionCasosUsos.Profesores;
using LogicaNegocio.ValueObjects;

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
        public void Ejecutar_DtoNulo_LanzaArgumentNullException()
        {
            // Arrange
            ProfesorAltaDto dto = null!;

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(
                () => _service.Ejecutar(dto)
            );
            Assert.Contains("profesorAltaDto", ex.ParamName);
        }

        [Fact]
        public void Ejecutar_NombreUsuarioExiste_LanzaException()
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
                .Setup(r => r.ExisteNombreUsuario("pepito123"))
                .Returns(true);

            // Act & Assert
            var ex = Assert.Throws<Exception>(
                () => _service.Ejecutar(dto)
            );
            Assert.Equal("El nombre de usuario ya está en uso.", ex.Message);
        }

        [Fact]
        public void Ejecutar_EmailExiste_LanzaException()
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
                .Setup(r => r.ExisteNombreUsuario("pepito123"))
                .Returns(false);
            _repoMock
                .Setup(r => r.ExisiteMailProfesor("pepito@dominio.com"))
                .Returns(true);

            // Act & Assert
            var ex = Assert.Throws<Exception>(
                () => _service.Ejecutar(dto)
            );
            Assert.Equal("El email de usuario ya está en uso.", ex.Message);
        }

        [Fact]
        public void Ejecutar_DatosValidos_AgregaProfesorConContraseniaHasheada()
        {
            // Arrange
            var dto = new ProfesorAltaDto
            {
                Email = "ana@ejemplo.com",
                NombreUsuario = "anaProf",
                Nombre = "Ana",
                Apellido = "López",
                Contrasenia = "Secret#123"
            };
            _repoMock.Setup(r => r.ExisteNombreUsuario("anaProf")).Returns(false);
            _repoMock.Setup(r => r.ExisiteMailProfesor("ana@ejemplo.com")).Returns(false);

            Profesor capturado = null!;
            _repoMock
                .Setup(r => r.Add(It.IsAny<Profesor>()))
                .Callback<Profesor>(p => capturado = p);

            // Act
            _service.Ejecutar(dto);

            // Assert
            _repoMock.Verify(r => r.Add(It.IsAny<Profesor>()), Times.Once);

            Assert.NotNull(capturado);
            // La contraseña debe estar hasheada (no es igual al texto plano)
            Assert.NotEqual("Secret#123", capturado.Contrasenia.Clave);
            Assert.True(BCrypt.Net.BCrypt.Verify("Secret#123", capturado.Contrasenia.Clave));

            // Verificar mapeo de email y usuario
            Assert.Equal("ana@ejemplo.com", capturado.correo.Correo);
            Assert.Equal("anaProf", capturado.NombreUsuario.Nombre);

            // (Opcional) si tu mapper también inicializa NombreCompleto:
            // Assert.Equal("Ana", capturado.NombreCompleto.Nombre);
            // Assert.Equal("López", capturado.NombreCompleto.Apellido);
        }
    }
}
