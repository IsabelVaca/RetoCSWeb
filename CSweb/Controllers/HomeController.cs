using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using CSweb.Models;
using CSweb.Services;

namespace CSweb.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IWebHostEnvironment _env;
    private readonly IPerfilApiService _perfilApi;
    private readonly IHomeApiService _homeApi;
    private readonly IExplorarApiService _explorarApi;
    private readonly IUsuarioAPIService _usuarioService;
    private readonly IPromptService _promptService;

    public HomeController(
        ILogger<HomeController> logger,
        IWebHostEnvironment env,
        IPerfilApiService perfilApi,
        IHomeApiService homeApi,
        IExplorarApiService explorarApi,
        IUsuarioAPIService usuarioService,
        IPromptService promptService)
    {
        _logger = logger;
        _env = env;
        _perfilApi = perfilApi;
        _homeApi = homeApi;
        _explorarApi = explorarApi;
        _usuarioService = usuarioService;
        _promptService = promptService;
    }
    public async Task<IActionResult> Index(string query)
    {
        var taskActivos    = _homeApi.ObtenerUsuariosActivos();
        var taskTendencias = _homeApi.ObtenerTendencias();
        var taskSugeridos  = _homeApi.ObtenerUsuariosSugeridos();
        var taskActividad  = _homeApi.ObtenerActividadesRecientes();
        var taskHoy        = _homeApi.ObtenerPromptsHoy();
        var taskTotales    = _homeApi.ObtenerPromptsTotales();

        await Task.WhenAll(taskActivos, taskTendencias, taskSugeridos,
                        taskActividad, taskHoy, taskTotales);

        var datos = new HomeViewModel
        {
            CreadoresActivos  = taskActivos.Result,
            PostsHoy          = taskHoy.Result,
            MiembrosComunidad = taskTotales.Result,
            Actividades       = taskActividad.Result,
            Tendencias        = taskTendencias.Result,
            Creadores         = taskSugeridos.Result
        };

        if (!string.IsNullOrWhiteSpace(query))
        {
            string texto = query.ToLower();

            datos.Actividades = datos.Actividades
                .Where(a => a.Nombre.ToLower().Contains(texto) ||
                            a.Accion.ToLower().Contains(texto))
                .ToList();

            datos.Creadores = datos.Creadores
                .Where(c => c.Nombre.ToLower().Contains(texto) ||
                            c.Categoria.ToLower().Contains(texto))
                .ToList();

            datos.Tendencias = datos.Tendencias
                .Where(t => t.Nombre.ToLower().Contains(texto))
                .ToList();
        }

        return View(datos);
    }

    public IActionResult Privacy()
    {
        return View();
    }

//RANKING
    [HttpGet]
public async Task<IActionResult> Ranking(string query, string tipo = "general")
{
    if (string.IsNullOrWhiteSpace(tipo))
    {
        tipo = "general";
    }

    int usuarioId = IdSesion();

    List<UsuarioRankingViewModel> listaRanking;

    if (tipo == "likes")
    {
        listaRanking = await _usuarioService.ObtenerRankingLikes();

        listaRanking = listaRanking
            .OrderByDescending(u => u.TotalLikes)
            .ToList();
    }
    else
    {
        listaRanking = await _usuarioService.ObtenerUsuariosRanking();

        listaRanking = listaRanking
            .OrderByDescending(u => u.Puntos)
            .ToList();
    }

    for (int i = 0; i < listaRanking.Count; i++)
    {
        listaRanking[i].Posicion = i + 1;
    }

    var usuarioActual = listaRanking
        .FirstOrDefault(u => u.IDUsuario == usuarioId);

    DetallePuntosViewModel? detallePuntos = null;

    if (usuarioActual != null && tipo == "general")
    {
        detallePuntos = await _usuarioService.ObtenerDetallePuntosUsuario(usuarioActual.IDUsuario);
    }

    if (!string.IsNullOrWhiteSpace(query))
    {
        string texto = query.ToLower();

        listaRanking = listaRanking
            .Where(u =>
                u.UserName.ToLower().Contains(texto) ||
                u.NombreUsuario.ToLower().Contains(texto)
            )
            .ToList();
    }

    var datos = new RankingViewModel
    {
        Usuarios = listaRanking,
        UsuarioActual = usuarioActual,
        DetallePuntosUsuario = detallePuntos,
        TipoRanking = tipo,
        Query = query ?? ""
    };

    return View(datos);
}
//FIN RANKING

