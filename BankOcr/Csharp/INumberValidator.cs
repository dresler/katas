namespace BankOcr
{
    /// <summary>
    /// Number validator.
    /// </summary>
    public interface INumberValidator
    {
        /// <summary>
        /// Validates account number.
        /// It validates format and checksum.
        /// </summary>
        /// <param name="accountNumber">Account number.</param>
        /// <returns>True, if valid format and checksum, otherwise false.</returns>
        bool IsValid(string accountNumber);
    }
}