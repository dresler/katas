using System;
using System.Linq;

using NUnit.Framework;
using Rhino.Mocks;

namespace Args
{
    [TestFixture]
    public class ParserTests
    {
        [TestCase(new char[0], new Type[0], "", true, TestName = "[bool f] [] => true")]
        [TestCase(new[] {'f'}, new[] {typeof(bool)}, "", true, TestName = "[bool f] [] => true")]
        [TestCase(new[] {'f'}, new[] {typeof(bool)}, "-f", true, TestName = "[bool f] [-f] => true")]
        [TestCase(new[] { 'f' }, new[] { typeof(bool) }, "-f 1", false, TestName = "[bool f] [-f 1] => false")]
        [TestCase(new[] { 'f' }, new[] { typeof(bool) }, "-f true", true, TestName = "[bool f] [-f true] => true")]
        [TestCase(new[] { 'f' }, new[] { typeof(bool) }, "-p", false, TestName = "[bool f] [-p] => false")]
        [TestCase(new[] { 'f' }, new[] { typeof(int) }, "-f true", false, TestName = "[int f] [-f true] => false")]
        [TestCase(new[] { 'f' }, new[] { typeof(int) }, "-f 1", true, TestName = "[int f] [-f 1] => true")]
        [TestCase(new[] { 'f' }, new[] { typeof(int) }, "-f 'A'", false, TestName = "[int f] [-f 'A'] => false")]
        [TestCase(new[] { 'f' }, new[] { typeof(int) }, "-f -1", true, TestName = "[int f] [-f -1] => true")]
        [TestCase(new[] { 'f' }, new[] { typeof(string) }, "-f 0", true, TestName = "[string f] [-f 0] => true")]
        [TestCase(new[] { 'f' }, new[] { typeof(string) }, "-f hello", true, TestName = "[string f] [-f hello] => true")]
        [TestCase(new[] { 'f' }, new[] { typeof(string) }, "-f -23", true, TestName = "[string f] [-f -23] => true")]
        [TestCase(new[] { 'f', 'g', 'h' }, new[] { typeof(bool), typeof(int), typeof(string) }, "-f true -g 11 -h hello", true, TestName = "[bool f, int g, string h] [-f true -g 11 -h hello] => true")]
        [TestCase(new[] { 'f', 'g', 'h' }, new[] { typeof(bool), typeof(int), typeof(string) }, "-g -h -f", true, TestName = "[bool f, int g, string h] [-g -h -f] => true")]
        [TestCase(new[] { 'f', 'g', 'h' }, new[] { typeof(bool), typeof(int), typeof(string) }, "-g -h -l", false, TestName = "[bool f, int g, string h] [-g -h -l] => false")]
        public void Verify_ForGivenSchemaAndArguments_ShouldReturnExpected(char[] flags, Type[] flagTypes, string arguments, bool expected)
        {
            var schema = CreateSchema(flags, flagTypes);
            var parser = new Parser(schema);
            Assert.AreEqual(expected, parser.Verify(arguments));
        }

        [TestCase(new[] { 'f' }, new[] { typeof(bool) }, "-f", 'f', false, TestName = "[bool f] [-f] [f] => false")]
        [TestCase(new[] { 'f' }, new[] { typeof(bool) }, "-f true", 'f', true, TestName = "[bool f] [-f true] [f] => true")]
        [TestCase(new[] { 'f' }, new[] { typeof(bool) }, "-f false", 'f', false, TestName = "[bool f] [-f false] [f] => false")]
        [TestCase(new[] { 'f' }, new[] { typeof(int) }, "-f", 'f', 0, TestName = "[int f] [-f] [f] => 0")]
        [TestCase(new[] { 'f' }, new[] { typeof(int) }, "-f 22", 'f', 22, TestName = "[int f] [-f 22] [f] => 22")]
        [TestCase(new[] { 'f' }, new[] { typeof(int) }, "-f -2", 'f', -2, TestName = "[int f] [-f -2] [f] => -2")]
        [TestCase(new[] { 'f' }, new[] { typeof(string) }, "-f", 'f', "", TestName = "[int f] [-f] [f] => \"\"")]
        [TestCase(new[] { 'f' }, new[] { typeof(string) }, "-f hello", 'f', "hello", TestName = "[int f] [-f hello] [f] => \"hello\"")]
        [TestCase(new[] { 'f', 'g', 'h' }, new[] { typeof(bool), typeof(int), typeof(string) }, "-g 22", 'g', 22, TestName = "[bool f, int g, string h] [-g 22] [g] => 22")]
        public void GetValue_ForGivenSchemaAndArguments_ShouldReturnExpected(char[] flags, Type[] flagTypes, string arguments, char flag, object expected)
        {
            var schema = CreateSchema(flags, flagTypes);
            var parser = new Parser(schema);
            Assert.AreEqual(expected, parser.GetValue(arguments, flag));
        }

        private static ISchema CreateSchema(char[] flags, Type[] flagTypes)
        {
            var schema = MockRepository.GenerateMock<ISchema>();
            flags.Select((flag, index) => schema.Stub(m => m.GetTypeOfValue(flag)).Return(flagTypes[index])).ToArray();
            flags.Select(flag => schema.Stub(m => m.ContainsFlag(flag)).Return(true)).ToArray();
            return schema;
        }
    }
}