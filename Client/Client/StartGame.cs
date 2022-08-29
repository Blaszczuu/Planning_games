
﻿using System;
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
            Console.Title = "ScrumMenu";
            Console.WriteLine("1) Rozpocznij Głosowanie");
            Console.WriteLine("2) Dodaj temat");
            Console.WriteLine("3) Wyświetl dodane tematy");
            Console.WriteLine("4) Wyślij dodany temat do estymacji");
            Console.WriteLine("5) Wróć");
            Console.Write("\r\nWybierz opcje: ");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    GameInstance.ScrumMenu();
                    break;
                case "2":
                    Console.Clear();
                    Menu.GameAdd();
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
                    Console.Clear();
                    ClientApp.SendI();
                    GameInstance.ScrumMenu();
                    break;
                case "5":
                    break;

            }
        }
    }
}
