using System;
using System.Linq;

namespace FizzBuzz
{
    class Program
    {
        static void Main(string[] args)
        {
            //PrintByClassicFizzBuzzPrinter();
            PrintByCompositePrinter();

            Console.ReadKey();
        }

        private static void PrintByCompositePrinter()
        {
            var printers = new[]
                {
                    new PredicatePrinter(number => number % 3 == 0 || Convert.ToString(number).Contains('3'), "Fizz"),
                    new PredicatePrinter(number => number % 5 == 0, "Buzz"),
                    new PredicatePrinter(number => number % 7 == 0, "Whizz"),
                };

            PrintFirst100Numbers(new CompositePrinter(printers));
        }

        private static void PrintByClassicFizzBuzzPrinter()
        {
            PrintFirst100Numbers(new FizzBuzzPrinter());
        }

        private static void PrintFirst100Numbers(IPrinter printer)
        {
            Enumerable.Range(1, 100).ToList().ForEach(number => Console.WriteLine(printer.Print(number)));
        }
    }
}
