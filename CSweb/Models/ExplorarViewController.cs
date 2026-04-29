using System.Collections.Generic;

namespace CSweb.Models
{
    // datos que necesita la página Explorar
    public class ExplorarViewModel
    {
        // lista de prompts que se desplegarán en la pantalla
        public List<PromptViewModel> Prompts { get; set; }

        // lista de filtros: Tendencias, Recientes, Más Gustados, etc.
        public List<string> Filters { get; set; }

        // guarda lo que el usuario escribió en la barra de búsqueda
        public string Query { get; set; }

        // guarda el filtro que está seleccionado actualmente
        public string FiltroActivo { get; set; }
    }
}