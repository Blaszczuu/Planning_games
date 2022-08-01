using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class DevMenu
    {
        public static void DevM()
        {
            Console.WriteLine("\nTemat do gry + 'implementacja wysłanego tematu'");
            Console.WriteLine("\nWybierz Kartę");
            //string card = Console.ReadLine();
            ClientApp.SendMessage();

        }
    }
}
