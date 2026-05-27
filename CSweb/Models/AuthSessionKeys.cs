namespace CSweb.Models;

// Claves de sesión para el login hardcodeado (sin base de datos).
public static class AuthSessionKeys
{
  // Indica que el usuario pasó el login (1 = sí).
  public const string Logeado = "Logeado";
  // Id de usuario demo asignado al iniciar sesión.
  public const string UsuarioId = "UsuarioId";
}
