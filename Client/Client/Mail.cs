using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Mail
    {
        static void MailCheck()
        {
            EmailCheck();
        }
        public static void EmailCheck()
        {
            Console.WriteLine("Podaj swoj E-mail");
            string Getmail = Console.ReadLine();
            if (Getmail == "kacper.pl")//Adminowanie grą
            {
                Console.Clear();
                Console.WriteLine("Witamy ScrumMastera!");
            }
            else if (Getmail == "sebastian.pl")//Oglądanie gry
            {
                Console.Clear();
                Console.WriteLine("Witamy ProductOwnera!");
            }
            else//Granie w grę
            {
                Console.Clear();
                Console.WriteLine("Witamy Programistę!");
            }

        }
    }
}