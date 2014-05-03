using System;
using System.Collections.Generic;
using System.Linq;

namespace BankOcr
{
    /// <summary>
    /// Generator of similar numbers.
    /// </summary>
    public class SimilarNumberGenerator : ISimilarNumberGenerator
    {
        private const char WRONG_DIGIT = '?';

        private IDictionary<Predicate<int>, Func<string, IEnumerable<string>>> m_GenerationRules;
        private IDictionary<char, char[]> m_SimilarDigits;

        /// <summary>
        /// Ctor.
        /// </summary>
        public SimilarNumberGenerator()
        {
            CreateSimilarDigitsMap();
            CreateGenerationRules();
        }

        /// <summary>
        /// Generates similar numbers.
        /// Similar number differs from original in one similar digit.
        /// </summary>
        /// <param name="accountNumber">Account number.</param>
        /// <returns>Collection of similar numbers.</returns>
        public IEnumerable<string> Generate(string accountNumber)
        {
            var numberOfWrongDigits = accountNumber.Count(digit => digit == WRONG_DIGIT);
            var generationRuleKey = m_GenerationRules.Keys.First(ruleCondition => ruleCondition(numberOfWrongDigits));
            return m_GenerationRules[generationRuleKey](accountNumber);
        }

        private void AddMap(char digit, params char[] similarDigits)
        {
            m_SimilarDigits.Add(digit, similarDigits);
        }

        private void CreateGenerationRules()
        {
            m_GenerationRules = new Dictionary<Predicate<int>, Func<string, IEnumerable<string>>>();
            m_GenerationRules.Add(numberOfWrongDigit => numberOfWrongDigit == 0, GenerateForNumberWithoutWrongDigits);
            m_GenerationRules.Add(numberOfWrongDigit => numberOfWrongDigit == 1, GenerateForNumberWithOneWrongDigit);
            m_GenerationRules.Add(numberOfWrongDigit => numberOfWrongDigit > 1, _ => new string[0]);
        }

        private void CreateSimilarDigitsMap()
        {
            m_SimilarDigits = new Dictionary<char, char[]>();

            AddMap('0', '8');
            AddMap('1', '7');
            AddMap('2');
            AddMap('3', '9');
            AddMap('4');
            AddMap('5', '6', '9');
            AddMap('6', '5', '8');
            AddMap('7', '1');
            AddMap('8', '0', '6', '9');
            AddMap('9', '3', '5', '8');
            AddMap(WRONG_DIGIT, '0', '1', '2', '3', '4', '5', '6', '7', '8', '9');
        }

        private string CreateSimilarNumber(string accountNumber, int index, char digit)
        {
            return string.Concat(accountNumber.Substring(0, index), digit, accountNumber.Substring(index + 1));
        }

        private IEnumerable<string> GenerateForNumberWithOneWrongDigit(string accountNumber)
        {
            return GenerateSimilarForIndex(accountNumber, WRONG_DIGIT, accountNumber.IndexOf(WRONG_DIGIT));
        }

        private IEnumerable<string> GenerateForNumberWithoutWrongDigits(string accountNumber)
        {
            return accountNumber.SelectMany((digit, index) => GenerateSimilarForIndex(accountNumber, digit, index)).ToArray();
        }

        private IEnumerable<string> GenerateSimilarForIndex(string accountNumber, char digit, int index)
        {
            return m_SimilarDigits[digit].Select(similarDigit => CreateSimilarNumber(accountNumber, index, similarDigit));
        }
    }
}