using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CSweb.Models;

namespace CSweb.Controllers;

// Simulación de base de datos
public class HomeController : Controller
{
    // Direcciona a la pantalla que le corresponde al darle click al icono/botón
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet] // Consulta y muestra prompts
    public IActionResult Explorar(string query, string filtro)
    {
        // Si el usuario no ha seleccionado filtro, por default se usa Tendencias
        if (string.IsNullOrEmpty(filtro))
        {
            filtro = "Tendencias";
        }

        // Fecha actual para calcular los prompts recientes
        DateTime ahora = DateTime.Now;

        // Se crea una lista simulando la base de datos
        var listaPrompts = new List<PromptViewModel>
        {
            new PromptViewModel
            {
                Id = 1,
                IdUsuario = 2,
                Title = "Ciudad Cyberpunk de Noche",
                Prompt = "Un paisaje urbano futurista cyberpunk de noche con luces de neón, autos voladores, calles mojadas por lluvia, tonos vibrantes de morado y azul, altamente detallado, iluminación cinemática",
                AuthorName = "Alex Rivera",
                Username = "alexr",
                InitialsProfile = "AR",
                CircleColor = "#F7AA63",
                Category = "Arte",
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
                Title = "Diseño de Espacio de Trabajo Minimalista",
                Prompt = "Configuración de oficina en casa minimalista y limpia con escritorio blanco, MacBook, plantas suculentas, iluminación natural desde ventana grande, estilo escandinavo, tonos cálidos",
                AuthorName = "Sarah Chen",
                Username = "sarahc",
                InitialsProfile = "SC",
                CircleColor = "#009FDC",
                Category = "Diseño",
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
                Title = "Bosque Mágico",
                Prompt = "Bosque encantado con luces brillantes, árboles enormes, niebla suave, flores luminosas y ambiente de fantasía",
                AuthorName = "Jimmy Stone",
                Username = "jimmys",
                InitialsProfile = "JS",
                CircleColor = "#8E44AD",
                Category = "Fantasía",
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
                Title = "Robot Futurista",
                Prompt = "Robot humanoide futurista en un laboratorio tecnológico, luces azules, estilo realista, detalles metálicos",
                AuthorName = "Mia Torres",
                Username = "miat",
                InitialsProfile = "MT",
                CircleColor = "#2ECC71",
                Category = "Tecnología",
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
                Title = "Castillo Flotante",
                Prompt = "Castillo flotando entre nubes doradas, estilo fantasía épica, iluminación mágica y detalles arquitectónicos antiguos",
                AuthorName = "Luna Park",
                Username = "lunap",
                InitialsProfile = "LP",
                CircleColor = "#E67E22",
                Category = "Fantasía",
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
                Title = "Logo para App de IA",
                Prompt = "Logo moderno para aplicación de inteligencia artificial, estilo minimalista, colores azul oscuro y blanco, ícono abstracto tecnológico",
                AuthorName = "Emma López",
                Username = "emmal",
                InitialsProfile = "EL",
                CircleColor = "#104B70",
                Category = "Diseño",
                Likes = 410,
                Comments = 72,
                Saves = 300,
                CreatedAt = "hace 10 horas",
                FechaPublicacion = ahora.AddHours(-10),
                Trending = false
            }
        };

        // Lista de filtros
        var listaFiltros = new List<string>
        {
            "Tendencias",
            "Recientes",
            "Más Gustados",
            "Más Guardados"
        };

        // Buscar prompts por texto
        // Esto sigue siendo GET porque solo consulta/filtra información
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

        // Aplicar filtro seleccionado
        if (filtro == "Tendencias")
        {
            // Muestra solo los prompts que tengan Trending en true
            listaPrompts = listaPrompts
                .Where(p => p.Trending == true)
                .ToList();
        }
        else if (filtro == "Recientes")
        {
            // Muestra solo los prompts publicados en las últimas 24 horas
            listaPrompts = listaPrompts
                .Where(p => p.FechaPublicacion >= ahora.AddHours(-24))
                .OrderByDescending(p => p.FechaPublicacion)
                .ToList();
        }
        else if (filtro == "Más Gustados")
        {
            // Ordena de mayor a menor por likes
            listaPrompts = listaPrompts
                .OrderByDescending(p => p.Likes)
                .ToList();
        }
        else if (filtro == "Más Guardados")
        {
            // Ordena de mayor a menor por guardados
            listaPrompts = listaPrompts
                .OrderByDescending(p => p.Saves)
                .ToList();
        }

        // Regresar datos a la vista
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
                mensaje = "Tu comentario contiene palabras no permitidas. Por favor, sé respetuoso en CODEX Land."
            });
        }

        // Aquí después se guardaría el comentario en la base de datos.
        // Por ahora solo regresamos que sí fue válido.
        return Json(new
        {
            ok = true,
            mensaje = "¡Comentario publicado con éxito!"
        });
    }

    public IActionResult Crear()
    {
        return View();
    }

    public IActionResult Juego()
    {
        return View();
    }

    public IActionResult Ranking()
    {
        return View();
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}