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

    namespace LudikApi.Tests
    {
        public class ValidationTests
        {
            private static IList<ValidationResult> Validate(object entity)
            {
                var ctx = new ValidationContext(entity);
                var list = new List<ValidationResult>();
                Validator.TryValidateObject(entity, ctx, list, validateAllProperties: true);
                return list;
            }

            [Fact]
            public void NombreCompleto_InvalidCharacters_ProducesError()
            {
                var nombre = new NombreCompleto("J0sé!", "Pérez");
                var errors = Validate(nombre);

                Assert.Contains(errors, e => e.ErrorMessage.Contains("solo puede contener letras"));
            }

            [Fact]
            public void Contrasenia_TooSimple_ProducesError()
            {
                var pwd = new Contrasenia("abc123");
                var errors = Validate(pwd);

                Assert.Contains(errors, e => e.ErrorMessage.Contains("La contraseña debe tener al menos 8 caracteres"));
            }
            [Fact]
            public void NombreCompleto_ValidValues_NoErrors()
            {
                var nombre = new NombreCompleto("José", "Pérez");
                var errors = Validate(nombre);

                Assert.Empty(errors);
            }

            [Fact]
            public void Contrasenia_ComplexEnough_NoErrors()
            {
                // Al menos 8 caracteres, una mayúscula, una minúscula, un número y un símbolo
                var pwd = new Contrasenia("Abcdef1!");
                var errors = Validate(pwd);

                Assert.Empty(errors);
            }

            [Fact]
            public void Email_ValidFormat_NoErrors()
            {
                var email = new Email("usuario.prueba-123@mail.com");
                var errors = Validate(email);

                Assert.Empty(errors);
            }
        }
    }
}
