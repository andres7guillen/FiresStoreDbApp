namespace FireBaseDomain.DTO;

public class UsuarioDto
{
    public string Id { get; set; } = default!;
    public string apellido { get; set; } = string.Empty;

    public string correo { get; set; } = string.Empty;

    public string direccion { get;  set; } = string.Empty;

    public int edad { get; set; }

    public string genero { get; set; } = string.Empty;

    public int idUsuario { get; set; }

    public string jornada { get; set; } = string.Empty;

    public string nombre { get; set; } = string.Empty;

    public int semestre { get; set; }

    public long telefono { get; set; }

    public string universidad { get; set; } = string.Empty;

}
