using System;
using NUnit.Framework;

namespace FizzBuzz
{
    public class PredicatePrinter : IPrinter
    {
        private readonly Predicate<int> m_Predicate;
        private readonly string m_Output;

        public PredicatePrinter(Predicate<int> predicate, string output)
        {
            m_Predicate = predicate;
            m_Output = output;
        }

        public string Print(int number)
        {
            return m_Predicate(number) ? m_Output : string.Empty;
        }
    }

    [TestFixture]
    public class PredicatePrinterTests
    {
        [Test]
        public void Print_ForNumberCompliedWithPredicate_ShouldReturnOutput()
        {
            const string Output = "Fizz";
            const int NumberUnderTest = 1;
            var printer = new PredicatePrinter(_ => true, Output);

            Assert.AreEqual(Output, printer.Print(NumberUnderTest));
        }

        [Test]
        public void Print_ForNumberDoesntCompliedWithPredicate_ShouldReturnEmptyString()
        {
            const string Output = "Fizz";
            const int NumberUnderTest = 1;
            var printer = new PredicatePrinter(_ => false, Output);

            Assert.IsEmpty(printer.Print(NumberUnderTest));
        }
    }
}