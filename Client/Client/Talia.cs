﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client 
{
    public class Talia 
    {
        public static List<string> karty = new List<string>()
        {
            "0","1","2","3","5","8","13","21","34","55","89"
        };
        public static void Talie()
        {
            Console.WriteLine("1) Talia Fibonacci");
            switch (Console.ReadLine())
            {
                case "1":
                    //Wypisz();
                    break;
            }
        }
        public static void Wypisz()
        {
            foreach (var talia in karty)
            {
                Console.WriteLine(talia);
            }
        }
    }
}