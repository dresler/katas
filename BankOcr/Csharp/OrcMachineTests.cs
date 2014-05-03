using NUnit.Framework;
using Rhino.Mocks;

namespace BankOcr
{
    [TestFixture]
    public class OrcMachineTests
    {
        private IEntryParser m_EntryParser;
        private INumberProcessor m_NumberProcessor;
        private OcrMachine m_OcrMachine;
        private const string VALID_NUMBER = "123456789";
        private const string INVALID_NUMBER = "123456781";
        private const string INVALID_NUMBER_OUTPUT = "123456781" + " ERR";
        private const string ENTITY_WITH_VALID_NUMBER = "___\r\n___\r\n___";
        private const string ENTITY_WITH_INVALID_NUMBER = "|__\r\n|__\r\n___";

        [SetUp]
        public void SetUp()
        {
            m_EntryParser = MockRepository.GenerateMock<IEntryParser>();
            m_NumberProcessor = MockRepository.GenerateMock<INumberProcessor>();

            m_EntryParser.Stub(m => m.Parse(ENTITY_WITH_VALID_NUMBER)).Return(VALID_NUMBER);
            m_EntryParser.Stub(m => m.Parse(ENTITY_WITH_INVALID_NUMBER)).Return(INVALID_NUMBER);

            m_NumberProcessor.Stub(m => m.Process(VALID_NUMBER)).Return(VALID_NUMBER);
            m_NumberProcessor.Stub(m => m.Process(INVALID_NUMBER)).Return(INVALID_NUMBER_OUTPUT);

            m_OcrMachine = new OcrMachine(m_EntryParser, m_NumberProcessor);
        }

        [Test]
        public void Process_ForSingleEntryContent_ShouldReturnExpected()
        {
            CollectionAssert.AreEqual(new[] { INVALID_NUMBER_OUTPUT}, m_OcrMachine.Process(ENTITY_WITH_INVALID_NUMBER));
        }        

        [Test]
        public void Process_ForMultiEntryContent_ShouldReturnExpected()
        {
            var content = string.Format("{0}\r\n   \r\n{1}", ENTITY_WITH_VALID_NUMBER, ENTITY_WITH_INVALID_NUMBER);
            CollectionAssert.AreEqual(new[] { VALID_NUMBER, INVALID_NUMBER_OUTPUT}, m_OcrMachine.Process(content));
        }        
    }
}