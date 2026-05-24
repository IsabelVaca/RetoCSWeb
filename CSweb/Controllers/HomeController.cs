using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using CSweb.Models;

namespace CSweb.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IWebHostEnvironment _env;

    // Límites de editar perfil (definidos aquí, no en appsettings.json).
    private static readonly PerfilEdicionOpciones PerfilLimites = new();

    // Foto de perfil por defecto en wwwroot (Imagenes/perfil/perfil.jpg).
    private const string FotoPerfilPorDefecto = "/Imagenes/perfil/perfil.jpg";

    // Usuario: solo letras, números y guion bajo.
    private static readonly Regex RegexUserNamePerfil = new(@"^[a-zA-Z0-9_]+$", RegexOptions.Compiled);

    public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env)
    {
        _logger = logger;
        _env = env;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Ranking(string query)
    {

        //simula sesión
        var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

        //asigna id
        if (usuarioId == null)
        {
            HttpContext.Session.SetInt32("UsuarioId", 2);
            usuarioId = 2;
        }

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

        // Este método recibe el comentario desde JavaScript usando fetch.
        // Ya NO redirige a Explorar porque queremos quedarnos en la misma pantalla.
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

    public IActionResult Login()
    {
        return View();
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


    // Perfil: demo en controlador. Al conectar API, sustituir sesión + RellenarPerfilConEjemploDatos por llamadas HTTP.

    // Pestañas del perfil (id, icono, etiqueta) para la barra de navegación.
    private static readonly (string Id, string Icon, string Label)[] PerfilTabsNav =
    {
        ("publicados", "bi-grid-3x3-gap", "Publicados"),
        ("guardados", "bi-bookmark", "Guardados"),
        ("likeados", "bi-heart", "Likeados"),
        ("actividad", "bi-lightning-charge", "Actividad"),
    };

    public IActionResult Perfil()
    {
        var perfil = ConstruirPerfilCompleto();
        PrepararViewBagPerfil(perfil);
        return View(perfil);
    }

    [HttpPost]
    public async Task<IActionResult> EditarPerfil(PerfilEditarViewModel model)
    {
        var limites = PerfilLimites;

        // Validación según límites
        ValidarPerfilEditar(model, ModelState);

        // Id de sesión para el nombre único del archivo
        var idUsuario = HttpContext.Session.GetInt32("UsuarioId") ?? 2;

        if (model.FotoPerfil != null && model.FotoPerfil.Length > 0)
        {
            // Demo: disco local. API: subir archivo y usar URL devuelta.
            var rutaNueva = await GuardarFotoPerfilAsync(model.FotoPerfil, idUsuario);
            if (rutaNueva == null)
            {
                var extTexto = string.Join(", ", limites.ExtensionesFotoPermitidas);
                ModelState.AddModelError(nameof(model.FotoPerfil), $"Extensiones NO permitidas. Usa {extTexto}.");
            }
            else
            {
                model.RutaFotoPerfil = rutaNueva;
            }
        }

        if (!ModelState.IsValid)
        {
            var perfil = ConstruirPerfilCompleto();
            AplicarCabeceraDesdeFormulario(perfil, model);
            ViewBag.PerfilEditar = model;
            PrepararViewBagPerfil(perfil, "editar");
            return View("Perfil", perfil);
        }

        // Demo: sesión. API: PUT/PATCH perfil del usuario.
        GuardarCabeceraPerfilEnSesion(model);
        TempData["PerfilMensaje"] = "Perfil actualizado correctamente.";
        return RedirectToAction(nameof(Perfil));
    }

    [HttpPost]
    public IActionResult PublicarComentarioPerfil(int promptId, string comentario, string? returnTab)
    {
        var mensaje = ValidarTextoComentarioPerfil(comentario);
        TempData["PerfilMensaje"] = mensaje;
        var tab = string.IsNullOrWhiteSpace(returnTab) ? "publicados" : returnTab;
        return RedirectToAction(nameof(Perfil), new { tab });
    }

    // Rellena ViewBag para Perfil.cshtml (pestaña, listas, formulario editar, URLs de foto).
    private void PrepararViewBagPerfil(PerfilViewModel perfil, string? tabForzado = null)
    {
        var tabRaw = tabForzado
            ?? Request.Query["tab"].FirstOrDefault()
            ?? "publicados";
        var tab = tabRaw.Equals("publicaciones", StringComparison.OrdinalIgnoreCase) ? "publicados" : tabRaw;

        ViewBag.Tab = tab;
        ViewBag.EnEditar = tab.Equals("editar", StringComparison.OrdinalIgnoreCase);
        ViewBag.EnGuardados = tab.Equals("guardados", StringComparison.OrdinalIgnoreCase);
        ViewBag.EnLikeados = tab.Equals("likeados", StringComparison.OrdinalIgnoreCase);
        ViewBag.EnActividad = tab.Equals("actividad", StringComparison.OrdinalIgnoreCase);
        ViewBag.TabsNav = PerfilTabsNav;
        ViewBag.Prompts = (bool)ViewBag.EnGuardados
            ? perfil.Guardados
            : (bool)ViewBag.EnLikeados
                ? perfil.Likeados
                : perfil.Publicados;

        var editar = ViewBag.PerfilEditar as PerfilEditarViewModel ?? new PerfilEditarViewModel
        {
            Nombre = perfil.Nombre,
            UserName = perfil.UserName,
            Bio = perfil.Bio,
            RutaFotoPerfil = perfil.ImagenPerfil,
        };
        ViewBag.PerfilEditar = editar;
        ViewBag.PerfilLimites = PerfilLimites;
        ViewBag.UrlFotoCabecera = UrlFotoPerfil(perfil.ImagenPerfil);
        ViewBag.UrlFotoEditar = UrlFotoPerfil(editar.RutaFotoPerfil);
    }

    // Convierte ruta de imagen de perfil a URL para la etiqueta img.
    private string UrlFotoPerfil(string? ruta)
    {
        if (string.IsNullOrWhiteSpace(ruta))
            return Url.Content("~/Imagenes/perfil/perfil.jpg")!;
        if (ruta.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            return ruta;
        return Url.Content("~" + (ruta.StartsWith("/") ? ruta : "/" + ruta))!;
    }

    // Valores por defecto de la cabecera cuando aún no hay nada en sesión.
    private static PerfilViewModel CrearPerfilPorDefecto()
    {
        return new PerfilViewModel
        {
            ImagenPerfil = FotoPerfilPorDefecto,
            Nombre = "Sofía Castillo",
            UserName = "sofiac_prompts",
            Bio = "Diseñadora de prompts para equipos de producto y marketing.",
            Correo = "sofia.castillo@ejemplo.com",
        };
    }

    // Arma el perfil: cabecera (demo/sesión) + listas demo. API: un GET que devuelva PerfilViewModel.
    private PerfilViewModel ConstruirPerfilCompleto()
    {
        var perfil = CrearPerfilPorDefecto();
        AplicarCabeceraPerfilDesdeSesion(perfil);
        RellenarPerfilConEjemploDatos(perfil);
        return perfil;
    }

    // Demo: lee cabecera guardada en sesión tras editar. API: datos del GET perfil.
    private void AplicarCabeceraPerfilDesdeSesion(PerfilViewModel p)
    {
        var nombre = HttpContext.Session.GetString("Perfil_Nombre");
        if (!string.IsNullOrWhiteSpace(nombre))
            p.Nombre = nombre;

        var user = HttpContext.Session.GetString("Perfil_UserName");
        if (!string.IsNullOrWhiteSpace(user))
            p.UserName = user;

        var bio = HttpContext.Session.GetString("Perfil_Bio");
        if (bio != null)
            p.Bio = bio;

        var imagen = HttpContext.Session.GetString("Perfil_ImagenPerfil");
        if (!string.IsNullOrWhiteSpace(imagen))
            p.ImagenPerfil = imagen;
        else if (HttpContext.Session.Keys.Contains("Perfil_ImagenPerfil"))
            p.ImagenPerfil = FotoPerfilPorDefecto;
    }

    // Demo: persiste cabecera en sesión. API: enviar modelo al endpoint de actualización.
    private void GuardarCabeceraPerfilEnSesion(PerfilEditarViewModel m)
    {
        HttpContext.Session.SetString("Perfil_Nombre", m.Nombre.Trim());
        HttpContext.Session.SetString("Perfil_UserName", m.UserName.Trim());
        HttpContext.Session.SetString("Perfil_Bio", m.Bio?.Trim() ?? string.Empty);

        if (string.IsNullOrWhiteSpace(m.RutaFotoPerfil))
            HttpContext.Session.Remove("Perfil_ImagenPerfil");
        else
            HttpContext.Session.SetString("Perfil_ImagenPerfil", m.RutaFotoPerfil.Trim());
    }

    // Tras error de validación: muestra en cabecera lo que el usuario escribió (sin llamar API).
    private static void AplicarCabeceraDesdeFormulario(PerfilViewModel p, PerfilEditarViewModel m)
    {
        p.Nombre = m.Nombre ?? p.Nombre;
        p.UserName = m.UserName ?? p.UserName;
        p.Bio = m.Bio ?? string.Empty;
        p.ImagenPerfil = string.IsNullOrWhiteSpace(m.RutaFotoPerfil)
            ? FotoPerfilPorDefecto
            : m.RutaFotoPerfil.Trim();
    }

    // Rellena el perfil con prompts y actividad inventados (demo sin base de datos).
    private static void RellenarPerfilConEjemploDatos(PerfilViewModel p)
    {
        var ahora = DateTime.UtcNow;

        // Iniciales para tarjetas demo: dos letras a partir del nombre visible actual.
        var nombreTrim = p.Nombre.Trim();
        var inicialesPerfil = nombreTrim.Length >= 2
            ? $"{char.ToUpperInvariant(nombreTrim[0])}{char.ToUpperInvariant(nombreTrim[1])}"
            : nombreTrim.Length == 1
                ? $"{char.ToUpperInvariant(nombreTrim[0])}·"
                : "··";

        // Prompts que el usuario del perfil publicó
        p.Publicados = new List<PromptViewModel>
        {
            CrearPromptEjemploPerfil(501, "Email de seguimiento B2B cordial",
                "Redacta un correo de seguimiento 48 h después de una demo, tono profesional y breve.",
                p.Nombre, p.UserName, inicialesPerfil, "#104B70", "Ventas", 34, 5, 12, 1, ahora),
            CrearPromptEjemploPerfil(502, "Brief creativo para campaña estival",
                "Genera un brief con objetivo, público, tono y 3 ideas de mensaje para redes.",
                p.Nombre, p.UserName, inicialesPerfil, "#0e536e", "Marketing", 21, 8, 19, 4, ahora),
            CrearPromptEjemploPerfil(503, "Checklist de revisión de prompt",
                "Lista en viñetas qué revisar antes de compartir un prompt con el equipo.",
                p.Nombre, p.UserName, inicialesPerfil, "#6998b8", "Producto", 56, 11, 31, 9, ahora),
        };

        // Prompts de otros que guardó
        p.Guardados = new List<PromptViewModel>
        {
            CrearPromptEjemploPerfil(601, "Resumen ejecutivo en 5 viñetas",
                "Convierte un informe largo en un resumen para C-level con métricas clave.",
                "Marco Ruiz", "marco_r", "MR", "#2d6a4f", "Negocio", 89, 14, 40, 2, ahora),
            CrearPromptEjemploPerfil(602, "Guion de voz para tutorial de 2 min",
                "Escribe un guion claro con pausas y énfasis para video de onboarding.",
                "Lucía Méndez", "lucia_m", "LM", "#bc6c25", "Educación", 45, 6, 22, 6, ahora),
        };

        // Prompts de otros a los que dio me gusta
        p.Likeados = new List<PromptViewModel>
        {
            CrearPromptEjemploPerfil(701, "Ideas de hooks para LinkedIn técnico",
                "10 hooks que no suenen a clickbait para posts sobre ingeniería de datos.",
                "Ana Torres", "ana_t", "AT", "#5c4d7d", "Redes", 120, 23, 55, 0, ahora),
            CrearPromptEjemploPerfil(702, "Traducción neutra ES ↔ EN para UI",
                "Traduce textos de interfaz manteniendo longitud similar y tono inclusivo.",
                "Diego Paredes", "diego_p", "DP", "#457b9d", "Localización", 67, 9, 28, 3, ahora),
            CrearPromptEjemploPerfil(703, "Retro de sprint en tono positivo",
                "Estructura una retro con qué salió bien, riesgos y 3 acciones con responsable.",
                "Elena Vázquez", "elena_v", "EV", "#9b2226", "Equipos", 38, 4, 15, 11, ahora),
        };

        // Interacciones de terceros con los prompts del perfil
        p.Actividad = new List<PerfilActividadItemViewModel>
        {
            new PerfilActividadItemViewModel
            {
                Tipo = "guardado",
                ActorNombre = "Lucía Méndez",
                ActorUserName = "lucia_m",
                TituloPrompt = "Email de seguimiento B2B cordial",
                Momento = "hace 35 min",
            },
            new PerfilActividadItemViewModel
            {
                Tipo = "like",
                ActorNombre = "Marco Ruiz",
                ActorUserName = "marco_r",
                TituloPrompt = "Brief creativo para campaña estival",
                Momento = "hace 2 h",
            },
            new PerfilActividadItemViewModel
            {
                Tipo = "comentario",
                ActorNombre = "Ana Torres",
                ActorUserName = "ana_t",
                TituloPrompt = "Checklist de revisión de prompt",
                Momento = "hace 5 h",
                ExtractoComentario = "Lo usamos en el equipo de diseño, ¡mil gracias!",
            },
            new PerfilActividadItemViewModel
            {
                Tipo = "like",
                ActorNombre = "Equipo Cobalt",
                ActorUserName = "cobalt_legends",
                TituloPrompt = "Email de seguimiento B2B cordial",
                Momento = "ayer",
            },
            new PerfilActividadItemViewModel
            {
                Tipo = "guardado",
                ActorNombre = "Diego Paredes",
                ActorUserName = "diego_p",
                TituloPrompt = "Checklist de revisión de prompt",
                Momento = "ayer",
            },
            new PerfilActividadItemViewModel
            {
                Tipo = "comentario",
                ActorNombre = "Elena Vázquez",
                ActorUserName = "elena_v",
                TituloPrompt = "Brief creativo para campaña estival",
                Momento = "hace 2 días",
                ExtractoComentario = "¿Podrías añadir una variante más informal?",
            },
        };

        p.NumeroPublicaciones = p.Publicados.Count;
    }

    // Construye un prompt de demostración para las pestañas del perfil.
    private static PromptViewModel CrearPromptEjemploPerfil(
        int id,
        string titulo,
        string cuerpo,
        string autor,
        string user,
        string iniciales,
        string color,
        string cat,
        int likes,
        int com,
        int saves,
        int dias,
        DateTime ahora)
    {
        var fecha = ahora.AddDays(-dias);
        return new PromptViewModel
        {
            Id = id,
            IdUsuario = 0,
            Title = titulo,
            Prompt = cuerpo,
            AuthorName = autor,
            Username = user,
            InitialsProfile = iniciales,
            CircleColor = color,
            Category = cat,
            Likes = likes,
            Comments = com,
            Saves = saves,
            CreatedAt = dias == 0 ? "hoy" : dias == 1 ? "ayer" : $"hace {dias} días",
            FechaPublicacion = fecha,
            Trending = false,
        };
    }

    // Valida el texto del comentario enviado desde Perfil (misma lógica que Explorar, sin JSON).
    private static string ValidarTextoComentarioPerfil(string comentario)
    {
        var malasPalabras = new List<string>
        {
            "palabra1", "palabra2", "palabra3", "palabra4", "palabra5",
            "palabra6", "palabra7", "palabra8", "palabra9", "palabra10"
        };

        if (string.IsNullOrWhiteSpace(comentario))
            return "Escribe un comentario antes de publicar.";

        var comentarioMinusculas = comentario.ToLower();
        if (malasPalabras.Any(p => comentarioMinusculas.Contains(p)))
            return "Tu comentario contiene palabras no permitidas. Por favor, sé respetuoso.";

        // API: POST comentario (promptId + texto).
        return "¡Comentario publicado con éxito!";
    }

    //Valida nombre, usuario y bio al editar perfil (límites en PerfilEdicionOpciones).
    private void ValidarPerfilEditar(PerfilEditarViewModel model, ModelStateDictionary modelState)
    {
        if (string.IsNullOrWhiteSpace(model.Nombre))
            modelState.AddModelError(nameof(model.Nombre), "El nombre es obligatorio.");
        else if (model.Nombre.Trim().Length > PerfilLimites.MaxNombre)
            modelState.AddModelError(nameof(model.Nombre), $"El nombre no puede superar {PerfilLimites.MaxNombre} caracteres.");

        if (string.IsNullOrWhiteSpace(model.UserName))
            modelState.AddModelError(nameof(model.UserName), "El usuario es obligatorio.");
        else if (model.UserName.Trim().Length > PerfilLimites.MaxUserName)
            modelState.AddModelError(nameof(model.UserName), $"El usuario no puede superar {PerfilLimites.MaxUserName} caracteres.");
        else if (!RegexUserNamePerfil.IsMatch(model.UserName.Trim()))
            modelState.AddModelError(nameof(model.UserName), "Solo letras, números y guion bajo.");

        if (model.Bio != null && model.Bio.Length > PerfilLimites.MaxBio)
            modelState.AddModelError(nameof(model.Bio), $"La biografía no puede superar {PerfilLimites.MaxBio} caracteres.");
    }

    // Demo: guarda en wwwroot. API: multipart al endpoint de avatar; devolver URL.
    private async Task<string?> GuardarFotoPerfilAsync(IFormFile foto, int idUsuario)
    {
        if (foto == null || foto.Length == 0)
            return null;

        var extension = Path.GetExtension(foto.FileName).ToLowerInvariant();
        var permitidas = PerfilLimites.ExtensionesFotoPermitidas
            .Select(e => e.StartsWith('.') ? e.ToLowerInvariant() : "." + e.ToLowerInvariant())
            .ToArray();

        if (!permitidas.Contains(extension))
            return null;

        var carpeta = Path.Combine(_env.WebRootPath, PerfilLimites.CarpetaFotosRelativa.Replace('/', Path.DirectorySeparatorChar));
        Directory.CreateDirectory(carpeta);

        var nombreArchivo = $"{Guid.NewGuid()}_FotoUsuario{idUsuario}{extension}";
        var rutaCompleta = Path.Combine(carpeta, nombreArchivo);

        await using (var stream = new FileStream(rutaCompleta, FileMode.Create))
        {
            await foto.CopyToAsync(stream);
        }

        var carpetaUrl = PerfilLimites.CarpetaFotosRelativa.Trim('/').Replace('\\', '/');
        return $"/{carpetaUrl}/{nombreArchivo}";
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
