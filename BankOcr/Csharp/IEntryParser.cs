namespace BankOcr
{
    /// <summary>
    /// Entry parser.
    /// </summary>
    public interface IEntryParser
    {
        /// <summary>
        /// Parse entry with account number.
        /// </summary>
        /// <param name="entry">Entry with account number.</param>
        /// <returns>Account number. If any digit was not recogonized then is used '?'.</returns>
        string Parse(string entry);
    }
}