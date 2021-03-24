using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPartB.Data
{
    abstract class Check
    {
        public bool YesOrNo()
        {
            Console.Write(">");
            string temp = Console.ReadLine();
            if (temp.ToUpper().Equals("Y"))
            {
                Console.WriteLine("");
                return true;
            }
            else if (temp.ToUpper().Equals("N"))
            {
                Console.WriteLine("");
                return false;
            }
            else
            {
                Console.WriteLine("Invalid!Type <Y> or <N>:");
                return YesOrNo();
            }

        }
        public bool StringEquals(string a, string b)
        {
            if (a.ToUpper().Equals(b.ToUpper()))
            {
                return true;
            }
            return false;
        }
        public bool ListEmpty<T>(List<T> list)
        {
            if (list.Any() == false)
            {
                return true;                //true ειναι αδεια
            }
            else
                return false;
        }
        public bool Exit(string a)
        {
            string b = a.ToLower();
            if (b.Contains("exit"))
            {
                return true;
            }
            return false;
        }
    }
}
