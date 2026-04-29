using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CSweb.Models;
namespace CSweb.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
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
            // si el usuario no ha seleccionado filtro, por default se usa Tendencias
            if (string.IsNullOrEmpty(filtro))
            {
                filtro = "Tendencias";
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
                    FechaPublicacion = ahora.AddHours(-2),
                    Trending = true
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
                    FechaPublicacion = ahora.AddHours(-5),
                    Trending = true
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
                    FechaPublicacion = ahora.AddDays(-1),
                    Trending = false
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
                    FechaPublicacion = ahora.AddDays(-2),
                    Trending = false
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
                    FechaPublicacion = ahora.AddMinutes(-30),
                    Trending = true
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
                    FechaPublicacion = ahora.AddHours(-10),
                    Trending = false
                }
            };

            // lista de filtros
            var listaFiltros = new List<string>
            {
                "Tendencias",
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
            if (filtro == "Tendencias")
            {
                // muestra solo los prompts que tengan Trending en true
                listaPrompts = listaPrompts
                    .Where(p => p.Trending == true)
                    .ToList();
            }
            else if (filtro == "Recientes")
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

            // regresar datos a la vista
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

    public IActionResult Perfil(int? id)
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