//EXPLORAR

    [HttpGet]
    public async Task<IActionResult> Explorar(string query, string filtro)
    {
        // si no seleccionó filtro, se usa Tendencias
        if (string.IsNullOrEmpty(filtro))
        {
            filtro = "Tendencias";
        }

        // usuario actual simulado
        int idUsuario = 1;

        // se llama al API y llama al SP_ObtenerPromptsExplorar
        // y despues regresa los prompts ordenados según el filtro
        var listaPrompts = await _explorarApi.ObtenerPromptsAsync(
            query ?? "",
            idUsuario,
            filtro
        );

        if (listaPrompts == null) //usa lista vacia 
        {
            listaPrompts = new List<PromptViewModel>();
        }

        // calcula tiempop relativo
        foreach (var prompt in listaPrompts)
        {
            prompt.CreatedAt = CalcularTiempoRelativo(prompt.FechaPublicacion);
        }

        //crea modelo de pagina 
        var datos = new ExplorarViewModel
        {
            Prompts = listaPrompts,
            Filters = new List<string>
            {
                "Tendencias",
                "Recientes",
                "Más Gustados",
                "Más Guardados"
            },
            Query = query ?? "",
            FiltroActivo = filtro
        };

        return View(datos);
    }

    [HttpPost]
    public async Task<IActionResult> ToggleLike(int promptId, string query, string filtro)
    {
        int idUsuario = 1;

        await _explorarApi.ToggleLikeAsync(promptId, idUsuario); //llama al servicio para mandarlo al API

        return RedirectToAction("Explorar", new //recarga pagina para ver cambio 
        {
            query = query,
            filtro = filtro
        });
    }

    [HttpPost]
    public async Task<IActionResult> ToggleGuardar(int promptId, string query, string filtro)
    {
        int idUsuario = 1;

        await _explorarApi.ToggleGuardarAsync(promptId, idUsuario);

        return RedirectToAction("Explorar", new
        {
            query = query,
            filtro = filtro
        });
    }

    [HttpPost]
    public async Task<IActionResult> PublicarComentario(int promptId, string comentario, string query, string filtro)
    {
        int idUsuario = 1;

        await _explorarApi.PublicarComentarioAsync(
            promptId,
            idUsuario,
            comentario ?? ""
        );

        return RedirectToAction("Explorar", new
        {
            query = query,
            filtro = filtro
        });
    }

//FIN EXPLORAR

//TIEMPO EXPLORAR
private static string CalcularTiempoRelativo(string? fechaTexto)
{
    if (string.IsNullOrWhiteSpace(fechaTexto))
    {
        return "fecha no disponible";
    }

    if (!DateTime.TryParse(fechaTexto, out DateTime fecha))
    {
        return fechaTexto;
    }

    var diferencia = DateTime.Now - fecha;

    if (diferencia.TotalMinutes < 1)
    {
        return "hace unos segundos";
    }

    if (diferencia.TotalMinutes < 60)
    {
        return $"hace {(int)diferencia.TotalMinutes} minutos";
    }

    if (diferencia.TotalHours < 24)
    {
        return $"hace {(int)diferencia.TotalHours} horas";
    }

    if (diferencia.TotalDays < 7)
    {
        return $"hace {(int)diferencia.TotalDays} días";
    }

    return fecha.ToString("dd/MM/yyyy");
}
//FIN EXPLORAR TIEMPO

