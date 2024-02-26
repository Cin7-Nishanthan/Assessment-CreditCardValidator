using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardValidator.Business.Support
{
    public static class Extensions
    {
        public static bool IsDigitsOnly(this string Number)
        {
            foreach (char Num in Number)
            {
                if (Num < '0' || Num > '9')
                    return false;
            }

            return true;
        }

        public static void ToDigits(this int Number, ref List<int> Digits)
        {
            /*if (Number < 10)
                Digits.Add(Number);
            else
                ToDigits(Number / 10, ref Digits);*/
            char[] Numbers = Number.ToString().ToCharArray();
            foreach (var Num in Numbers)
            {
                Digits.Add(int.Parse(Num.ToString()));
            }
        }
    }
}
