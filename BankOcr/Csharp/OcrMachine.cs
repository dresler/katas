using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BankOcr
{
    /// <summary>
    /// ORC bank machine.
    /// </summary>
    public class OcrMachine
    {
        private readonly INumberProcessor m_NumberProcessor;
        private readonly IEntryParser m_EntryParser;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="entryParser">Entry parser.</param>
        /// <param name="numberProcessor">Number processor.</param>
        public OcrMachine(IEntryParser entryParser, INumberProcessor numberProcessor)
        {
            m_EntryParser = entryParser;
            m_NumberProcessor = numberProcessor;
        }

        /// <summary>
        /// Process content with entries.
        /// </summary>
        /// <param name="content">Content.</param>
        /// <returns>Processed numbers.</returns>
        public IEnumerable<string> Process(string content)
        {
            var entries = GetEntries(content);
            return entries.Select(ProcessEntry).ToArray();
        }

        private string[] GetEntries(string content)
        {
            const string EntriesDelimiterPattern = "\r\n +\r\n";
            var entries = Regex.Split(content, EntriesDelimiterPattern, RegexOptions.None);
            return entries;
        }

        private string ProcessEntry(string entry)
        {
            var accountNumber = m_EntryParser.Parse(entry);
            var processedNumber = m_NumberProcessor.Process(accountNumber);
            return processedNumber;
        }
    }
}