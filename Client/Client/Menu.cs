using DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client
{
    public class Menu : Talia
    {
        private static List<string> items = new List<string>();
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
            Console.WriteLine("1) Rozpocznij grę");
            Console.WriteLine("2) Dodaj temat");
            Console.WriteLine("3) Wyświetl dodane tematy");
            Console.WriteLine("4) Wybierz talie");
            Console.Write("\r\nWybierz opcje: ");

            switch (Console.ReadLine())
            {
                case "1":
                
                    break;
                case "2":
                    Console.Clear();
                    GameAdd();
                    break;
                case "3":
                    ShowID();
                    Console.ReadKey();
                    break;
                case "4":
                    Console.Clear();
                    Talia.Talie();
                    //SelectTalia();
                    Console.ReadKey();
                    break;
            }
        }
        public static void SelectTalia()
        {
            Console.Write("Wybierz Kartę: ");
            string Talia = Console.ReadLine();
            GetTalia(Talia);
        }
        public static void GetTalia(string tekst1)
        {
            CardPacksRequest CardRequest = new CardPacksRequest()
            {
                packName = tekst1,
            };

            string json = JsonSerializer.Serialize(CardRequest);
            
        }
        private static string CaptureID()
        { 
            Console.Write("Podaj ID rozgrywki: ");
            return Console.ReadLine();

        }
        public static string CaptureInput()
        {
            Console.Write("Podaj Temat rozgrywki: ");
            return Console.ReadLine();
        }

        private static void GameAdd()
        {
            Console.WriteLine("Rozgrywka: ");
            items.Add("ID: "+CaptureID()+"\nTemat rozgrywki: "+ CaptureInput());

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