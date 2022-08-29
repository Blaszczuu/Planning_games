<<<<<<< HEAD
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
=======
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
>>>>>>> a15b80c509ab27299c9d88f741eecc58a0a708b4
