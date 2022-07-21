using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client 
{
    public interface ITalia
    {
        string GetNazwaTalii();

        List<string> GetTalia();
    }

    public class FibonacciTalia : ITalia
    {
        public string GetNazwaTalii()
        {
            return "Talia Fibonacci";
        }

        public List<string> GetTalia()
        {
            return new List<string>() { "0","1","2","3","5","8","13","21","34","55","89" };
        }
    }

    public class KoszulkiTalia : ITalia
    {
        public string GetNazwaTalii()
        {
            return "Talia koszulki";
        }

        public List<string> GetTalia()
        {
            return new List<string>() { "S", "M", "XL" };
        }
    }


    public class TaliaXyz
    {
        List<ITalia> listaTalii = new List<ITalia>();

        public TaliaXyz()
        {
            listaTalii.Add(new FibonacciTalia());
            listaTalii.Add(new KoszulkiTalia());
        }

        public List<string> GetDostepneTalieKart()
        {
            return listaTalii.Select(talia => talia.GetNazwaTalii()).ToList();
        }
    }






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
