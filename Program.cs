using System;
using System.Text.RegularExpressions;

namespace AiSP_NZ2_2021_2022
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            input = Console.ReadLine();
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input) || input.Length > 5000)
                throw new NotImplementedException();

            int i = 0;

            bool[] brojeviCheck = new bool[input.Length];
            for (i = 0; i < input.Length; i++)
                brojeviCheck[i] = false;

            List<int> brojevi = new List<int>();
            i = 0;
            foreach(char c in input)
            {
                if (char.IsNumber(c))
                {
                    brojeviCheck[i] = true;
                }
                i++;
            }
            for (i = 0; i < brojeviCheck.Length; i++)
            {
                string temp = string.Empty;
                while (brojeviCheck[i])
                {
                    temp += input[i];
                    i++;
                }
                if (!string.IsNullOrEmpty(temp)) brojevi.Add(Int32.Parse(temp));
            }
            foreach (int integer in brojevi)
                Console.WriteLine(integer);
        }

        //public int Multiply()
        //public int Zagrade()
    }
}