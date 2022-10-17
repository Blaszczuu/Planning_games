using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;

namespace Server
{
    public class Calculator
    {
        public static int Calculate(List<int> votes)
        {
            var fibo = new[] { 0, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89 };

            double average = Math.Round(Convert.ToDouble(votes.Sum()) / votes.Count());
            votes.Clear();
            return fibo.First(element => element >= average);
        }
    }
}
