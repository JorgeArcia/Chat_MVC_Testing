using ConsoleApp_p2.Controlador;
using System;


namespace ConsoleApp_p2
{
    class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {
                Controller cc = new Controller();
                cc.Funcionar();
            }
        }
    }
}
