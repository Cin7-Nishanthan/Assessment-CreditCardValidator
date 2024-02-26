using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardValidator.Business.Support
{
    public class Luhn
    {
        List<LuhnData> LuneDataList = new List<LuhnData>();
        string CardNumber;

        public Luhn(string CardNumber)
        {
            this.CardNumber = CardNumber;
            ToLuneData();
            Process();
        }

        void ToLuneData()
        {
            char[] Digits = CardNumber.ToCharArray();
            for (int Index = Digits.Length - 2; Index >= 0; Index--)
            {
                if (Index % 2 == 1)
                    LuneDataList.Add(new LuhnData() { Digit = int.Parse(Digits[Index].ToString()), Weight = 2 });
                else
                    LuneDataList.Add(new LuhnData() { Digit = int.Parse(Digits[Index].ToString()), Weight = 1 });
            }
        }

        void Process()
        {
            int Product;
            List<int> ProductDigits;
            foreach (var LuneData in LuneDataList)
            {
                Product = LuneData.Digit * LuneData.Weight;
                if (Product < 10)
                    LuneData.Product = Product;
                else
                {
                    ProductDigits = new List<int>();
                    Product.ToDigits(ref ProductDigits);
                    Product = ProductDigits.Sum();
                    LuneData.Product = Product;
                }
            }
        }

        public bool IsValid()
        {
            int NumberOfDigits = CardNumber.Length;

            int Sum = 0;
            bool IsSecond = false;
            for (int i = NumberOfDigits - 1; i >= 0; i--)
            {

                int D = CardNumber[i] - '0';

                if (IsSecond == true)
                    D = D * 2;

                // We add two digits to handle
                // cases that make two digits 
                // after doubling
                Sum += D / 10;
                Sum += D % 10;

                IsSecond = !IsSecond;
            }
            return (Sum % 10 == 0);
            /*if (LuneDataList.Sum(L => L.Product) % 10 == 0)
                return true;
            else
                return false;*/


        }

    }

}
