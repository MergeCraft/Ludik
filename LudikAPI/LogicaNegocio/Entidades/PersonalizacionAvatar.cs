using LogicaNegocio.EntidadesAuxiliares;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Resultados;
using System.ComponentModel.DataAnnotations.Schema;
using LogicaNegocio.InterfacesEntidades;

namespace LogicaNegocio.Entidades;

public class PersonalizacionAvatar: Recompensa
{
    public int AtributoAvatarId { get; set; }

    public override RepresentacionVisualBase Representacion { get; protected set; }

    [ForeignKey(nameof(AtributoAvatarId))]
    public virtual AtributoAvatar AtributoDesbloqueable { get; set; }


    public PersonalizacionAvatar()
    {
        Representacion = new RepresentacionImagen();
    }


    public override Resultado Otorgar(PerfilEstudiante perfil)
    {
        throw new NotImplementedException();
    }
}