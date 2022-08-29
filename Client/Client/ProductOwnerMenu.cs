
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class ProductOwner
    {
        public static void PoM()
        {
            Console.Title = "Devform";
            Console.WriteLine("\nOczekiwanie na wysłanie tematu przez ScrumMastera");
            while (!ClientApp.ReceiveID())
            {

            }
        }
    }
}

