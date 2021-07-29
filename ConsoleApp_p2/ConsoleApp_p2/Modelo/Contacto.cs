using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_p2.Modelo
{
    class Contacto
    {
        public string Nombre { get; set;}
        public string Info { get; set; }

        public Contacto(string name,string inf)
        {
            if (inf == null || name == null)
            {
                throw new ArgumentException("Dato ingresado no puede ser null");
            }

            this.Nombre = name;
            this.Info = inf;
        }
    }
}
