using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_p2.Modelo
{
    class Chat
    {
        public Contacto Contacto { get; set; }
        public List<Mensaje> Mensajes { get; set; } = new List<Mensaje>();
        public Random random = new Random();

        public Chat(Contacto contacto)
        {
            if (contacto == null)
            {
                throw new ArgumentException();
            }
            this.Contacto = contacto;
        }

        public int ContarNoLeidos()
        {
            int cantidad = 0;
            for (int i = 0; i < Mensajes.Count; i++)
            {
                if (this.Mensajes[i].esmio == false && this.Mensajes[i].visto == false)
                {
                    cantidad++;
                }
            }
            return cantidad;
        }

        public void Enviar(Mensaje msg)
        {
            if (msg != null && (msg.texto != null || msg.texto != " "))
            {
                msg.esmio = true;
                this.Mensajes.Add(msg);
            }
        }

        public bool ContieneTermino(string texto)
        {
            for (int i = 0; i < Mensajes.Count; i++)
            {
                if (this.Mensajes[i].texto.Contains(texto))
                {
                    return true;
                }
            }
            return false;
        }
        public void ActualizarVisto()
        {
            for (int i = 0; i < this.Mensajes.Count; i++)
            {
                if (this.Mensajes[i].esmio == false && this.Mensajes[i].visto == false)
                {
                    this.Mensajes[i].visto = true;
                }
            }
        }

        public int IndexDe(Mensaje msg)
        {
            for (int i = 0; i < Mensajes.Count; i++)
            {
                if (this.Mensajes[i] == msg)
                {
                    return i;
                }
            }
            return -1;
        }
        public string UltimoMsj()
        {
            string AuxString;
            int aux = this.Mensajes.Count;
            AuxString = this.Mensajes[aux-1].texto;
            return AuxString;
        }

        public void AgregarMsg()
        {
            string AuxString = " ";
            int Ultimo = this.Mensajes.Count;
            Mensaje MensajeRobot;

            if (this.Mensajes.Count == 0)
            {
                AuxString = "hola, ¿cómo estás?";
                MensajeRobot = new Mensaje()
                {
                    FechaHora = DateTime.Now,
                    texto = AuxString,
                    esmio = false,
                    visto = false,
                    mensajeRespondido = null
                };
                this.Mensajes.Add(MensajeRobot);

            }
            else if (this.Mensajes[Ultimo-1].esmio == true)
            {

                AuxString = this.Mensajes[Ultimo-1].texto.ToUpper();
                MensajeRobot = new Mensaje()
                {
                    FechaHora = DateTime.Now,
                    texto = AuxString,
                    esmio = false,
                    visto = false,
                    mensajeRespondido = null
                };
                this.Mensajes.Add(MensajeRobot);
            }
            else if (!this.Mensajes[Ultimo-1].esmio == true)
            {
                AuxString = "Respondeme pliz";
                MensajeRobot = new Mensaje()
                {
                    FechaHora = DateTime.Now,
                    texto = AuxString,
                    esmio = false,
                    visto = false,
                    mensajeRespondido = null
                };
                this.Mensajes.Add(MensajeRobot);
            }
            
        }
        public bool Refrescar()
        {
            int Rng = random.Next(0, 101);

            if (Rng >= 25)
            {
                return false;
            }
            else
            {
                this.ActualizarVisto();
                this.AgregarMsg();
                return true;
            }
        }
    }
}
