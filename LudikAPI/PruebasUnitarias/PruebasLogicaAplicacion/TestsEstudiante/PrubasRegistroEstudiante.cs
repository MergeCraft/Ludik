using System;
using Moq;
using Xunit;
using LogicaNegocio.Entidades;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.UsuarioDTOs;
using LogicaAplicacion.ImplementacionCasosUsos.Estudiantes;
using LogicaNegocio.ValueObjects;
using LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Identity;

namespace PruebasUnitarias.PruebasLogicaAplicacion.Estudiante
{
    public class PruebasAltaEstudiante
    {
        private readonly Mock<UserManager<Usuario>> _userManagerMock;
        private readonly Mock<IPasswordHasher<Usuario>> _passwordHasherMock;

        public PruebasAltaEstudiante()
        {
            var storeMock = new Mock<IUserStore<Usuario>>();
            _userManagerMock = new Mock<UserManager<Usuario>>(
                storeMock.Object, null, null, null, null, null, null, null, null
            );
            _passwordHasherMock = new Mock<IPasswordHasher<Usuario>>();
        }

        [Fact]
        public async Task Ejecutar_DtoNulo_RetornaFalloConErrorEsperado()
        {
            // Arrange
            _passwordHasherMock
                .Setup(h => h.HashPassword(It.IsAny<Usuario>(), It.IsAny<string>()))
                .Returns("hashed_respuesta_de_prueba");
            var service = new AltaEstudiante(_userManagerMock.Object, _passwordHasherMock.Object);
            EstudianteAltaDto dto = null!;

            // Act
            var resultado = await service.EjecutarAsync(dto);

            // Assert
            Assert.True(resultado.EsFallo);
            var error = resultado.Errores.First();
            Assert.Equal("Validation", error.Codigo);
            Assert.Equal("Los datos del estudiante no pueden ser nulos.", error.Mensaje);
        }

        [Fact]
        public async Task Ejecutar_NombreUsuarioExiste_RetornaFalloConErrorDeConflicto()
        {
            // Arrange
            _passwordHasherMock
                .Setup(h => h.HashPassword(It.IsAny<Usuario>(), It.IsAny<string>()))
                .Returns("hashed_respuesta_de_prueba");
            var dto = new EstudianteAltaDto
            {
                NombreUsuario = "pedro25",
                Nombre = "Juan",
                Apellido = "Pérez",
                Contrasenia = "Camaracac52."
            };

            _userManagerMock.Setup(x => x.FindByNameAsync("pedro25"))
                .ReturnsAsync(new Usuario()); // Simula que ya existe

            var service = new AltaEstudiante(_userManagerMock.Object, _passwordHasherMock.Object);

            // Act
            var resultado = await service.EjecutarAsync(dto);

            // Assert
            Assert.True(resultado.EsFallo);
            var error = resultado.Errores.First();
            Assert.Equal("Conflict", error.Codigo);
            Assert.Equal("El nombre de usuario ya está en uso.", error.Mensaje);
        }

        [Fact]
        public async Task Ejecutar_DatosValidos_CreaUsuarioYAsignaRol()
        {
            // Arrange
            _passwordHasherMock
                .Setup(h => h.HashPassword(It.IsAny<Usuario>(), It.IsAny<string>()))
                .Returns("hashed_respuesta_de_prueba");
            var dto = new EstudianteAltaDto
            {
                NombreUsuario = "maria99",
                Nombre = "María",
                Apellido = "López",
                Contrasenia = "Misecreto.5"
            };

            _userManagerMock.Setup(x => x.FindByNameAsync("maria99"))
                .ReturnsAsync((Usuario)null!); // No existe aún

            _userManagerMock.Setup(x =>
                    x.CreateAsync(It.IsAny<Usuario>(), "Misecreto.5"))
                .ReturnsAsync(IdentityResult.Success);

            _userManagerMock.Setup(x =>
                    x.AddToRoleAsync(It.IsAny<Usuario>(), "Estudiante"))
                .ReturnsAsync(IdentityResult.Success);

            var service = new AltaEstudiante(_userManagerMock.Object, _passwordHasherMock.Object);

            // Act
            await service.EjecutarAsync(dto);

            // Assert
            _userManagerMock.Verify(x => x.CreateAsync(It.IsAny<Usuario>(), "Misecreto.5"), Times.Once);
            _userManagerMock.Verify(x => x.AddToRoleAsync(It.IsAny<Usuario>(), "Estudiante"), Times.Once);
        }
        
    }
}
