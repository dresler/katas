using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Args
{
    public class Parser
    {
        private readonly ISchema m_Schema;
        private readonly IDictionary<Type, Func<string, object>> m_TypeParsers = new Dictionary<Type, Func<string, object>>();
        private readonly IDictionary<Type, Predicate<string>> m_TypeValidators = new Dictionary<Type, Predicate<string>>();

        public Parser(ISchema schema)
        {
            m_Schema = schema;

            CreateTypeValidators();
            CreateTypeParsers();
        }

        public string GetArgumentValue(string argument)
        {
            return argument.Length <= 2 ? string.Empty : argument.Substring(3);
        }

        public object GetValue(string arguments, char flag)
        {
            var argumentList = GetArguments(arguments);
            var argument = GetArgument(argumentList, flag);
            var argumentValue = GetArgumentValue(argument);
            return ConvertToObject(argumentValue, flag);
        }

        public bool Verify(string arguments)
        {
            var argumentList = GetArguments(arguments);
            return argumentList.All(IsValidArgument);
        }

        private object ConvertToObject(string argumentValue, char flag)
        {
            var typeOfValue = m_Schema.GetTypeOfValue(flag);
            if (!m_TypeParsers.ContainsKey(typeOfValue)) throw new InvalidOperationException(string.Format("Not supported type {0}", typeOfValue));
            return m_TypeParsers[typeOfValue].Invoke(argumentValue);
        }

        private void CreateTypeParsers()
        {
            m_TypeParsers[typeof(bool)] = s => !string.IsNullOrWhiteSpace(s) && Convert.ToBoolean(s);
            m_TypeParsers[typeof(int)] = s => string.IsNullOrWhiteSpace(s) ? 0 : Convert.ToInt32(s);
            m_TypeParsers[typeof(string)] = s => s;
        }

        private void CreateTypeValidators()
        {
            m_TypeValidators[typeof(bool)] = s => { bool tmp; return bool.TryParse(s, out tmp); };
            m_TypeValidators[typeof(int)] = s => { int tmp; return int.TryParse(s, out tmp); };
            m_TypeValidators[typeof(string)] = s => true;
        }

        private string GetArgument(IEnumerable<string> argumentList, char flag)
        {
            return argumentList.FirstOrDefault(arg => arg.StartsWith(string.Format("-{0}", flag)));
        }

        private IEnumerable<string> GetArguments(string argumentList)
        {
            const string ArgumentsPattern = @"(?=-[a-z])";
            return Regex.Split(argumentList, ArgumentsPattern).Where(arg => !string.IsNullOrWhiteSpace(arg)).ToArray();
        }

        private char GetFlag(string argument)
        {
            return argument[1];
        }

        private bool IsValidArgument(string argument)
        {
            var flag = GetFlag(argument);
            if (!m_Schema.ContainsFlag(flag)) return false;
            var value = GetArgumentValue(argument);
            return ValidateValue(flag, value);
        }

        private bool ValidateValue(char flag, string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return true;

            var typeOfValue = m_Schema.GetTypeOfValue(flag);
            if (!m_TypeValidators.ContainsKey(typeOfValue)) throw new InvalidOperationException(string.Format("Not supported type {0}", typeOfValue));
            return m_TypeValidators[typeOfValue].Invoke(value);
        }
    }
}