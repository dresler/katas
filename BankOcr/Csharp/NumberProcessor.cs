using System.Collections.Generic;
using System.Linq;

namespace BankOcr
{
    /// <summary>
    /// Number processor.
    /// </summary>
    public class NumberProcessor : INumberProcessor
    {
        private readonly ISimilarNumberGenerator m_SimilarNumberGenerator;
        private readonly INumberValidator m_NumberValidator;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="numberValidator">Number validator.</param>
        /// <param name="similarNumberGenerator">Similar number generator.</param>
        public NumberProcessor(INumberValidator numberValidator, ISimilarNumberGenerator similarNumberGenerator)
        {
            m_NumberValidator = numberValidator;
            m_SimilarNumberGenerator = similarNumberGenerator;
        }

        /// <summary>
        /// Process account number.
        /// </summary>
        /// <param name="accountNumber">Account number.</param>
        /// <returns>Valid number, similar number or invalid number with error mark.</returns>
        public string Process(string accountNumber)
        {
            if (m_NumberValidator.IsValid(accountNumber)) return accountNumber;

            var similarValidNumbers = GetSimilarValidNumbers(accountNumber);

            if (similarValidNumbers.Any())
            {
                return similarValidNumbers.Count() == 1 ? similarValidNumbers.First() : string.Format("{0} AMB", accountNumber);
            }
            else
            {
                var hasWrongDigit = accountNumber.IndexOf('?') >= 0;
                return string.Format("{0} {1}", accountNumber, hasWrongDigit ? "ILL" : "ERR");
            }
        }

        private IEnumerable<string> GetSimilarValidNumbers(string accountNumber)
        {
            var similarNumbers = m_SimilarNumberGenerator.Generate(accountNumber);
            var similarValidNumbers = similarNumbers.Where(m_NumberValidator.IsValid).ToArray();

            return similarValidNumbers;
        }
    }
}