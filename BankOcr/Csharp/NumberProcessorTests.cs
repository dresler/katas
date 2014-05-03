using NUnit.Framework;
using Rhino.Mocks;

namespace BankOcr
{
    [TestFixture]
    public class NumberProcessorTests
    {
        private NumberProcessor m_Processor;
        private INumberValidator m_NumberValidator;
        private ISimilarNumberGenerator m_SimilarNumberGenerator;

        [SetUp]
        public void SetUp()
        {
            m_NumberValidator = MockRepository.GenerateMock<INumberValidator>();
            m_SimilarNumberGenerator = MockRepository.GenerateMock<ISimilarNumberGenerator>();
            m_Processor = new NumberProcessor(m_NumberValidator, m_SimilarNumberGenerator);
        }

        [Test]
        public void Process_ForValidNumber_ShouldReturnThisNumber()
        {
            const string ValidNumber = "457508000";
            m_NumberValidator.Stub(m => m.IsValid(ValidNumber)).Return(true);
            
            Assert.AreEqual(ValidNumber, m_Processor.Process(ValidNumber));
        }

        [Test]
        public void Process_ForInvalidNumberWithOnlyOneValidSimilarNumber_ShouldReturnSimilarNumber()
        {
            const string SimilarValidNumber = "457508000";
            const string InvalidNumber = "123456789";

            m_NumberValidator.Stub(m => m.IsValid(InvalidNumber)).Return(false);
            m_NumberValidator.Stub(m => m.IsValid(SimilarValidNumber)).Return(true);
            m_SimilarNumberGenerator.Stub(m => m.Generate(InvalidNumber)).Return(new[] { SimilarValidNumber });

            Assert.AreEqual(SimilarValidNumber, m_Processor.Process(InvalidNumber));
        }

        [Test]
        public void Process_ForInvalidNumberWithNoValidSimilarNumber_ShouldReturnExpected()
        {
            const string InvalidNumber = "123456789";

            m_NumberValidator.Stub(m => m.IsValid(InvalidNumber)).Return(false);
            m_SimilarNumberGenerator.Stub(m => m.Generate(InvalidNumber)).Return(new string[0]);

            Assert.AreEqual("123456789 ERR", m_Processor.Process(InvalidNumber));
        }

        [Test]
        public void Process_ForInvalidNumberWithWrongDigitAndWithNoValidSimilarNumber_ShouldReturnExpected()
        {
            const string InvalidNumberWithWrongDigit = "12345678?";

            m_NumberValidator.Stub(m => m.IsValid(InvalidNumberWithWrongDigit)).Return(false);
            m_SimilarNumberGenerator.Stub(m => m.Generate(InvalidNumberWithWrongDigit)).Return(new string[0]);

            Assert.AreEqual("12345678? ILL", m_Processor.Process(InvalidNumberWithWrongDigit));
        }

        [Test]
        public void Process_ForInvalidNumberWithMoreValidSimilarNumbers_ShouldReturnExpected()
        {
            const string InvalidNumber = "123456781";
            const string ValidSimilarNumber1 = "111111111";
            const string ValidSimilarNumber2 = "111111112";

            m_NumberValidator.Stub(m => m.IsValid(InvalidNumber)).Return(false);
            m_NumberValidator.Stub(m => m.IsValid(ValidSimilarNumber1)).Return(true);
            m_NumberValidator.Stub(m => m.IsValid(ValidSimilarNumber2)).Return(true);

            m_SimilarNumberGenerator.Stub(m => m.Generate(InvalidNumber)).Return(new[] { ValidSimilarNumber1, ValidSimilarNumber2 });

            Assert.AreEqual("123456781 AMB", m_Processor.Process(InvalidNumber));
        }
    }
}