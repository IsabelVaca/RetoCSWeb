namespace CSweb.Models;

// Formulario de login demo; la sesión usa siempre el usuario con id 1.
public class LoginViewModel
{
  // Id fijo tras iniciar sesión (perfil, ranking, etc.).
  public const int UsuarioPorDefectoId = 1;

  // Credenciales demo precargadas en el formulario.
  public const string UsuarioDemo = "admin1";
  public const string PasswordDemo = "12345";

  public string Usuario { get; set; } = UsuarioDemo;
  public string Password { get; set; } = PasswordDemo;
  public string? Error { get; set; }
}
