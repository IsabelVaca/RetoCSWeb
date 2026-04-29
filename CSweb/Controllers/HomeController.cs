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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
