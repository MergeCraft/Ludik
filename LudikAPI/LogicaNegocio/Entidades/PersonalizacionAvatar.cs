using System.ComponentModel.DataAnnotations.Schema;

namespace LogicaNegocio.Entidades;

public class PersonalizacionAvatar: Recompensa
{
    public int AtributoAvatarId { get; set; }

    [ForeignKey(nameof(AtributoAvatarId))]
    public virtual AtributoAvatar AtributoDesbloqueable { get; set; }

    public PersonalizacionAvatar()
    {
        RequiereImagen = true; // Por defecto, las personalizaciones de avatar requieren imagen
    }
}