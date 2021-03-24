using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Linq;

namespace ProjectPartB.Data
{
    abstract class InputUser : Check
    {
        public int Integer(int min, int max)
        {
            int tempint;
            string input = Console.ReadLine();
            while (!int.TryParse(input, out tempint) || (tempint < min) || (tempint > max))
            {
                Console.WriteLine("Invalid Number");
                Console.Write($"Please type a number between {min} and {max}: ");
                input = Console.ReadLine();
            }
            return tempint;
        }
        public double Double(int min, int max)
        {

            double tempint;
            string input = Console.ReadLine();
            while (!double.TryParse(input, out tempint) || (tempint < min) || (tempint > max))
            {
                Console.WriteLine("Invalid Number");
                Console.Write($"Please type a number between {min} and {max}:\n> ");
                input = Console.ReadLine();
            }
            return tempint;

        }
        public string String()
        {

            Console.Write("First letter must be capital and use only letters [Vasilis]\n>");
            string temp = Console.ReadLine();
            if (temp == "exit") { return temp; }
            bool check = Regex.Match(temp, @"^[A-Z][a-z]+?$").Success;
            while (check == false)
            {
                Console.Write("First letter must be capital and use only letters[Santas]\n(for example:Mark)\n>");
                temp = Console.ReadLine();
                check = Regex.Match(temp, @"^[A-Z][a-z]+?$").Success;
            }
            return temp;
        }
        public DateTime Dates(DateTime min, DateTime max)
        {
            byte i = 0; // ο χρηστης μετα απο πεντε αποτυχημενες επιλογες να μπορει να αποχωρει
            DateTime date = new DateTime();
            while (date < min || date > max)
            {
                string temp = Console.ReadLine();
                while (!DateTime.TryParseExact(temp, "d", null, 0, out date))
                {
                    i++;
                    Console.WriteLine("Wrong Format");
                    if (i > 5)
                    {
                        Console.Write("Do you want to exit? <Y> or <N>?:\n>");
                        if (YesOrNo()) { return DateTime.Now; }
                    }
                    Console.Write("The Format is (dd/MM/yyyy)\n>");
                    temp = Console.ReadLine();
                }
                i++;
                if (date < min || date > max)
                { Console.Write($"The Date must be inside {min:d} and {max:d}!\nTry Again\n>"); }
                if (i > 5)
                {
                    Console.Write("Do you want to exit? <Y> or <N>?:\n>");
                    if (YesOrNo()) { return DateTime.Now; }
                }
            }
            return date;
        }
        public int IntegerId<T>(List<T> list) where T : class
        {
            int tempint, size;
            size = list.Count;
            int[] id = new int[size];
            Random random = new Random();

            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.Name.IndexOf("id", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    string input = Console.ReadLine();
                    int i = 0;
                    do
                    {
                        while (!int.TryParse(input, out tempint))
                        {
                            Console.WriteLine("Invalid Number");
                            Console.Write("Please type a correct Id Number: ");
                            input = Console.ReadLine();
                        }
                        if (tempint == 0)
                        { return 0; }
                        foreach (T item in list)
                        {
                            if (tempint == (int)property.GetValue(item, null))
                            {
                                return tempint;
                            }
                        }
                        Console.WriteLine("Invalid Number");
                        Console.Write("Please type a correct Id Number: ");
                        input = Console.ReadLine();
                        i++;
                    } while (true || i == 5);
                    return 0;
                }
            }
            return 0;
        }
    }
}
