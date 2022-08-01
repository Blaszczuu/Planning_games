using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Menu : Talia
    {
        private static List<string> items = new List<string>();
        private static TaliaXyz talieKart = new TaliaXyz();
        public static void MainM()
        {
            Console.Title = "ServerMenu";
            bool showMenu = true;
            while (showMenu)
            {
               MainMenu();
            }

        }
        private static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Wybierz opcje:");
            Console.WriteLine("1) Dodaj temat");
            Console.WriteLine("2) Wyświetl dodane tematy");
            Console.WriteLine("3) Wybierz talie");
            Console.Write("\r\nWybierz opcje: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    GameAdd();
                    break;
                case "2":
                    ShowID();
                    Console.ReadKey();
                    break;
                case "3":
                    Console.Clear();
                    var talia = talieKart.GetDostepneTalieKart();
                    for (int i = 0; i < talia.Count; i++)
                    {
                        Console.WriteLine(i+1 + ") " + talia[i]);
                    }
                    Console.ReadKey();
                    break;
            }
        }

        private static string CaptureInput()
        {
            Console.Write("Podaj temat rozgrywki: ");
            return Console.ReadLine();
        }

        private static void GameAdd()
        {
            Console.WriteLine("Rozgrywka: ");
            items.Add(CaptureInput());

        }

        private static void ShowID()
        {
            Console.Clear();
            Console.WriteLine("Dodane ID rozgrywek");
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }

        }
    }
}