using System.Linq;
using System.Text.RegularExpressions;

namespace BankOcr
{
    /// <summary>
    /// Number validator.
    /// </summary>
    public class NumberValidator : INumberValidator
    {
        /// <summary>
        /// Validates account number.
        /// It validates format and checksum.
        /// </summary>
        /// <param name="accountNumber">Account number.</param>
        /// <returns>True, if valid format and checksum, otherwise false.</returns>
        public bool IsValid(string accountNumber)
        {
            return IsValidFormat(accountNumber) && IsValidCheckSum(accountNumber);
        }

        private int CountCheckSum(string accountNumber)
        {
            return accountNumber.Select((digit, index) => (digit - 48) * (index + 1)).Sum();
        }

        private bool IsValidFormat(string accountNumber)
        {
            const string NumberPattern = "[0-9]{9}";
            return Regex.IsMatch(accountNumber, NumberPattern, RegexOptions.None);
        }

        private bool IsValidCheckSum(string accountNumber)
        {
            var checkSum = CountCheckSum(accountNumber);
            return checkSum % 11 == 0;
        }
    }
}