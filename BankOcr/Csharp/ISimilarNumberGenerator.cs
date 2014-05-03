using System.Collections.Generic;

namespace BankOcr
{
    /// <summary>
    /// Generator of similar numbers.
    /// </summary>
    public interface ISimilarNumberGenerator
    {
        /// <summary>
        /// Generates similar numbers.
        /// Similar number differs from original in one similar digit.
        /// </summary>
        /// <param name="accountNumber">Account number.</param>
        /// <returns>Collection of similar numbers.</returns>
        IEnumerable<string> Generate(string accountNumber);
    }
}