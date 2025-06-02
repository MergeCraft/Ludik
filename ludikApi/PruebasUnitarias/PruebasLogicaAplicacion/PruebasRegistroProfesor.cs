using System;
using Moq;
using Xunit;
using Dominio;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.ProfesorDTOs;
using LogicaAplicacion.ImplementacionCasosUsos.Profesores;
using LogicaNegocio.ValueObjects;
using LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Identity;

namespace PruebasUnitarias.PruebasLogicaAplicacion
{
    public class PruebasAltaProfesor
    {
        private readonly Mock<UserManager<Usuario>> _userManagerMock;

        public PruebasAltaProfesor()
        {
            var storeMock = new Mock<IUserStore<Usuario>>();
            _userManagerMock = new Mock<UserManager<Usuario>>(
                storeMock.Object, null, null, null, null, null, null, null, null
            );
        }

        [Fact]
        public async Task EjecutarAsync_DtoNulo_DevuelveIdentityResultFallido()
        {
            // Arrange
            var service = new AltaProfesor(_userManagerMock.Object);
            ProfesorAltaDto dto = null!;

            // Act
            var resultado = await service.EjecutarAsync(dto);

            // Assert
            Assert.False(resultado.Succeeded);
            Assert.Contains(resultado.Errors, e => e.Code == "ArgNull");
        }

        [Fact]
        public async Task EjecutarAsync_NombreUsuarioExiste_DevuelveIdentityResultFallido()
        {
            // Arrange
            var dto = new ProfesorAltaDto
            {
                Correo = "pepito@dominio.com",
                NombreUsuario = "pepito123",
                Nombre = "Pepito",
                Apellido = "González",
                Contrasenia = "Abc12345!"
            };

            _userManagerMock.Setup(x => x.FindByNameAsync("pepito123"))
                .ReturnsAsync(new Usuario()); // Usuario ya existe

            var service = new AltaProfesor(_userManagerMock.Object);

            // Act
            var resultado = await service.EjecutarAsync(dto);

            // Assert
            Assert.False(resultado.Succeeded);
            Assert.Contains(resultado.Errors, e => e.Code == "DuplicateUserName");
        }

        [Fact]
        public async Task EjecutarAsync_EmailExiste_DevuelveIdentityResultFallido()
        {
            // Arrange
            var dto = new ProfesorAltaDto
            {
                Correo = "pepito@dominio.com",
                NombreUsuario = "pepito123",
                Nombre = "Pepito",
                Apellido = "González",
                Contrasenia = "Abc12345!"
            };

            _userManagerMock.Setup(x => x.FindByNameAsync("pepito123"))
                .ReturnsAsync((Usuario)null!); // Usuario no existe
            _userManagerMock.Setup(x => x.FindByEmailAsync("pepito@dominio.com"))
                .ReturnsAsync(new Usuario()); // Email ya está registrado

            var service = new AltaProfesor(_userManagerMock.Object);

            // Act
            var resultado = await service.EjecutarAsync(dto);

            // Assert
            Assert.False(resultado.Succeeded);
            Assert.Contains(resultado.Errors, e => e.Code == "DuplicateEmail");
        }

        [Fact]
        public async Task EjecutarAsync_DatosValidos_CreaUsuarioYAsignaRol()
        {
            // Arrange
            var dto = new ProfesorAltaDto
            {
                Correo = "ana@ejemplo.com",
                NombreUsuario = "anaprof",
                Nombre = "Ana",
                Apellido = "López",
                Contrasenia = "Secret#123"
            };

            _userManagerMock.Setup(x => x.FindByNameAsync("anaprof"))
                .ReturnsAsync((Usuario)null!);
            _userManagerMock.Setup(x => x.FindByEmailAsync("ana@ejemplo.com"))
                .ReturnsAsync((Usuario)null!);
            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<Usuario>(), "Secret#123"))
                .ReturnsAsync(IdentityResult.Success);
            _userManagerMock.Setup(x => x.AddToRoleAsync(It.IsAny<Usuario>(), "Profesor"))
                .ReturnsAsync(IdentityResult.Success);

            var service = new AltaProfesor(_userManagerMock.Object);

            // Act
            var resultado = await service.EjecutarAsync(dto);

            // Assert
            Assert.True(resultado.Succeeded);
            _userManagerMock.Verify(x => x.CreateAsync(It.IsAny<Usuario>(), "Secret#123"), Times.Once);
            _userManagerMock.Verify(x => x.AddToRoleAsync(It.IsAny<Usuario>(), "Profesor"), Times.Once);
        }
    }


}
