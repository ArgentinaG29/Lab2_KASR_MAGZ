using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2_KASR_MAGZ.Models
{
    public class Calculations
    {
        public static int resupply()
        {
            var NumberRandom = new Random();
            int NewStock = NumberRandom.Next(1,15);
            return NewStock;
        }

        public static int NTotal(IEnumerable<Medicine> NameList)
        {
            int Total = 0;
            for(int i =0; i < NameList.Count(); i++)
            {
                int part1 = Convert.ToInt32(NameList.ElementAt(i).Price) * Convert.ToInt32(NameList.ElementAt(i).Stock);
                Total += part1;
            }
            return Total;
        }
    }
}
