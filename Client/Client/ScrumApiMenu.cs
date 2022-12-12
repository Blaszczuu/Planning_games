using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class ScrumApiMenu
    {
        public static void ApiSender()
        {
            Console.Title="ApiMenu";
            ClientApp.GetSprintName();
            Console.WriteLine("Pobieram dane z wybranego sprintu..." );
            while(!ClientApp.ReceiveSprintDetails())
            {
            }
            Console.WriteLine("Pobieranie danych zakonczone");
            Console.ReadKey();
            

        }
    }
}
