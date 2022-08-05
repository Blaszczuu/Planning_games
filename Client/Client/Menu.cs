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
            Console.WriteLine("2) Wybierz talie");
            Console.Write("\r\nWybierz opcje: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    StartGame.Description();
                    break;
                case "2":
                    Console.Clear();
                    Talia.Talie();
                    //SelectTalia();
                    Console.ReadKey();
                    break;
            }
        }

        private static void StartGamee()
        {
            // Wyslac tematy do serwera
        }

        public static void SelectTalia()
        {
            Console.ReadKey();
            
        }
        public static void GetTalia(string tekst1)
        {
            CardPacksRequest TaliaRequest = new()
            {
                packName = tekst1,
            };

            string json = JsonSerializer.Serialize(TaliaRequest);
            
        }
        public static string CaptureID()
        { 
            Console.Write("Podaj ID rozgrywki: ");
            return Console.ReadLine();

        }
        public static string CaptureInput()
        {
            Console.Write("Podaj Temat rozgrywki: ");
            return Console.ReadLine();
        }

        public static void GameAdd()
        {
            Console.WriteLine("Rozgrywka: ");
            items.Add("ID: "+CaptureID()+"\nTemat rozgrywki: "+ CaptureInput());

        }

        public static void ShowID()
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