using NUnit.Framework;

namespace BankOcr
{
    [TestFixture]
    public class EntryParserTests
    {
        private const string NEW_LINE = "\r\n";

        [TestCase(
            "    _  _     _  _  _  _  _ ", 
            "  | _| _||_||_ |_   ||_||_|",
            "  ||_  _|  | _||_|  ||_| _|", 
            "123456789")]
        [TestCase(
            "    _  _  _  _  _  _     _ ", 
            "  | _| _||_||_ |_   ||_|| |",
            "  | _| _||_| _| _|  |  ||_|", 
            "133855740")]
        [TestCase(
            "    _  _  _  _  _  _     _ ", 
            "| | _|  ||_||_ |_   ||_|| |",
            "  | _| _||_| || _|  |  | _|", 
            "?3?8?574?")]
        public void ParseAccountNumbers_ForGivenContent_ShouldReturnExpected(string line1, string line2, string line3, string expected)
        {
            Assert.AreEqual(expected, new EntryParser().Parse(line1 + NEW_LINE + line2 + NEW_LINE + line3));
        }
    }
}