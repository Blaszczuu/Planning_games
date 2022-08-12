using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class GameInstance
    {
        public static void ScrumMenu()
        {
            Console.Title = "ScrumMaster";
            Console.WriteLine("ID Problemu: ");
            Console.WriteLine("Estymowany Problem: ");
            Console.WriteLine("\n\nRezultat Głosowania: ");
            ClientApp.SendCard();

        }
    }
}
