using NUnit.Framework;

namespace BowlingGame
{
    [TestFixture]
    public class GameTests
    {
        [TestCase("12345123451234512345", 60)]
        [TestCase("1/5-----------------", 20)]
        [TestCase("1/51----------------", 21)]
        [TestCase("X51----------------", 22)]
        [TestCase("9-9-9-9-9-9-9-9-9-9-", 90)]
        [TestCase("XX13--------------", 39)]
        [TestCase("XX13-------------/X", 59)]
        [TestCase("5/5/5/5/5/5/5/5/5/5/5", 150)]
        [TestCase("XXXXXXXXXXXX", 300)]
        public void GetScore_ForGivenScoreLine_ShouldReturnExpectedGameScore(string scoreLine, int expectedGameScore)
        {
            var game = new Game();
            Assert.AreEqual(expectedGameScore, game.GetScore(scoreLine));
        }
    }
}