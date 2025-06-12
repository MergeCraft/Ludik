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

namespace PruebasUnitarias.PruebasLogicaAplicacion.Profesor
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
        public async Task EjecutarAsync_DtoNulo_DevuelveResultadoFallido()
        {
            // Arrange
            var service = new AltaProfesor(_userManagerMock.Object);
            ProfesorAltaDto dto = null!;

            // Act
            var resultado = await service.EjecutarAsync(dto);

            // Assert
            Assert.False(resultado.EsExitoso);
            Assert.Contains(resultado.Errores, e => e.Codigo == "Error.Validation");
            Assert.Contains(resultado.Errores, e => e.Mensaje.Contains("no pueden ser nulos"));
        }

        //[Fact]
        //public async Task EjecutarAsync_NombreUsuarioExiste_DevuelveResultadoFallido()
        //{
        //    // Arrange
        //    var dto = new ProfesorAltaDto
        //    {
        //        Correo = "pepito@dominio.com",
        //        NombreUsuario = "pepito123",
        //        Nombre = "Pepito",
        //        Apellido = "González",
        //        Contrasenia = "Abc12345!"
        //    };

        //    _userManagerMock.Setup(x => x.FindByNameAsync("pepito123"))
        //        .ReturnsAsync(new Usuario()); // Usuario ya existe

        //    var service = new AltaProfesor(_userManagerMock.Object);

        //    // Act
        //    var resultado = await service.EjecutarAsync(dto);

        //    // Assert
        //    Assert.False(resultado.EsExitoso);
        //    Assert.Contains(resultado.Errores, e => e.Codigo == "Conflict");
        //    Assert.Contains(resultado.Errores, e => e.Mensaje.Contains("El nombre de usuario ya está en uso."));
        //}

        //[Fact]
        //public async Task EjecutarAsync_EmailExiste_DevuelveResultadoFallido()
        //{
        //    // Arrange
        //    var dto = new ProfesorAltaDto
        //    {
        //        Correo = "pepito@dominio.com",
        //        NombreUsuario = "pepito123",
        //        Nombre = "Pepito",
        //        Apellido = "González",
        //        Contrasenia = "Abc12345!"
        //    };

        //    _userManagerMock.Setup(x => x.FindByNameAsync("pepito123"))
        //        .ReturnsAsync((Usuario)null!);
        //    _userManagerMock.Setup(x => x.FindByEmailAsync("pepito@dominio.com"))
        //        .ReturnsAsync(new Usuario()); // Email ya registrado

        //    var service = new AltaProfesor(_userManagerMock.Object);

        //    // Act
        //    var resultado = await service.EjecutarAsync(dto);

        //    // Assert
        //    Assert.False(resultado.EsExitoso);
        //    Assert.Contains(resultado.Errores, e => e.Codigo == "Conflict");
        //    Assert.Contains(resultado.Errores, e =>
        //    e.Mensaje.Contains("correo electrónico ya está en uso", StringComparison.OrdinalIgnoreCase));
        //}

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
            Assert.True(resultado.EsExitoso);
            _userManagerMock.Verify(x => x.CreateAsync(It.IsAny<Usuario>(), "Secret#123"), Times.Once);
            _userManagerMock.Verify(x => x.AddToRoleAsync(It.IsAny<Usuario>(), "Profesor"), Times.Once);
        }
        [Fact]
        public async Task EjecutarAsync_CreacionUsuarioFalla_DevuelveErroresDeIdentity()
        {
            // Arrange
            var dto = new ProfesorAltaDto
            {
                Correo = "nuevo@correo.com",
                NombreUsuario = "nuevoUser",
                Nombre = "Nuevo",
                Apellido = "Apellido",
                Contrasenia = "123" // débil
            };

            _userManagerMock.Setup(x => x.FindByNameAsync(dto.NombreUsuario))
                .ReturnsAsync((Usuario)null!);
            _userManagerMock.Setup(x => x.FindByEmailAsync(dto.Correo))
                .ReturnsAsync((Usuario)null!);
            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<Usuario>(), dto.Contrasenia))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "La contraseña es muy débil." }));

            var service = new AltaProfesor(_userManagerMock.Object);

            // Act
            var resultado = await service.EjecutarAsync(dto);

            // Assert
            Assert.False(resultado.EsExitoso);
            Assert.Contains(resultado.Errores, e => e.Codigo == "Error.Validation");
            Assert.Contains(resultado.Errores, e => e.Mensaje.Contains("débil"));
        }
        [Fact]
        public async Task EjecutarAsync_AsignacionRolFalla_EliminaUsuarioYDevuelveError()
        {
            // Arrange
            var dto = new ProfesorAltaDto
            {
                Correo = "nuevo@correo.com",
                NombreUsuario = "nuevoUser",
                Nombre = "Nuevo",
                Apellido = "Apellido",
                Contrasenia = "Abc12345!"
            };

            _userManagerMock.Setup(x => x.FindByNameAsync(dto.NombreUsuario))
                .ReturnsAsync((Usuario)null!);
            _userManagerMock.Setup(x => x.FindByEmailAsync(dto.Correo))
                .ReturnsAsync((Usuario)null!);
            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<Usuario>(), dto.Contrasenia))
                .ReturnsAsync(IdentityResult.Success);
            _userManagerMock.Setup(x => x.AddToRoleAsync(It.IsAny<Usuario>(), "Profesor"))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Rol inválido" }));
            _userManagerMock.Setup(x => x.DeleteAsync(It.IsAny<Usuario>()))
                .ReturnsAsync(IdentityResult.Success);

            var service = new AltaProfesor(_userManagerMock.Object);

            // Act
            var resultado = await service.EjecutarAsync(dto);

            // Assert
            Assert.False(resultado.EsExitoso);
            Assert.Contains(resultado.Errores, e => e.Codigo == "Error.Unexpected");
            Assert.Contains(resultado.Errores, e => e.Mensaje.Contains("asignar rol"));
            _userManagerMock.Verify(x => x.DeleteAsync(It.IsAny<Usuario>()), Times.Once);
        }
    }
    //faltan una pruebas mas que tiene que ver con el chequeo de cada campo


}
