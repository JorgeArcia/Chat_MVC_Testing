using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_p2.Modelo
{
    class MSNMessenger
    {
        public List<Contacto> contactos { get; set; } = new List<Contacto>();
        public List<Chat> Chats { get; set; } = new List<Chat>();

        public bool AgregarContacto(Contacto c)
        {
            bool AuxBool = false;

            if (contactos.Count == 0)
            {
                this.contactos.Add(c);
                AuxBool = true;
            }
            else
            {
                for (int i = 0; i < this.contactos.Count; i++)
                {
                    if (this.contactos[i].Nombre != c.Nombre)
                    {
                        this.contactos.Add(c);
                        return true;
                    }
                }
                return false;
            }
            return AuxBool;
        }

        public Chat AgregarChat(Contacto c)
        {
            for (int i = 0; i < this.Chats.Count; i++)
            {
                if (this.Chats[i].Contacto == c)
                {
                    return this.Chats[i];
                }
            }
            Chat NewChat = new Chat(c);
            this.Chats.Add(NewChat);
            return NewChat;
        }
        public void Refrescar()
        {
            for (int i = 0; i < this.Chats.Count; i++)
            {
                this.Chats[i].Refrescar();
            }

        }

        public List<Chat> BuscarChats(string buscar)
        {
            List<Chat> NewChats = new List<Chat>();

            for (int i = 0; i < this.Chats.Count; i++)
            {
                for (int x = 0; x < this.Chats[i].Mensajes.Count; x++)
                {
                    if (this.Chats[i].Mensajes[x].texto.Contains(buscar))
                    {
                        NewChats.Add(this.Chats[i]);
                    }
                }
            }
            return NewChats;
        }
    }
}
