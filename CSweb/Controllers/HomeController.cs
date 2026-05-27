using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using CSweb.Models;
using CSweb.Services;
//.
namespace CSweb.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IWebHostEnvironment _env;
    private readonly IPerfilApiService _perfilApi;

    public HomeController(
        ILogger<HomeController> logger,
        IWebHostEnvironment env,
        IPerfilApiService perfilApi)
    {
        _logger = logger;
        _env = env;
        _perfilApi = perfilApi;
    }

    public IActionResult Index(string query)
    {
        var datos = new HomeViewModel
        {
            // Poner API stuff
            // Obtener elo de abajo
            CreadoresActivos = 1200,
            PostsHoy = 5800,
            MiembrosComunidad = 23000,
            Actividades = new List<HomeActividadViewModel>
            {
                new HomeActividadViewModel
                {
                    Nombre = "Alex Rivera",
                    Accion = "publicó un nuevo diseño",
                    Tiempo = "hace 2 min",
                    Likes = 124,
                    Comentarios = 18,
                    ImagenPerfil = "/Imagenes/1.png",
                    ImagenPreview = "/Imagenes/2.png"
                },
                new HomeActividadViewModel
                {
                    Nombre = "Sarah Chen",
                    Accion = "le gustó una publicación",
                    Tiempo = "hace 5 min",
                    Likes = 89,
                    Comentarios = 12,
                    ImagenPerfil = "/Imagenes/3.png",
                    ImagenPreview = "/Imagenes/4.png"
                },
                new HomeActividadViewModel
                {
                    Nombre = "Marcus James",
                    Accion = "comentó en tu publicación",
                    Tiempo = "hace 12 min",
                    Likes = 156,
                    Comentarios = 24,
                    ImagenPerfil = "/Imagenes/5.png",
                    ImagenPreview = "/Imagenes/6.png"
                }

            },
            Tendencias = new List<HomeTendenciaViewModel>
            {
                new HomeTendenciaViewModel
                {
                    Nombre = "Estilos de Marketing",
                    Posts = 1234
                },
                new HomeTendenciaViewModel
                {
                    Nombre = "Gráficos Financieros",
                    Posts = 892
                },
                new HomeTendenciaViewModel
                {
                    Nombre = "Plantillas",
                    Posts = 567
                },
                new HomeTendenciaViewModel
                {
                    Nombre = "Herramientas Modernas",
                    Posts = 445
                }
            },
            Creadores = new List<HomeCreadorViewModel>
            {
                new HomeCreadorViewModel
                {
                    Nombre = "Jordan Smith",
                    Categoria = "Arte Digital",
                    Imagen = "/Imagenes/1.png",
                    Seguidores = "2.8k"
                },

                new HomeCreadorViewModel
                {
                    Nombre = "Maya Rodriguez",
                    Categoria = "Fotografía",
                    Imagen = "/Imagenes/2.png",
                    Seguidores = "3.2k"
                },

                new HomeCreadorViewModel
                {
                    Nombre = "Chris Lee",
                    Categoria = "Diseño 3D",
                    Imagen = "/Imagenes/3.png",
                    Seguidores = "1.9k"
                }
            }
        };
        // filtro local temporal
        // Poner API stuff
        // Flask deberá regresar resultados filtrados

        if (!string.IsNullOrWhiteSpace(query))
        {
            string texto = query.ToLower();

            datos.Actividades = datos.Actividades
                .Where(a =>
                    a.Nombre.ToLower().Contains(texto) ||
                    a.Accion.ToLower().Contains(texto))
                .ToList();

            datos.Creadores = datos.Creadores
                .Where(c =>
                    c.Nombre.ToLower().Contains(texto) ||
                    c.Categoria.ToLower().Contains(texto))
                .ToList();

            datos.Tendencias = datos.Tendencias
                .Where(t =>
                    t.Nombre.ToLower().Contains(texto))
                .ToList();
        }
        return View(datos);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Ranking(string query)
    {

        // Id de sesión tras login hardcodeado.
        var usuarioId = IdSesion();

        //lista de ranking
        var lista_usuarios_ranking = new List<UsuarioRankingViewModel>
        {

            new UsuarioRankingViewModel { Id = 1, userName = "CodeMaster21", Puntos = 950, imagen = "/Imagenes/1.png" },
            new UsuarioRankingViewModel { Id = 2, userName = "PixelNinja", Puntos = 870, imagen = "/Imagenes/2.png" },
            new UsuarioRankingViewModel { Id = 3, userName = "DevQueen", Puntos = 990, imagen = "/Imagenes/3.png" },
            new UsuarioRankingViewModel { Id = 4, userName = "BugHunter", Puntos = 760, imagen = "/Imagenes/4.png" },
            new UsuarioRankingViewModel { Id = 5, userName = "ScriptWizard", Puntos = 820, imagen = "/Imagenes/5.png" },
            new UsuarioRankingViewModel { Id = 6, userName = "DarkCoder", Puntos = 910, imagen = "/Imagenes/6.png" },
            new UsuarioRankingViewModel { Id = 7, userName = "FrontendHero", Puntos = 880, imagen = "/Imagenes/1.png" },
            new UsuarioRankingViewModel { Id = 8, userName = "BackendBoss", Puntos = 940, imagen = "/Imagenes/2.png" },
            new UsuarioRankingViewModel { Id = 9, userName = "AIExplorer", Puntos = 970, imagen = "/Imagenes/3.png" },
            new UsuarioRankingViewModel { Id = 10, userName = "DataKnight", Puntos = 860, imagen = "/Imagenes/4.png" },
            new UsuarioRankingViewModel { Id = 11, userName = "CyberSamurai", Puntos = 830, imagen = "/Imagenes/5.png" },
            new UsuarioRankingViewModel { Id = 12, userName = "CloudRider", Puntos = 780, imagen = "/Imagenes/6.png" },
            new UsuarioRankingViewModel { Id = 13, userName = "NeonDev", Puntos = 920, imagen = "/Imagenes/1.png" },
            new UsuarioRankingViewModel { Id = 14, userName = "LogicLover", Puntos = 800, imagen = "/Imagenes/2.png" },
            new UsuarioRankingViewModel { Id = 15, userName = "StackOverlord", Puntos = 890, imagen = "/Imagenes/3.png" },
            new UsuarioRankingViewModel { Id = 16, userName = "BinaryBeast", Puntos = 840, imagen = "/Imagenes/4.png" },
            new UsuarioRankingViewModel { Id = 17, userName = "QuantumCoder", Puntos = 960, imagen = "/Imagenes/5.png" },
            new UsuarioRankingViewModel { Id = 18, userName = "DebugDiva", Puntos = 870, imagen = "/Imagenes/6.png" },
            new UsuarioRankingViewModel { Id = 19, userName = "CodePhantom", Puntos = 910, imagen = "/Imagenes/1.png" },
            new UsuarioRankingViewModel { Id = 20, userName = "AlgoMaster", Puntos = 930, imagen = "/Imagenes/2.png" }

        };
        //ordenar lista
        var lista_ranking_ordenada = lista_usuarios_ranking.OrderByDescending(u => u.Puntos).ToList();

        //dar posiciones 
        for (int i = 0; i < lista_ranking_ordenada.Count; i++)
        {
            lista_ranking_ordenada[i].Posicion = i + 1;
        }

       
        //objeto usuario actual, obtenido de la lista ordenada con pos
        var usuario_actual = lista_ranking_ordenada
            .FirstOrDefault(u => u.Id == usuarioId);

        //si el usuario escribió algo en searchbar
        if (!string.IsNullOrEmpty(query))
        {
            lista_ranking_ordenada = lista_ranking_ordenada
                .Where(u => u.userName.ToLower().Contains(query.ToLower()))
                .ToList();
        }

       
        //regresar datos a la vista 
        var datos = new RankingViewModel
        {
            Usuarios = lista_ranking_ordenada,
            UsuarioActual = usuario_actual
        };
    return View(datos);
}    

//explorar

    [HttpGet] // Consulta y muestra prompts
        public IActionResult Explorar(string query, string filtro)
        {
            // si el usuario no ha seleccionado filtro, por defecto se muestran los más recientes
            if (string.IsNullOrEmpty(filtro))
            {
                filtro = "Recientes";
            }

            // fecha actual para calcular los prompts recientes
            DateTime ahora = DateTime.Now;

            // lista simulando la base de datos
            var listaPrompts = new List<PromptViewModel>
            {
                new PromptViewModel
                {
                    Id = 1,
                    IdUsuario = 2,
                    Title = "Optimización de Producción en Whirlpool",
                    Prompt = "Genera una propuesta para mejorar la eficiencia en una línea de producción de electrodomésticos Whirlpool, considerando reducción de tiempos muertos, control de calidad, seguridad operativa y uso eficiente de materiales.",
                    AuthorName = "Alex Rivera",
                    Username = "alexr",
                    InitialsProfile = "AR",
                    CircleColor = "#F7AA63",
                    Category = "Producción",
                    Likes = 234,
                    Comments = 45,
                    Saves = 128,
                    CreatedAt = "hace 2 horas",
                    FechaPublicacion = ahora.AddHours(-2)
                },

                new PromptViewModel
                {
                    Id = 2,
                    IdUsuario = 5,
                    Title = "Campaña de Marketing para Whirlpool",
                    Prompt = "Crea una campaña de marketing digital para promocionar una nueva línea de refrigeradores Whirlpool, destacando ahorro de energía, diseño moderno, tecnología inteligente y confianza de marca.",
                    AuthorName = "Sarah Chen",
                    Username = "sarahc",
                    InitialsProfile = "SC",
                    CircleColor = "#009FDC",
                    Category = "Marketing",
                    Likes = 189,
                    Comments = 32,
                    Saves = 95,
                    CreatedAt = "hace 5 horas",
                    FechaPublicacion = ahora.AddHours(-5)
                },

                new PromptViewModel
                {
                    Id = 3,
                    IdUsuario = 10,
                    Title = "Atención al Cliente Whirlpool",
                    Prompt = "Diseña un protocolo de atención al cliente para usuarios de productos Whirlpool que incluya seguimiento de garantías, solución de fallas comunes, tiempos de respuesta y mejora de la experiencia postventa.",
                    AuthorName = "Jimmy Stone",
                    Username = "jimmys",
                    InitialsProfile = "JS",
                    CircleColor = "#8E44AD",
                    Category = "Servicio al Cliente",
                    Likes = 145,
                    Comments = 18,
                    Saves = 70,
                    CreatedAt = "hace 1 día",
                    FechaPublicacion = ahora.AddDays(-1)
                },

                new PromptViewModel
                {
                    Id = 4,
                    IdUsuario = 20,
                    Title = "Análisis Financiero para Whirlpool",
                    Prompt = "Elabora un análisis financiero básico para Whirlpool considerando costos de producción, ventas proyectadas, margen de ganancia, presupuesto por departamento y estrategias para reducir gastos operativos.",
                    AuthorName = "Mia Torres",
                    Username = "miat",
                    InitialsProfile = "MT",
                    CircleColor = "#2ECC71",
                    Category = "Finanzas",
                    Likes = 98,
                    Comments = 12,
                    Saves = 41,
                    CreatedAt = "hace 2 días",
                    FechaPublicacion = ahora.AddDays(-2)
                },

                new PromptViewModel
                {
                    Id = 5,
                    IdUsuario = 25,
                    Title = "Recursos Humanos en Whirlpool",
                    Prompt = "Propón una estrategia de recursos humanos para mejorar la capacitación, motivación, comunicación interna y retención de empleados en los diferentes departamentos de Whirlpool.",
                    AuthorName = "Luna Park",
                    Username = "lunap",
                    InitialsProfile = "LP",
                    CircleColor = "#E67E22",
                    Category = "Recursos Humanos",
                    Likes = 500,
                    Comments = 60,
                    Saves = 210,
                    CreatedAt = "hace 30 minutos",
                    FechaPublicacion = ahora.AddMinutes(-30)
                },

                new PromptViewModel
                {
                    Id = 6,
                    IdUsuario = 30,
                    Title = "Innovación Tecnológica Whirlpool",
                    Prompt = "Genera ideas de innovación tecnológica para productos Whirlpool, incluyendo sensores inteligentes, conectividad IoT, ahorro energético, mantenimiento predictivo y funciones automatizadas para mejorar la experiencia del usuario.",
                    AuthorName = "Emma López",
                    Username = "emmal",
                    InitialsProfile = "EL",
                    CircleColor = "#104B70",
                    Category = "Innovación y Tecnología",
                    Likes = 410,
                    Comments = 72,
                    Saves = 300,
                    CreatedAt = "hace 10 horas",
                    FechaPublicacion = ahora.AddHours(-10)
                }
            };

            // lista de filtros
            var listaFiltros = new List<string>
            {
                "Recientes",
                "Más Gustados",
                "Más Guardados"
            };

            //serach bars, buscar prompts por texto
            //  GET porque solo consulta/filtra información
            if (!string.IsNullOrEmpty(query))
            {
                string queryMinuscula = query.ToLower();

                listaPrompts = listaPrompts
                    .Where(p =>
                        p.Title.ToLower().Contains(queryMinuscula) ||
                        p.Prompt.ToLower().Contains(queryMinuscula) ||
                        p.Category.ToLower().Contains(queryMinuscula) ||
                        p.AuthorName.ToLower().Contains(queryMinuscula) ||
                        p.Username.ToLower().Contains(queryMinuscula)
                    )
                    .ToList();
            }

            // aplicar filtro seleccionado
            if (filtro == "Recientes")
            {
                // muestra solo los prompts publicados en las últimas 24 horas
                listaPrompts = listaPrompts
                    .Where(p => p.FechaPublicacion >= ahora.AddHours(-24))
                    .OrderByDescending(p => p.FechaPublicacion)
                    .ToList();
            }
            else if (filtro == "Más Gustados")
            {
                // ordena de mayor a menor por likes
                listaPrompts = listaPrompts
                    .OrderByDescending(p => p.Likes)
                    .ToList();
            }
            else if (filtro == "Más Guardados")
            {
                // ordena de mayor a menor por guardados
                listaPrompts = listaPrompts
                    .OrderByDescending(p => p.Saves)
                    .ToList();
            }
            else
            {
                // Filtro desconocido o URLs antiguas: mismo criterio que "Recientes" (por fecha)
                listaPrompts = listaPrompts
                    .Where(p => p.FechaPublicacion >= ahora.AddHours(-24))
                    .OrderByDescending(p => p.FechaPublicacion)
                    .ToList();
            }
            var datos = new ExplorarViewModel
            {
                Prompts = listaPrompts,
                Filters = listaFiltros,
                Query = query,
                FiltroActivo = filtro
            };

            return View(datos);
        }

        [HttpPost]
        public IActionResult PublicarComentario(int promptId, string comentario)
        {
            var malasPalabras = new List<string>
            {
                "palabra1", "palabra2", "palabra3", "palabra4", "palabra5",
                "palabra6", "palabra7", "palabra8", "palabra9", "palabra10"
            };

            if (string.IsNullOrWhiteSpace(comentario))
            {
                return Json(new
                {
                    ok = false,
                    mensaje = "Escribe un comentario antes de publicar."
                });
            }

            string comentarioMinusculas = comentario.ToLower();

            bool tieneMalasPalabras = malasPalabras.Any(palabra =>
                comentarioMinusculas.Contains(palabra)
            );

            if (tieneMalasPalabras)
            {
                return Json(new
                {
                    ok = false,
                    //no cumple con las condiciones, no lo guarda en la base de datos
                    mensaje = "Tu comentario contiene palabras no permitidas. Por favor, sé respetuoso."
                });
            }

            // aqui se guardaria el comentario en la base de datos.
            return Json(new
            {
                ok = true,
                mensaje = "¡Comentario publicado con éxito!"
                //mensaje de ocnfirmacion
            });
        }

        public IActionResult Ajustes()
    {
        return View();
    }

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

    public IActionResult Crear()
    {
        PromptViewModelCrear prompt = new PromptViewModelCrear();

        return View(prompt);
    }

[HttpPost]
    public IActionResult Create(PromptViewModelCrear prompt)
    {
       

        ViewData["Mensaje"] = "Se guardó correctamente tu prompt";

        return View(prompt);
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

    // POST al MVC; un solo PUT /perfil/{id} a Flask (datos + rutaFotoPerfil).
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

    // Solo POST al MVC (sin API aún). No confundir con PUT de editar perfil.
    [HttpPost]
    public IActionResult PublicarComentarioPerfil(int promptId, string comentario, string? returnTab)
    {
        TempData["PerfilMensaje"] = MensajeComentario(comentario);
        return RedirectToAction(nameof(Perfil), new { tab = string.IsNullOrWhiteSpace(returnTab) ? "publicados" : returnTab });
    }

    // --- Sesión y pestaña ---
    private int IdSesion()
    {
        return HttpContext.Session.GetInt32(AuthSessionKeys.UsuarioId) ?? LoginViewModel.UsuarioPorDefectoId;
    }

    private static string Tab(string? t) =>
        string.IsNullOrWhiteSpace(t) || t.Equals("publicaciones", StringComparison.OrdinalIgnoreCase) ? "publicados" : t;

    // --- Llamada API y ViewBag ---
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

    // --- JSON de la API → Models ---
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
        CreatedAt = S(d, "fechaPublicacion"), Trending = N(d, "trending") == 1,
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
        string[] malas = ["palabra1", "palabra2", "palabra3", "palabra4", "palabra5", "palabra6", "palabra7", "palabra8", "palabra9", "palabra10"];
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
}