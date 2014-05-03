using NUnit.Framework;

namespace BankOcr
{
    [TestFixture]
    public class SimilarNumberGeneratorTests
    {
        [TestCase("123456789", 
            "723456789", "129456789", "123466789", "123496789", "123455789", "123458789", "123456189", "123456709", "123456769", "123456799",
            "123456783", "123456785", "123456788")]
        [TestCase("1222442?2",
            "122244202", "122244212", "122244222", "122244232", "122244242", "122244252", "122244262", "122244272", "122244282", "122244292")]
        [TestCase("1222442??")]
        [TestCase("422244222")]
        public void Generate_ForGivenNumber_ShouldReturnExpected(string accountNumber, params string[] expected)
        {
            var generator = new SimilarNumberGenerator();
            CollectionAssert.AreEquivalent(expected, generator.Generate(accountNumber));
        }
    }
}