using System;
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
            Console.WriteLine("\ntest");
            while (!ClientApp.ReceiveID())
            {

            }
            ClientApp.SendCard();
            
            
        }
    }
}
