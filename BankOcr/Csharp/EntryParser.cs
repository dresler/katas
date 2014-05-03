using System;
using System.Collections.Generic;
using System.Linq;

namespace BankOcr
{
    /// <summary>
    /// Entry parser.
    /// </summary>
    public class EntryParser : IEntryParser
    {
        private const string NEW_LINE = "\r\n";

        private IDictionary<string, char> m_DigitMap;

        /// <summary>
        /// Ctor.
        /// </summary>
        public EntryParser()
        {
            InitializeNumberMap();
        }

        /// <summary>
        /// Parse entry with account number.
        /// </summary>
        /// <param name="entry">Entry with account number.</param>
        /// <returns>Account number. If any digit was not recogonized then is used '?'.</returns>
        public string Parse(string entry)
        {
            var entryLines = entry.Split(new[] { NEW_LINE }, StringSplitOptions.None);
            var digits = GetDigits(entryLines);
            return new string(digits.ToArray());
        }

        private char GetDigit(string line1Fragment, string line2Fragment, string line3Fragment)
        {
            var digitKey = GetDigitKey(line1Fragment, line2Fragment, line3Fragment);
            return m_DigitMap.ContainsKey(digitKey) ? m_DigitMap[digitKey] : '?';
        }

        private string GetDigitKey(string line1Fragment, string line2Fragment, string line3Fragment)
        {
            return string.Concat(line1Fragment, line2Fragment, line3Fragment);
        }

        private IEnumerable<char> GetDigits(string[] lines)
        {
            const int EntryLineLengthInChars = 27;

            for (var i = 0; i < EntryLineLengthInChars; i += 3)
            {
                Func<string, string> getDigitFragment = line => line.Substring(i, 3);

                yield return GetDigit(getDigitFragment(lines[0]), getDigitFragment(lines[1]), getDigitFragment(lines[2]));
            }
        }

        private void InitializeNumberMap()
        {
            m_DigitMap = new Dictionary<string, char>();

            m_DigitMap[GetDigitKey(
                " _ ", 
                "| |", 
                "|_|")] = '0';
            m_DigitMap[GetDigitKey(
                "   ", 
                "  |", 
                "  |")] = '1';
            m_DigitMap[GetDigitKey(
                " _ ", 
                " _|", 
                "|_ ")] = '2';
            m_DigitMap[GetDigitKey(
                " _ ", 
                " _|", 
                " _|")] = '3';
            m_DigitMap[GetDigitKey(
                "   ", 
                "|_|", 
                "  |")] = '4';
            m_DigitMap[GetDigitKey(
                " _ ", 
                "|_ ", 
                " _|")] = '5';
            m_DigitMap[GetDigitKey(
                " _ ", 
                "|_ ", 
                "|_|")] = '6';
            m_DigitMap[GetDigitKey(
                " _ ", 
                "  |", 
                "  |")] = '7';
            m_DigitMap[GetDigitKey(
                " _ ", 
                "|_|", 
                "|_|")] = '8';
            m_DigitMap[GetDigitKey(
                " _ ", 
                "|_|", 
                " _|")] = '9';
        }
    }
}
