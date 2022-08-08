using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class StartGame
    {
        public static void Description()
        {
            Console.WriteLine("1) Rozpocznij Głosowanie");
            Console.WriteLine("2) Dodaj temat");
            Console.WriteLine("3) Wyświetl dodane tematy");
            Console.WriteLine("4) Wróć");
            Console.Write("\r\nWybierz opcje: ");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    break;
                case "2":
                    Console.Clear();
                    ClientApp.SendI();
                    while (!ClientApp.ReceiveID())
                    {

                    }
                    Console.Clear();
                    Description();
                    break;
                case "3":
                    Menu.ShowID();
                    Console.ReadKey();
                    Console.Clear();
                    Description();
                    break;
                case "4":
                    break;

            }
        }
    }
}
