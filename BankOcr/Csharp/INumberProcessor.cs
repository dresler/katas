namespace BankOcr
{
    /// <summary>
    /// Number processor.
    /// </summary>
    public interface INumberProcessor
    {
        /// <summary>
        /// Process account number.
        /// </summary>
        /// <param name="accountNumber">Account number.</param>
        /// <returns>Valid number, similar number or invalid number with error mark.</returns>
        string Process(string accountNumber);
    }
}