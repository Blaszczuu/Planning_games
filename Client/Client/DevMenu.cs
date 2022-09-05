
﻿using System;
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
            Console.Title = "Devform";
            Console.WriteLine("\nOczekiwanie na wysłanie tematu przez ScrumMastera");

            while (!ClientApp.ReceiveID())
            {

            }
            Console.WriteLine("\n");
            ClientApp.SendCard();   
        }
    }
}
