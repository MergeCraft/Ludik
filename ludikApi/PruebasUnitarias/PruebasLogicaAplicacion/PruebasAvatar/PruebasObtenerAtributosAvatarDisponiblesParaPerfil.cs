using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.AtributoAvatarDTOs;
using LogicaNegocio.Entidades;
using LogicaNegocio.Resultados;
using LogicaAplicacion.ImplementacionCasosUsos.Avatar;
using Moq;
using Xunit;

namespace PruebasUnitarias.PruebasLogicaAplicacion.PruebasAvatar
{
    public class PruebasObtenerAtributosAvatarDisponiblesParaPerfil
    {
        private readonly Mock<IRepositorioPerfilEstudianteGrupo> _mockPerfilRepo;
        private readonly ObtenerAtributosAvatarDisponiblesParaPerfil _casoUso;

        private const int PerfilId = 5;
        private const string UserId = "user-5";

        public PruebasObtenerAtributosAvatarDisponiblesParaPerfil()
        {
            _mockPerfilRepo = new Mock<IRepositorioPerfilEstudianteGrupo>();
            _casoUso = new ObtenerAtributosAvatarDisponiblesParaPerfil(_mockPerfilRepo.Object);
        }

        [Fact]
        public async Task PerfilNoExiste_RetornaNotFound()
        {
            _mockPerfilRepo
                .Setup(r => r.GetByIdAsync(PerfilId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.PerfilEstudiante>.Falla(new Error("Error.NotFound", "Perfil de estudiante no encontrado.")));

            var res = await _casoUso.EjecutarAsync(PerfilId, UserId);

            Assert.True(res.EsFallo);
            Assert.Equal("Error.NotFound", res.Errores.First().Codigo);
            Assert.Contains("Perfil de estudiante no encontrado", res.Errores.First().Mensaje);
        }

        [Fact]
        public async Task UsuarioNoPertenece_RetornaForbidden()
        {
            var perfil = new LogicaNegocio.Entidades.PerfilEstudiante { Id = PerfilId, EstudianteId = "otro-user" };
            _mockPerfilRepo
                .Setup(r => r.GetByIdAsync(PerfilId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.PerfilEstudiante>.Exitoso(perfil));

            var res = await _casoUso.EjecutarAsync(PerfilId, UserId);

            Assert.True(res.EsFallo);
            Assert.Equal("Error.Forbidden", res.Errores.First().Codigo);
        }

        [Fact]
        public async Task CaminoFeliz_RetornaAtributosDtoCorrectos()
        {
            var attrA = new AtributoAvatar { Id = 10, Nombre = "Pelo", Tipo = TipoAtributo.Pelo, RutaRecurso = "r1", CodigoUnico = "c1" };
            var attrB = new AtributoAvatar { Id = 20, Nombre = "Gafas", Tipo = TipoAtributo.Gafas, RutaRecurso = "r2", CodigoUnico = "c2" };
            var pa1 = new PersonalizacionAvatar { AtributoDesbloqueable = attrA };
            var pa2 = new PersonalizacionAvatar { AtributoDesbloqueable = attrB };
            var perfil = new LogicaNegocio.Entidades.PerfilEstudiante
            {
                Id = PerfilId,
                EstudianteId = UserId,
                InventarioRecompensas = new List<PerfilEstudianteRecompensa>
                {
                    new PerfilEstudianteRecompensa { Recompensa = pa1 },
                    new PerfilEstudianteRecompensa { Recompensa = pa2 }
                }
            };

            _mockPerfilRepo
                .Setup(r => r.GetByIdAsync(PerfilId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.PerfilEstudiante>.Exitoso(perfil));

            var res = await _casoUso.EjecutarAsync(PerfilId, UserId);

            Assert.True(res.EsExitoso);
            var dtoList = res.Valor.ToList();
            Assert.Equal(2, dtoList.Count);
            Assert.Contains(dtoList, d => d.Id == 10 && d.CodigoUnico == "c1");
            Assert.Contains(dtoList, d => d.Id == 20 && d.CodigoUnico == "c2");
        }
    }
}
