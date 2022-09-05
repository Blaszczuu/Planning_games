
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class GameInstance
    {
        public static void ScrumMenu()
        {
            Console.Title = "ScrumMaster";
            ClientApp.SendCard();
            while (!ClientApp.ReceiveResult())
            {

            }

        }
    }
}
