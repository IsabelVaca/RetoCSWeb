namespace CSweb.Models
{
    public class TiendaViewModel
    {
        public string Saldo { get; set; }
        public List<ItemTiendaModel> Items { get; set; } = new List<ItemTiendaModel>();
        public int IdUsuario { get; set; }
    }
}