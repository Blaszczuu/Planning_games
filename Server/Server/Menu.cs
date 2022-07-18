﻿using System;
using System.Collections.Generic;


namespace Server
{
    class Menu : Program
    {
        private static List<string> items = new List<string>();

        static void Main(string[] args)
        {
            Console.Title = "ServerMenu";
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }

        }
        private static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Wybierz opcje:");
            Console.WriteLine("1) Dodaj temat");
            Console.WriteLine("2) Wyświetl dodane tematy");
            Console.WriteLine("3) Uruchom Server");
            Console.WriteLine("4) Wyjdź z Aplikacji");
            Console.Write("\r\nWybierz opcje: ");

            switch (Console.ReadLine())
            {
                case "1":
                    GameAdd();
                    return true;
                case "2":
                    ShowID();
                    Console.ReadKey();
                    return true;
                case "3":
                    Console.WriteLine("\n");
                    ServerC();
                    return true;
                case "4":
                    return false;
                default:
                    return true;
                case "5":
                    // miejsce na wybór talii
                    return true;
            }
        }

        private static string CaptureInput()
        {
            Console.Write("Podaj temat rozgrywki: ");
            return Console.ReadLine();
        }

        private static void GameAdd()
        {
            Console.Clear();
            Console.WriteLine("Rozgrywka: ");
            items.Add(CaptureInput());
            //DisplayResult(CaptureInput());

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

        private static void DisplayResult(string message)
        {
            Console.WriteLine($"\r\nTwoja rozgrywka '{message}' została dodana do listy tematów");
            Console.Write("\r\nNaciśnij enter aby wrócić do menu");
            Console.ReadLine();
        }


    }
}