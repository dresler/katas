using System.Text;
using NUnit.Framework;

namespace FizzBuzz
{
    public class FizzBuzzPrinter : IPrinter
    {
        public string Print(int number)
        {
            var output = new StringBuilder();

            if (number % 3 == 0) output.Append("Fizz");
            if (number % 5 == 0) output.Append("Buzz");
            if (output.Length == 0) output.Append(number);

            return output.ToString();
        }
    }

    [TestFixture]
    public class FizzBuzzPrinterTests
    {
        [TestCase(1, "1")]
        [TestCase(2, "2")]
        [TestCase(3, "Fizz")]
        [TestCase(4, "4")]
        [TestCase(5, "Buzz")]
        [TestCase(15, "FizzBuzz")]
        public void Print_ForNumber_ShouldReturnExpected(int number, string expected)
        {
            Assert.AreEqual(expected, new FizzBuzzPrinter().Print(number));
        }
    }
}