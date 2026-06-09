namespace CSweb.Models
{
    public class ItemTiendaModel
    {
        public int IDItem { get; set; }
        public string NombreItem { get; set; }
        public string DescripcionItem { get; set; }
        public int Precio { get; set; }
        public string UrlImagenItem { get; set; }
        public bool Desbloqueado { get; set; }
    }
}
