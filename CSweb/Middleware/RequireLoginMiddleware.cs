using CSweb.Models;

namespace CSweb.Middleware;

// Redirige a Login si no hay sesión activa (excepto Login, IniciarSesion y Error).
public class RequireLoginMiddleware
{
  private readonly RequestDelegate _next;

  public RequireLoginMiddleware(RequestDelegate next) => _next = next;

  public async Task InvokeAsync(HttpContext context)
  {
    if (EsRutaPublica(context))
    {
      await _next(context);
      return;
    }

  // Sin flag de sesión, siempre mostrar login primero.
    var logeado = context.Session.GetInt32(AuthSessionKeys.Logeado) == 1;
    if (!logeado)
    {
      context.Response.Redirect("/Home/Login");
      return;
    }

    await _next(context);
  }

  private static bool EsRutaPublica(HttpContext context)
  {
    var controller = context.GetRouteValue("controller")?.ToString();
    var action = context.GetRouteValue("action")?.ToString();

    if (!string.Equals(controller, "Home", StringComparison.OrdinalIgnoreCase))
      return false;

    // GET del formulario y POST al pulsar Entrar.
    if (string.Equals(action, "Login", StringComparison.OrdinalIgnoreCase)
        || string.Equals(action, "IniciarSesion", StringComparison.OrdinalIgnoreCase)
        || string.Equals(action, "Error", StringComparison.OrdinalIgnoreCase))
      return true;

    return false;
  }
}
