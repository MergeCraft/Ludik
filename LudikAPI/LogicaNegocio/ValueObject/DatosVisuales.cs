namespace LogicaNegocio.ValueObject;


public abstract record DatosVisuales;

public record DatosImagen(string NombreMiniatura, string NombreCompleta) : DatosVisuales;

public record DatosIcono(string NombreIcono) : DatosVisuales;