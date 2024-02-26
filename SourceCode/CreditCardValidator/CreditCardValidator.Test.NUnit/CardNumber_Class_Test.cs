using CreditCardValidator.Business;
using CreditCardValidator.Business.Rule;

namespace CreditCardValidator.Test.NUnit
{
    public class CardNumber_Class_Test
    {
        NotEmpty NotEmpty;
        OnlyNumbers OnlyNumbers;
        LuhnValidate LuhnValidate;
        CardNumber CardNumber;
        CreditCard CreditCard;

        [SetUp]
        public void Setup()
        {
            NotEmpty = new NotEmpty();
            OnlyNumbers = new OnlyNumbers();
            LuhnValidate = new LuhnValidate();
            CardNumber = new CardNumber();

            CreditCard = new CreditCard(NotEmpty, OnlyNumbers, LuhnValidate, CardNumber);
        }

        [Test]
        public void VisaValidCard_True_Success()
        {
            bool IsValid = CreditCard.Validate("4444333322221111");
            Assert.IsTrue(IsValid);
        }

        [Test]
        public void NullCard_False_Success()
        {
            bool IsValid = CreditCard.Validate(null);
            Assert.IsFalse(IsValid);
        }

        [Test]
        public void EmptyCard_False_Success()
        {
            bool IsValid = CreditCard.Validate("");
            Assert.IsFalse(IsValid);
        }

        [Test]
        public void AlphanumericCard_False_Success()
        {
            bool IsValid = CreditCard.Validate("444433332222111a");
            Assert.IsFalse(IsValid);
        }

        [Test]
        public void InvalidLuhnCard_False_Success()
        {
            bool IsValid = CreditCard.Validate("4444333322221121");
            Assert.IsFalse(IsValid);
        }
    }
}