//LOGIN
    // Pantalla de login (primera página al iniciar la app).
    [HttpGet]
    public IActionResult Login()
    {
        if (HttpContext.Session.GetInt32(AuthSessionKeys.Logeado) == 1)
            return RedirectToAction(nameof(Index));

        return View(new LoginViewModel());
    }

    // Valida admin1 / 12345 y entra como el usuario con id 1.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult IniciarSesion(LoginViewModel model)
    {
        var usuarioOk = string.Equals(model.Usuario?.Trim(), LoginViewModel.UsuarioDemo, StringComparison.OrdinalIgnoreCase);
        var passwordOk = model.Password == LoginViewModel.PasswordDemo;

        if (usuarioOk && passwordOk)
        {
            HttpContext.Session.SetInt32(AuthSessionKeys.Logeado, 1);
            HttpContext.Session.SetInt32(AuthSessionKeys.UsuarioId, LoginViewModel.UsuarioPorDefectoId);
            return RedirectToAction(nameof(Index));
        }

        model.Error = "Usuario o contraseña incorrectos.";
        return View(nameof(Login), model);
    }

    // Cierra sesión y vuelve al login.
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction(nameof(Login));
    }
//FIN LOGIN

//CREAR
    [HttpGet]
public async Task<IActionResult> Crear()
{
    PromptViewModelCrear vm = new PromptViewModelCrear();

    vm.Categorias = await _promptService.ObtenerCategoriasPrompt();
    vm.Tips = await _promptService.ObtenerTipsPrompt();
    vm.ConsejosRapidos = await _promptService.ObtenerConsejosRapidosPrompt();

    return View(vm);
}

