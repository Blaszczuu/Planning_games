using System;
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

            ClientApp.SendI();//4 razy sie wykonuje przy sprint 0, nie przechodzi do while nizej po jednym wykonaniu
            //while (!ClientApp.ReceiveID())
            //{
            //}
            Console.WriteLine("\n");
            //ClientApp.SendCard();
            //while (!ClientApp.ReceiveResult())
            //{
            //}
            Console.ReadKey();
        }
    }
}
