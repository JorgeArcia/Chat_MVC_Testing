using System;
using ConsoleApp_p2.Modelo;
using ConsoleApp_p2.Vista;
using ConsoleApp_p2.Vista.Pantallas;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_p2.Controlador
{
    class Controller
    {
        private MSNMessenger Modelo = new MSNMessenger();
        private MSNMessengerView Vista = new MSNMessengerView();

        public void Funcionar()
        {
            while (true)
            {
                int Opcion = (int)this.Vista.MostarMenuPrincipal();
                switch (Opcion)
                {
                    case 0:
                        Modelo.Refrescar();
                        NuevoContacto();
                        break;
                    case 1:
                        Modelo.Refrescar();
                        NuevoChat();
                        break;
                    case 2:
                        Modelo.Refrescar();
                        VerChat(this.Modelo.Chats);
                        break;
                    case 3:
                        Modelo.Refrescar();
                        BuscarChat();
                        break;
                }
            }
        }
        public void NuevoContacto()
        {
            ContactoViewModel cvm = this.Vista.MostrarPantallaCrearContacto();

            if (cvm == null)
            {
                return;
            }
            else
            {
                Contacto Aux = new Contacto(cvm.Nombre, cvm.Info);

                if (!Modelo.AgregarContacto(Aux))
                {
                    Vista.MostrarError("Error. Contacto ya agendado");
                }
            }
        }

        public void Chatear(Chat chat)
        {
            Mensaje msj;

            chat.ActualizarVisto();

            if (chat == null)
            {
                return;
            }
            else
            {
                ChatViewModel cvm = new ChatViewModel()
                {
                    Nombre = chat.Contacto.Nombre,
                    Info = chat.Contacto.Info,
                    Mensajes = new List<MensajeViewModel>()
                };
                for (int i = 0; i < chat.Mensajes.Count; i++)
                {
                    cvm.Mensajes.Add(new MensajeViewModel()
                    {
                        FechaHora = chat.Mensajes[i].FechaHora,
                        Texto = chat.Mensajes[i].texto,
                        EsMio = chat.Mensajes[i].esmio,
                        Visto = chat.Mensajes[i].visto,
                        MensajeCitadoIndex = chat.IndexDe(chat.Mensajes[i].mensajeRespondido)
                    });
                }

                MensajeViewModel mvm = this.Vista.MostrarPantallaDeChat(cvm);

                while (mvm != null)
                {

                    if (mvm.MensajeCitadoIndex == -1)
                    {
                        msj = new Mensaje()
                        {
                            FechaHora = mvm.FechaHora,
                            texto = mvm.Texto,
                            esmio = mvm.EsMio,
                            visto = mvm.Visto,
                            mensajeRespondido = null
                        };
                    }
                    else
                    {
                        msj = new Mensaje()
                        {
                            FechaHora = mvm.FechaHora,
                            texto = mvm.Texto,
                            esmio = mvm.EsMio,
                            visto = mvm.Visto,
                            mensajeRespondido = chat.Mensajes[mvm.MensajeCitadoIndex]
                        };
                    }

                    chat.Enviar(msj);
                    chat.ActualizarVisto();
                    chat.Refrescar();
                    cvm.Mensajes.Clear();

                    for (int i = 0; i < chat.Mensajes.Count; i++)
                    {
                        cvm.Mensajes.Add(new MensajeViewModel()
                        {
                            FechaHora = chat.Mensajes[i].FechaHora,
                            Texto = chat.Mensajes[i].texto,
                            EsMio = chat.Mensajes[i].esmio,
                            Visto = chat.Mensajes[i].visto,
                            MensajeCitadoIndex = chat.IndexDe(chat.Mensajes[i].mensajeRespondido)
                        });
                    }

                    mvm = this.Vista.MostrarPantallaDeChat(cvm);
                }
            }
            Console.ReadKey();
        }

        public void VerChat(List<Chat> Chats)
        {
            int aux = 0;

            List<ChatItemViewModel> ChatsVM = new List<ChatItemViewModel>();

            for (int i = 0; i < Chats.Count; i++)
            {
                ChatsVM.Add(new ChatItemViewModel()
                { 
                Nombre = Chats[i].Contacto.Nombre,
                Info = Chats[i].Contacto.Info,
                CantMsjsNuevos = Chats[i].ContarNoLeidos(),
                UltimoMsj = Chats[i].UltimoMsj()
                });
            }

            aux = this.Vista.MostrarPantallaSeleccionDeChat(ChatsVM);

            if (aux != -1)
            {
                Chatear(Chats[aux]);
            }
        }

        public void NuevoChat()
        {
            int aux = 0;
            List<ContactoViewModel> clivm = new List<ContactoViewModel>();

            for (int i = 0; i < Modelo.contactos.Count; i++)
            {
                clivm.Add(new ContactoViewModel()
                {
                    Nombre = this.Modelo.contactos[i].Nombre,
                    Info = this.Modelo.contactos[i].Info
                });
            }

            aux = this.Vista.MostrarPantallaSeleccionDeContacto(clivm);

            if (aux != (-1))
            {
                
                Chat auxChat =  this.Modelo.AgregarChat(this.Modelo.contactos[aux]);

                Chatear(auxChat);
            }
        }

        public void BuscarChat()
        {
            string AuxString = this.Vista.MostrarPantallaDeBusqueda();

            List<Chat> AuxChat = new List<Chat>();

            if (AuxString == null)
            {
                return;
            }
            else
            {
                AuxChat = this.Modelo.BuscarChats(AuxString);

                this.VerChat(AuxChat);
            }
        
        }
    }
}
 

           
