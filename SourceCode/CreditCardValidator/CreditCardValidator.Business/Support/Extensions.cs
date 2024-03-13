

namespace CreditCardValidator.Business.Support
{
    public static class Extensions
    {
        #region " Methods "

        public static bool IsDigitsOnly(this string number)
        {
            foreach (char num in number)
            {
                if (num < '0' || num > '9')
                    return false;
            }

            return true;
        }

        public static void ToDigits(this int number, ref List<int> digits)
        {
            char[] numbers = number.ToString().ToCharArray();
            foreach (var num in numbers)
            {
                digits.Add(int.Parse(num.ToString()));
            }
        }

        #endregion
    }
}
