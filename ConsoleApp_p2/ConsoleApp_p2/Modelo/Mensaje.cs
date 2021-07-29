using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_p2.Modelo
{
    class Mensaje
    {
        public DateTime FechaHora { get; set; } = DateTime.Now;
        public string texto { get; set; }
        public bool esmio { get; set; }
        public bool visto { get; set; }
        public Mensaje mensajeRespondido { get; set; }
    }

    
}
