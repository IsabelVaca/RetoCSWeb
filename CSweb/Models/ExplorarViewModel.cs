using System.Collections.Generic;

namespace CSweb.Models
{
    // Datos que necesita la página Explorar
    public class ExplorarViewModel
    {
        // Lista de prompts que se desplegarán en la pantalla
        public List<PromptViewModel> Prompts { get; set; }

        // Lista de filtros: Tendencias, Recientes, Más Gustados, etc.
        public List<string> Filters { get; set; }

        // Guarda lo que el usuario escribió en la barra de búsqueda
        public string Query { get; set; }

        // Guarda el filtro que está seleccionado actualmente
        public string FiltroActivo { get; set; }
    }
}