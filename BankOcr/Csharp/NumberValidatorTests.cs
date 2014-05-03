using NUnit.Framework;

namespace BankOcr
{
    [TestFixture]
    public class NumberValidatorTests
    {
        [TestCase("123456789", false)]
        [TestCase("457508100", true)]
        [TestCase("4?7508100", false)]
        public void IsValid_ForGivenAccountNumber_ShouldReturnExpected(string accountNumber, bool expected)
        {
            var validator = new NumberValidator();
            Assert.AreEqual(expected, validator.IsValid(accountNumber));
        }
    }
}