using System.Linq;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.RecompensaDTOs;
using LogicaAplicacion.ImplementacionCasosUsos.Recompensa;
using LogicaNegocio.Entidades;
using LogicaNegocio.Resultados;
using Moq;
using Xunit;

namespace PruebasUnitarias.PruebasLogicaAplicacion.Recompensa
{
    public class PruebasAltaRecompensa
    {
        private readonly Mock<IRepositorioProfesores> _repoProfesoresMock;
        private readonly AltaRecompensa _casoUso;
        private const string ProfesorId = "prof-1";

        public PruebasAltaRecompensa()
        {
            _repoProfesoresMock = new Mock<IRepositorioProfesores>();
            _casoUso = new AltaRecompensa(_repoProfesoresMock.Object);
        }

        [Fact]
        public async Task EjecutarAsync_DtoNulo_RetornaFalloValidation()
        {
            var resultado = await _casoUso.EjecutarAsync(null, ProfesorId);

            Assert.True(resultado.EsFallo);
            Assert.Contains(resultado.Errores, e =>
                e.Codigo == "Error.Validation" &&
                e.Mensaje.Contains("No hay información"));
        }

        [Fact]
        public async Task EjecutarAsync_ProfesorNoExiste_RetornaNotFound()
        {
            var dtoValido = new RecompensaAltaDto
            {
                Nombre = "RecompensaValida",
                Precio = 10,
                RutaImagenCompleta = "imgCompleta",
                RutaImagenMiniatura = "imgMini"
            };

            _repoProfesoresMock
                .Setup(r => r.GetByStringIdAsync(ProfesorId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Profesor>.Falla(Error.NotFound));

            var resultado = await _casoUso.EjecutarAsync(dtoValido, ProfesorId);

            Assert.True(resultado.EsFallo);
            Assert.Contains(resultado.Errores, e => e.Codigo == "Error.NotFound");
        }

        [Fact]
        public async Task EjecutarAsync_RecompensaDuplicada_RetornaFalloValidationDominio()
        {
            // Creamos un profesor real y le agregamos primero una recompensa con Id=0
            var profesorConRecompensa = new LogicaNegocio.Entidades.Profesor { Id = ProfesorId };
            profesorConRecompensa.CrearRecompensa(new RecompensaSimple
            {
                Id = 0,  // el mapper fromDto también dejará Id=0
                Nombre = "Duplicada",
                NombreImagenCompleta = "x",
                NombreImagenMiniatura = "y",
                Precio = 1
            });

            _repoProfesoresMock
                .Setup(r => r.GetByStringIdAsync(ProfesorId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Profesor>.Exitoso(profesorConRecompensa));

            var dto = new RecompensaAltaDto
            {
                Nombre = "Duplicada",
                Precio = 1,
                RutaImagenCompleta = "u1",
                RutaImagenMiniatura = "u2"
            };

            var resultado = await _casoUso.EjecutarAsync(dto, ProfesorId);

            Assert.True(resultado.EsFallo);
            Assert.Contains(resultado.Errores, e =>
                e.Codigo == "Error.Conflict" ||
                e.Codigo == "Error.Validation");
            // Aseguramos que no se persiste nada:
            _repoProfesoresMock.Verify(r => r.UpdateAsync(It.IsAny<LogicaNegocio.Entidades.Profesor>()), Times.Never);
            // Y la colección sigue con exactamente 1 elemento:
            Assert.Single(profesorConRecompensa.RecompensasCreadas);
        }

        [Fact]
        public async Task EjecutarAsync_AltaCorrecta_RecompensaAgregadaYPersistida()
        {
            // Profesor sin recompensas al inicio
            var profesorVacio = new LogicaNegocio.Entidades.Profesor { Id = ProfesorId };

            _repoProfesoresMock
                .Setup(r => r.GetByStringIdAsync(ProfesorId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Profesor>.Exitoso(profesorVacio));
            _repoProfesoresMock
                .Setup(r => r.UpdateAsync(It.IsAny<LogicaNegocio.Entidades.Profesor>()))
                .ReturnsAsync(Resultado.Exitoso());

            var dto = new RecompensaAltaDto
            {
                Nombre = "Nueva",
                Precio = 5,
                RutaImagenCompleta = "urlCompleta",
                RutaImagenMiniatura = "urlMini"
            };

            var resultado = await _casoUso.EjecutarAsync(dto, ProfesorId);

            Assert.True(resultado.EsExitoso);

            // Ahí mismo, el objeto profesorVacio debe tener 1 nueva creación
            var creadas = profesorVacio.RecompensasCreadas.ToList();
            Assert.Single(creadas);

            var pr = creadas[0];
            // La recompensa asociada
            Assert.NotNull(pr.Recompensa);
            Assert.Equal(dto.Nombre, pr.Recompensa.Nombre);
            Assert.Equal(dto.Precio, pr.Recompensa.Precio);
            Assert.Equal(dto.RutaImagenCompleta, pr.Recompensa.NombreImagenCompleta);
            Assert.Equal(dto.RutaImagenMiniatura, pr.Recompensa.NombreImagenMiniatura);

            // Verificamos que persistió el profesor modificado
            _repoProfesoresMock.Verify(r => r.UpdateAsync(profesorVacio), Times.Once);
        }
    }
}