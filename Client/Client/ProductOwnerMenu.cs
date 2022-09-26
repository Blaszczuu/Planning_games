
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
            Console.Title = "ProductOwner";
            Console.WriteLine("\nOczekiwanie na wysłanie tematu przez ScrumMastera");
            while (true)
            {
                while (!ClientApp.ReceiveID())
                {

                }
                while (!ClientApp.ReceiveResult())
                {

                }
            }
        }
    }
}

