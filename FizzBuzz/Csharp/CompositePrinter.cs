using System;
using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using System.Linq;

namespace FizzBuzz
{
    public class CompositePrinter : IPrinter
    {
        private readonly IEnumerable<IPrinter> m_Printers;

        public CompositePrinter(IEnumerable<IPrinter> printers)
        {
            m_Printers = printers;
        }

        public string Print(int number)
        {
            var output = string.Concat(m_Printers.Select(printer => printer.Print(number)));
            return string.IsNullOrWhiteSpace(output) ? Convert.ToString(number) : output;
        }
    }

    [TestFixture]
    public class CompositePrinterTests
    {
        [Test]
        public void Print_ForOnePrinterAndNumberWithOutput_ShouldReturnOutput()
        {
            const int Number = 1;
            const string Output = "Fizz";

            var printer = CreatePrinter(Number, Output);
            var compositePrinter = new CompositePrinter(new[] { printer });

            Assert.AreEqual(Output, compositePrinter.Print(Number));
        }

        [Test]
        public void Print_ForOnePrinterAndNumberWithoutOutput_ShouldReturnOutput()
        {
            const int NumberUnderTest = 1;
            const int OtherNumber = 2;
            const string Output = "Fizz";

            var printer = CreatePrinter(OtherNumber, Output);
            var compositePrinter = new CompositePrinter(new[] { printer });

            Assert.AreEqual(NumberUnderTest.ToString(), compositePrinter.Print(NumberUnderTest));
        }

        [Test]
        public void Print_ForTwoPrintersAndNumberWithoutOutputInBothPrinters_ShouldReturnOutput()
        {
            const int NumberUnderTest = 1;
            const int OtherNumber = 2;
            const string Output1 = "Fizz";
            const string Output2 = "Buzz";

            var printer1 = CreatePrinter(OtherNumber, Output1);
            var printer2 = CreatePrinter(OtherNumber, Output2);
            var compositePrinter = new CompositePrinter(new[] { printer1, printer2 });

            Assert.AreEqual(NumberUnderTest.ToString(), compositePrinter.Print(NumberUnderTest));
        }

        [Test]
        public void Print_ForTwoPrintersAndNumberWithOutputInSecondPrinter_ShouldReturnOutput()
        {
            const int NumberUnderTest = 1;
            const int OtherNumber = 2;
            const string Output1 = "Fizz";
            const string Output2 = "Buzz";

            var printer1 = CreatePrinter(OtherNumber, Output1);
            var printer2 = CreatePrinter(NumberUnderTest, Output2);
            var compositePrinter = new CompositePrinter(new[] { printer1, printer2 });

            Assert.AreEqual(Output2, compositePrinter.Print(NumberUnderTest));
        }

        [Test]
        public void Print_ForTwoPrintersAndNumberWithOutputInBothPrinters_ShouldReturnOutput()
        {
            const int NumberUnderTest = 1;
            const string Output1 = "Fizz";
            const string Output2 = "Buzz";

            var printer1 = CreatePrinter(NumberUnderTest, Output1);
            var printer2 = CreatePrinter(NumberUnderTest, Output2);
            var compositePrinter = new CompositePrinter(new[] { printer1, printer2 });

            Assert.AreEqual(string.Concat(Output1, Output2), compositePrinter.Print(NumberUnderTest));
        }

        private IPrinter CreatePrinter(int number, string output)
        {
            var printer = MockRepository.GenerateMock<IPrinter>();
            printer.Stub(m => m.Print(number)).Return(output);
            return printer;
        }
    }
}