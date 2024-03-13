

namespace CreditCardValidator.Business.Support
{
    public class Luhn
    {
        #region " Vars "

        List<LuhnData> _luneDataList = new List<LuhnData>();
        string _cardNumber;

        #endregion


        #region " Constructor "
        
        public Luhn(string cardNumber)
        {
            this._cardNumber = cardNumber;
            ToLuneData();
            Process();
        }

        #endregion


        #region " Methods "

        void ToLuneData()
        {
            char[] digits = _cardNumber.ToCharArray();
            for (int index = digits.Length - 2; index >= 0; index--)
            {
                if (index % 2 == 1)
                    _luneDataList.Add(new LuhnData() { Digit = int.Parse(digits[index].ToString()), Weight = 2 });
                else
                    _luneDataList.Add(new LuhnData() { Digit = int.Parse(digits[index].ToString()), Weight = 1 });
            }
        }

        void Process()
        {
            int product;
            List<int> productDigits;
            foreach (var luneData in _luneDataList)
            {
                product = luneData.Digit * luneData.Weight;
                if (product < 10)
                    luneData.Product = product;
                else
                {
                    productDigits = new List<int>();
                    product.ToDigits(ref productDigits);
                    product = productDigits.Sum();
                    luneData.Product = product;
                }
            }
        }

        public bool IsValid()
        {
            int numberOfDigits = _cardNumber.Length;

            int sum = 0;
            bool isSecond = false;
            int d;
            for (int i = numberOfDigits - 1; i >= 0; i--)
            {

                d = _cardNumber[i] - '0';

                if (isSecond == true)
                    d = d * 2;

                // We add two digits to handle
                // cases that make two digits 
                // after doubling
                sum += d / 10;
                sum += d % 10;

                isSecond = !isSecond;
            }
            return (sum % 10 == 0);
        }

        #endregion

    }

}