[HttpPost]
public async Task<IActionResult> Crear(PromptViewModelCrear vm)
{
    int idUsuario = IdSesion();

    bool resultado = await _promptService.CrearPrompt(vm, idUsuario);

    vm.Categorias = await _promptService.ObtenerCategoriasPrompt();
    vm.Tips = await _promptService.ObtenerTipsPrompt();
    vm.ConsejosRapidos = await _promptService.ObtenerConsejosRapidosPrompt();

    if (resultado)
    {
        ViewData["Mensaje"] = "Se guardó correctamente tu prompt";
    }
    else
    {
        ViewData["Error"] = "No se pudo guardar el prompt";
    }

    return View(vm);
}

    // PERFIL
    private static readonly (string Id, string Icon, string Label)[] PerfilTabsNav =
    {
        ("publicados", "bi-grid-3x3-gap", "Publicados"),
        ("guardados", "bi-bookmark", "Guardados"),
        ("likeados", "bi-heart", "Likeados"),
        ("actividad", "bi-lightning-charge", "Actividad"),
    };

    // Ver perfil (GET)
    public async Task<IActionResult> Perfil()
    {
        var id = IdSesion();
        var tab = Tab(Request.Query["tab"].FirstOrDefault());
        var apiTab = tab == "editar" ? "publicados" : tab;
        var vm = await CargarDesdeApi(id, apiTab);
        PrepararVista(vm, tab == "editar" ? "editar" : tab);
        return View(vm);
    }

    // POST al MVC; un solo PUT /perfil/{id} a Flask (datos y rutaFotoPerfil)
    [HttpPost]
    public async Task<IActionResult> EditarPerfil(PerfilEditarViewModel model)
    {
        var id = IdSesion();
        if (string.IsNullOrWhiteSpace(model.Nombre))
            ModelState.AddModelError(nameof(model.Nombre), "El nombre es obligatorio.");
        if (string.IsNullOrWhiteSpace(model.UserName))
            ModelState.AddModelError(nameof(model.UserName), "El usuario es obligatorio.");

        if (model.FotoPerfil is { Length: > 0 })
        {
            model.RutaFotoPerfil = await GuardarFotoEnDisco(model.FotoPerfil, id);
            if (model.RutaFotoPerfil == null)
                ModelState.AddModelError(nameof(model.FotoPerfil), "Extensiones NO permitidas. Usa .jpg, .jpeg, .png, .gif.");
        }

        if (!ModelState.IsValid)
            return await VistaEditar(id, model);

        var rutaApi = string.IsNullOrWhiteSpace(model.RutaFotoPerfil)
            ? ""
            : RutaParaApi(model.RutaFotoPerfil);

        if (!await _perfilApi.ActualizarPerfilAsync(id, model.Nombre, model.UserName, model.Bio ?? "", rutaApi))
        {
            ModelState.AddModelError(string.Empty, "No se pudo actualizar el perfil en la API.");
            return await VistaEditar(id, model);
        }

        TempData["PerfilMensaje"] = "Perfil actualizado correctamente.";
        return RedirectToAction(nameof(Perfil));
    }

    // Solo POST al MVC
    [HttpPost]
    public IActionResult PublicarComentarioPerfil(int promptId, string comentario, string? returnTab)
    {
        TempData["PerfilMensaje"] = MensajeComentario(comentario);
        return RedirectToAction(nameof(Perfil), new { tab = string.IsNullOrWhiteSpace(returnTab) ? "publicados" : returnTab });
    }

    // Sesión y pestaña
    private int IdSesion()
    {
        return HttpContext.Session.GetInt32(AuthSessionKeys.UsuarioId) ?? LoginViewModel.UsuarioPorDefectoId;
    }

    private static string Tab(string? t) =>
        string.IsNullOrWhiteSpace(t) || t.Equals("publicaciones", StringComparison.OrdinalIgnoreCase) ? "publicados" : t;

    // Llamada API y ViewBag 

    private async Task<PerfilViewModel> CargarDesdeApi(int id, string tab)
    {
        var (cab, lista) = await _perfilApi.ObtenerPerfilAsync(id, tab, id);
        if (cab.Count == 0)
        {
            TempData["PerfilError"] = "No se pudieron cargar los datos del perfil. ¿Está la API en http://127.0.0.1:8001?";
            return new PerfilViewModel();
        }
        return AModelo(cab[0], lista, tab);
    }

    private void PrepararVista(PerfilViewModel p, string tab)
    {
        ViewBag.Tab = tab;
        ViewBag.EnEditar = tab == "editar";
        ViewBag.EnGuardados = tab == "guardados";
        ViewBag.EnLikeados = tab == "likeados";
        ViewBag.EnActividad = tab == "actividad";
        ViewBag.TabsNav = PerfilTabsNav;
        ViewBag.Prompts = tab == "guardados" ? p.Guardados : tab == "likeados" ? p.Likeados : p.Publicados;

        var ed = new PerfilEditarViewModel { Nombre = p.Nombre, UserName = p.UserName, Bio = p.Bio, RutaFotoPerfil = p.ImagenPerfil };
        ViewBag.PerfilEditar = ed;
        var id = IdSesion();
        ViewBag.UrlFotoCabecera = RutaImagen(p.ImagenPerfil, id);
        ViewBag.UrlFotoEditar = RutaImagen(ed.RutaFotoPerfil, id);
    }

    private async Task<IActionResult> VistaEditar(int id, PerfilEditarViewModel m)
    {
        var p = await CargarDesdeApi(id, "publicados");
        p.Nombre = m.Nombre;
        p.UserName = m.UserName;
        p.Bio = m.Bio ?? "";
        if (!string.IsNullOrWhiteSpace(m.RutaFotoPerfil)) p.ImagenPerfil = m.RutaFotoPerfil.Trim();
        ViewBag.PerfilEditar = m;
        PrepararVista(p, "editar");
        return View("Perfil", p);
    }

    private static string RutaParaApi(string ruta) =>
        (ruta.Trim().StartsWith('/') ? ruta.Trim() : "/" + ruta.Trim())
            .Replace("/Imagenes/", "/imagenes/", StringComparison.OrdinalIgnoreCase);

    private static PerfilViewModel AModelo(Dictionary<string, object> cab, List<Dictionary<string, object>> lista, string tab)
    {
        var p = new PerfilViewModel
        {
            Nombre = S(cab, "nombre"),
            UserName = S(cab, "userName"),
            Bio = S(cab, "bio"),
            ImagenPerfil = SNull(cab, "imagenPerfil"),
            Correo = S(cab, "correo"),
            NumeroPublicaciones = N(cab, "numeroPublicaciones"),
        };
        if (tab == "actividad") p.Actividad = lista.Select(Actividad).ToList();
        else if (tab == "guardados") p.Guardados = lista.Select(APrompt).ToList();
        else if (tab == "likeados") p.Likeados = lista.Select(APrompt).ToList();
        else p.Publicados = lista.Select(APrompt).ToList();
        return p;
    }

    private static PromptViewModel APrompt(Dictionary<string, object> d) => new()
    {
        Id = N(d, "id"), IdUsuario = N(d, "idUsuario"), Title = S(d, "title"), Prompt = S(d, "prompt"),
        AuthorName = S(d, "authorName"), Username = S(d, "username"), InitialsProfile = S(d, "initialsProfile"),
        CircleColor = S(d, "circleColor", "#104B70"), Category = S(d, "category"),
        Likes = N(d, "likes"), Comments = N(d, "comments"), Saves = N(d, "saves"),
        CreatedAt = S(d, "fechaPublicacion"), Trending = N(d, "trending"),
    };

    private static PerfilActividadItemViewModel Actividad(Dictionary<string, object> d) => new()
    {
        Tipo = S(d, "tipo"), ActorNombre = S(d, "actorNombre"), ActorUserName = S(d, "actorUserName"),
        TituloPrompt = S(d, "tituloPrompt"), Momento = S(d, "fecha"), ExtractoComentario = SNull(d, "extractoComentario"),
    };

    private static string S(Dictionary<string, object> d, string k, string def = "") =>
        d.TryGetValue(k, out var v) ? v?.ToString() ?? def : def;

    private static string? SNull(Dictionary<string, object> d, string k) =>
        d.TryGetValue(k, out var v) && v != null && v.ToString() != "" ? v.ToString() : null;

    private static int N(Dictionary<string, object> d, string k) =>
        d.TryGetValue(k, out var v) ? v switch
        {
            int i => i, long l => (int)l, double x => (int)x,
            _ => int.TryParse(v.ToString(), out var n) ? n : 0
        } : 0;

    // --- Foto en disco ---
    private string RutaImagen(string? rutaApi, int id)
    {
        const string porDefecto = "/Imagenes/fotosperfil/trabajador.jpg";
        string? rel = null;
        if (!string.IsNullOrWhiteSpace(rutaApi))
        {
            rel = rutaApi.Trim().Replace("/imagenes/", "/Imagenes/", StringComparison.OrdinalIgnoreCase);
            if (!rel.StartsWith('/')) rel = "/" + rel;
            if (!System.IO.File.Exists(Path.Combine(_env.WebRootPath, rel.TrimStart('/').Replace('/', Path.DirectorySeparatorChar))))
                rel = null;
        }
        if (rel != null) return Url.Content("~" + rel)!;
        var dir = Path.Combine(_env.WebRootPath, "Imagenes", "fotosperfil");
        if (!Directory.Exists(dir)) return Url.Content("~" + porDefecto)!;
        var ultima = Directory.GetFiles(dir, $"*FotoUsuario{id}*").OrderByDescending(System.IO.File.GetLastWriteTimeUtc).FirstOrDefault();
        rel = ultima != null ? $"/Imagenes/fotosperfil/{Path.GetFileName(ultima)}" : porDefecto;
        return Url.Content("~" + rel)!;
    }

    private static string MensajeComentario(string t)
    {
        if (string.IsNullOrWhiteSpace(t)) return "Escribe un comentario antes de publicar.";
        string[] malas = [];
        return malas.Any(p => t.Contains(p, StringComparison.OrdinalIgnoreCase))
            ? "Tu comentario contiene palabras no permitidas. Por favor, sé respetuoso."
            : "¡Comentario publicado con éxito!";
    }

    private async Task<string?> GuardarFotoEnDisco(IFormFile foto, int id)
    {
        var ext = Path.GetExtension(foto.FileName).ToLowerInvariant();
        if (!".jpg,.jpeg,.png,.gif".Split(',').Contains(ext)) return null;
        var dir = Path.Combine(_env.WebRootPath, "Imagenes", "fotosperfil");
        Directory.CreateDirectory(dir);
        var nombre = $"{Guid.NewGuid()}_FotoUsuario{id}{ext}";
        await using var stream = new FileStream(Path.Combine(dir, nombre), FileMode.Create);
        await foto.CopyToAsync(stream);
        return $"/Imagenes/fotosperfil/{nombre}";
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    //TIENDA
    public IActionResult Tienda()
{
    return View();
}

//JUEGOS
    public IActionResult Juego()
    {
        return View();


    }
}