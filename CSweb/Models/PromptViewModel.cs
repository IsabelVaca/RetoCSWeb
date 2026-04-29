using System;

namespace CSweb.Models
{
    // Modelo de la estructura de un prompt
    public class PromptViewModel
    {
        public int Id { get; set; } // Identificador de cada prompt

        public int IdUsuario { get; set; } // No aparece en el front, solo es para datos

        public string Title { get; set; }

        public string Prompt { get; set; }

        public string AuthorName { get; set; }

        public string Username { get; set; }

        public string InitialsProfile { get; set; }

        public string CircleColor { get; set; }

        public string Category { get; set; }

        public int Likes { get; set; }

        public int Comments { get; set; }

        public int Saves { get; set; }

        // Texto que se muestra en la tarjeta
        // Ejemplo: "hace 2 horas"
        public string CreatedAt { get; set; }

        // Fecha real para poder filtrar recientes
        public DateTime FechaPublicacion { get; set; }

        // Sirve para saber si aparece en Tendencias
        public bool Trending { get; set; }
    }
}