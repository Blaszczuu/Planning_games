using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;
using System.Xml.Linq;

namespace Server
{
    public static class Calculator
    {
        public static int[] fibo = new[] { 0, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89 };

        public static int Calculate(List<int> votes)
        {
            double average = Math.Round(Convert.ToDouble(votes.Sum()) / votes.Count());
            
            return fibo.OrderBy(x => x).First(element => element >= average);
        }

        public static int Calculate_Floor(List<int> votes)
        {
            double average = Math.Floor(Convert.ToDouble(votes.Sum()) / votes.Count());

            return fibo.OrderBy(x => x).First(element => element >= average);
        }

        public static int Calculate_Ceiling(List<int> votes)
        { 
            double average = Math.Ceiling(Convert.ToDouble(votes.Sum()) / votes.Count());

            return fibo.OrderBy(x => x).First(element => element >= average);
        }
    }
}
