using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebasUnitarias.PruebasLogicaNegocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Xunit;
    using LogicaNegocio.ValueObjects;
    using Dominio;
    using InterfacesRepositorio;
    using LogicaAplicacion.DTOs.UsuarioDTOs;
    using LogicaAplicacion.ImplementacionCasosUsos.Usuarios;
    using Moq;

    namespace PruebasUnitarias.PruebasLogicaDominio
    {
        public class PruebasLogin
        {
            private readonly Mock<IRepositorioUsuarios> _mockRepo;
            private readonly LoginPrueba _loginPrueba;

            public PruebasLogin()
            {
                _mockRepo = new Mock<IRepositorioUsuarios>();
                _loginPrueba = new LoginPrueba(_mockRepo.Object);
            }

            // --- VerificarContrasenia: casos éxito/error ---

            [Fact]
            public void VerificarContrasenia_ReturnsTrue_WhenPasswordMatchesHash()
            {
                // Arrange


                // Act


                // Assert

            }

            [Fact]
            public void VerificarContrasenia_ReturnsFalse_WhenPasswordDoesNotMatchHash()
            {
                // Arrange


                // Act


                // Assert

            }

            // --- Ejecutar: caso de éxito ---

            [Fact]
            public void Ejecutar_ReturnsDto_WhenUserExistsAndPasswordCorrect()
            {
                // Arrange


                // Configuramos el ValueObject Contrasenia con el hash generado


                // Nota: el mapper tomará cualquier propiedad (por ejemplo NombreUsuario, Rol, etc.)
                // como valor por defecto (null o 0). Lo importante es que no arroje excepción.
                // Si tu mapper lee otras propiedades, no olvides configurarlas aquí con Setup.
                // Ejemplo:
                // usuarioMock.Setup(u => u.Id).Returns(Guid.NewGuid());
                // usuarioMock.Setup(u => u.NombreUsuario).Returns("usuario1");
                // usuarioMock.Setup(u => u.Rol).Returns(Rol.Administrador);



                // Act

                // Assert

                // Como no sabemos exactamente qué hace el mapper, al menos comprobamos que no sea null.
            }

            // --- Ejecutar: caso de error por usuario inexistente ---

            [Fact]
            public void Ejecutar_ReturnsNull_WhenUserNotFound()
            {
                // Arrange


                // Act

                // Assert

            }

            // --- Ejecutar: caso de error por contraseña incorrecta ---

            [Fact]
            public void Ejecutar_ReturnsNull_WhenPasswordIncorrect()
            {
                // Arrange


                // Usamos una contraseña que no coincida con el hash


                // Act


                // Assert

            }

            // --- Ejecutar: caso borde (strings vacíos) ---

            [Fact]
            public void Ejecutar_ReturnsNull_WhenEmptyUsernameAndPasswordProvided()
            {
                // Arrange
              

                // Incluso si el repositorio devuelve un IRepositorioUsuarios, al pasar "" como password,
                // VerificarContrasenia terminará en falso (no coincide con el hash generado).


                // Act


                // Assert
;
            }
        }
    }
}
