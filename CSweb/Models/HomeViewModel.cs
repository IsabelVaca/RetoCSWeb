namespace CSweb.Models
{
    public class HomeViewModel
    {
        public int CreadoresActivos { get; set; }
        public int PostsHoy { get; set; }
        public int MiembrosComunidad { get; set; }

        public List<HomeActividadViewModel> Actividades { get; set; }
        public List<HomeTendenciaViewModel> Tendencias { get; set; }
        public List<HomeCreadorViewModel> Creadores { get; set; }
    }
}