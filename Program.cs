using System;
using System.Linq;
using System.Collections.Generic;

namespace AiSP_NZ2_2021_2022
{
    public class DivideByZeroException : Exception
    {
        public DivideByZeroException(string message) : base(message) { }
    }
    public class UnknownOperatorException : Exception
    {
        public UnknownOperatorException(string message) : base(message) { }
    }
    public class EmptyInputException : Exception
    {
        public EmptyInputException(string message) : base(message) { }
    }
    public class InputOverflowException : Exception
    {
        public InputOverflowException(string message) : base(message) { }
    }
    public class IllegalInputException : Exception
    {
        public IllegalInputException(string message, char c) : base(message)
        {
            Console.WriteLine($"{c} je nedopušteni znak.");
        }
    }
    static class Program
    {
        public static bool ProvjeraPrioriteta(this char lijeviOperator, char desniOperator)
        {
            if (desniOperator.Equals('(') || desniOperator.Equals(')'))
                return false;
            else if ((lijeviOperator.Equals('*') || lijeviOperator.Equals('/'))
                && (desniOperator.Equals('+') || desniOperator.Equals('-')))
                return false;
            return true;
        }
        public static int IzvrsavanjeOperacije(this char Operator, int desnaVrijednost, int lijevaVrijednost)
        {
            if (Operator.Equals('*')) return lijevaVrijednost * desnaVrijednost;
            else if (Operator.Equals('/'))
            {
                if (desnaVrijednost.Equals(0))
                    throw new DivideByZeroException("Pokušano je dijeliti s nulom. Nedopušteni potez.");
                return lijevaVrijednost / desnaVrijednost;
            }
            else if (Operator.Equals('+')) return lijevaVrijednost + desnaVrijednost;
            else if (Operator.Equals('-')) return lijevaVrijednost - desnaVrijednost;
            throw new UnknownOperatorException("Korišten je nedefinirani operator. Nedopušteni potez.");
        }
        public static bool IsOperator(this char inputChar, char[] operators)
        {
            foreach (char Operator in operators)
                if (inputChar.Equals(Operator)) return true;
            return false;
        }
        static void Main(string[] args)
        {
            char[] data = new char[0];
            char[] allowedInputOperators = new char[] { '*', '/', '-', '+', '(', ')', ' ' };
            char[] trueOperators = new char[] { '*', '/', '-', '+' };

            try
            {
                char[] inputString = Console.ReadLine().ToCharArray();
                if (inputString.Length.Equals(0))
                    throw new EmptyInputException("Uneseni string je prazan. Nedopušteni potez.");
                else if (inputString.Length > 5000)
                    throw new InputOverflowException("Uneseni string premašjue ograničenje od 5000 znakova. Nedopušteni potez.");
                else foreach (char inputChar in inputString)
                        if (!inputChar.IsOperator(allowedInputOperators) && !char.IsDigit(inputChar))
                            throw new IllegalInputException("U stringu se nalazi nedopušteni znak. Nedopušteni potez.", inputChar);
                data = inputString;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Environment.Exit(1);
            }

            Stack<char> operatori = new Stack<char>();
            Stack<int> brojevi = new Stack<int>();

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].Equals('(')) operatori.Push(data[i]);

                else if (data[i].Equals(')'))
                {
                    while (!operatori.Peek().Equals('('))
                    {
                        try
                        {
                            brojevi.Push(operatori.Pop().IzvrsavanjeOperacije(brojevi.Pop(), brojevi.Pop()));
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception.Message);
                            Environment.Exit(1);
                        }
                    }
                    operatori.Pop();
                }

                else if (data[i].IsOperator(trueOperators))
                {
                    while (operatori.Any() && data[i].ProvjeraPrioriteta(operatori.Peek()))
                    {
                        try
                        {
                            brojevi.Push(operatori.Pop().IzvrsavanjeOperacije(brojevi.Pop(), brojevi.Pop()));
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception.Message);
                            Environment.Exit(1);
                        }
                    }
                    operatori.Push(data[i]);
                }

                else if (char.IsDigit(data[i]))
                {
                    string tempValue = new string(data[i], 1);
                    while (i + 1 < data.Length && char.IsDigit(data[i + 1]))
                        tempValue += data[++i];
                    brojevi.Push(int.Parse(tempValue));
                }

                else if (data[i].Equals(' ')) continue;
            }

            while (operatori.Any())
            {
                try
                {
                    brojevi.Push(operatori.Pop().IzvrsavanjeOperacije(brojevi.Pop(), brojevi.Pop()));
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    Environment.Exit(1);
                }
            }

            Console.WriteLine(brojevi.Pop());
        }
    }
}