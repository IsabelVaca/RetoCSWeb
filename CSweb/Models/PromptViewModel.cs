using System;

namespace CSweb.Models
{
    // modelo de la estructura de un prompt
    public class PromptViewModel
    {
        public int Id { get; set; } // identificador de cada prompt

        public int IdUsuario { get; set; } // no aparece en el front, solo es para datos

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

        public string CreatedAt { get; set; } //no se usa en la base de datos solo es para el front

        public DateTime FechaPublicacion { get; set; } //este es el q se usa realmente para filtrar

        public bool Trending { get; set; }
    }
}