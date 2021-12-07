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

            char[] data = new char[input.Length];
            int i = 0;
            foreach (char c in input)
            {
                data[i] = c;
                i++;
            }
            foreach(char s in data) Console.WriteLine(s);


        }
    }
}