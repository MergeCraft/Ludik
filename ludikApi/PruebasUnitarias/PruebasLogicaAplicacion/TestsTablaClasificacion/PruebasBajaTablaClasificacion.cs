using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.ImplementacionCasosUsos.TablaClasificacion;
using LogicaNegocio.Resultados;
using Moq;
using Xunit;

namespace PruebasUnitarias.PruebasLogicaAplicacion.TestsTablaClasificacion
{
    public class PruebasBajaTablaClasificacion
    {
        private readonly Mock<IRepositorioTablasClasificacion> _repoTablaMock;
        private readonly BajaTablaClasificacion _casoUso;
        private const int TablaId = 42;

        public PruebasBajaTablaClasificacion()
        {
            _repoTablaMock = new Mock<IRepositorioTablasClasificacion>();
            _casoUso = new BajaTablaClasificacion(_repoTablaMock.Object);
        }

        [Fact]
        public async Task EjecutarAsync_ErrorEnRemoveAsync_RetornaFallo()
        {
            var error = new Error("Error.DB", "Fallo al eliminar");
            _repoTablaMock
                .Setup(r => r.RemoveAsync(TablaId))
                .ReturnsAsync(Resultado.Falla(error));

            var resultado = await _casoUso.EjecutarAsync(TablaId);

            Assert.True(resultado.EsFallo);
            Assert.Contains(resultado.Errores, e => e.Codigo == error.Codigo && e.Mensaje == error.Mensaje);
            _repoTablaMock.Verify(r => r.RemoveAsync(TablaId), Times.Once);
        }

        [Fact]
        public async Task EjecutarAsync_DatosValidos_RetornaExitoso()
        {
            _repoTablaMock
                .Setup(r => r.RemoveAsync(TablaId))
                .ReturnsAsync(Resultado.Exitoso());

            var resultado = await _casoUso.EjecutarAsync(TablaId);

            Assert.True(resultado.EsExitoso);
            _repoTablaMock.Verify(r => r.RemoveAsync(TablaId), Times.Once);
        }
    }
}
