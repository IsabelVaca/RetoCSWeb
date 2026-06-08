using System;

namespace CSweb.Models
{
    //estructura q debe tener un prompt 
    public class PromptViewModel
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string Title { get; set; } = "";
        public string Prompt { get; set; } = "";
        public string AuthorName { get; set; } = "";
        public string Username { get; set; } = "";
        public string InitialsProfile { get; set; } = "";
        public string CircleColor { get; set; } = "";
        public string Category { get; set; } = "";
        public int Likes { get; set; }
        public int Comments { get; set; }
        public int Saves { get; set; }
        public string? FechaPublicacion { get; set; }
        public string CreatedAt { get; set; } = "";
        public int Trending { get; set; }
        public int LikedByMe { get; set; }
        public int SavedByMe { get; set; }
            }
        }