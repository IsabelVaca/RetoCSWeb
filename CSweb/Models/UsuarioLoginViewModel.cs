namespace CSweb.Models;

public class UsuarioLoginViewModel
{
    public int IdUsuario { get; set; }
    public string? UserName { get; set; }
    public string? Nombre { get; set; }
    public string? Email { get; set; }
}

public class LoginApiErrorViewModel
{
    public string? Mensaje { get; set; }
    public string? Error { get; set; }
}
