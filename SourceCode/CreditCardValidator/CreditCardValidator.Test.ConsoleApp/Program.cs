// See https://aka.ms/new-console-template for more information
using CreditCardValidator.Business;
using CreditCardValidator.Business.Rule;
using CreditCardValidator.Business.Support;


Console.WriteLine("Not Empty");
NotEmpty NotEmpty = new NotEmpty();
Console.WriteLine(NotEmpty.IsValid("12212"));
Console.WriteLine(NotEmpty.IsValid(null));
Console.WriteLine(NotEmpty.IsValid(""));

Console.WriteLine("");
Console.WriteLine("Only Numbers");
OnlyNumbers OnlyNumbers = new OnlyNumbers();
Console.WriteLine(OnlyNumbers.IsValid("121212"));
Console.WriteLine(OnlyNumbers.IsValid("121212ddd"));


CardNumber CardNumber = new CardNumber();

Console.WriteLine("");
Console.WriteLine("Luhn");
LuhnValidate LuhnValidate = new LuhnValidate();
Console.WriteLine(LuhnValidate.IsValid("4444333322221111"));
Console.WriteLine(LuhnValidate.IsValid("5105105105105100"));
Console.WriteLine(LuhnValidate.IsValid("2223000048400011"));
Console.WriteLine(LuhnValidate.IsValid("2223520043560014"));
Console.WriteLine(LuhnValidate.IsValid("378282246310005"));
Console.WriteLine(LuhnValidate.IsValid("6011000400000000"));
Console.WriteLine(LuhnValidate.IsValid("4333453412345634"));
Console.WriteLine(LuhnValidate.IsValid("4487984578353700"));


Console.WriteLine("Validate Card");
CreditCard CreditCard = new CreditCard(NotEmpty, OnlyNumbers, LuhnValidate, CardNumber);
Console.WriteLine("4444333322221111: "+CreditCard.Validate("4444333322221111"));
Console.WriteLine("444433332222111: " + CreditCard.Validate("444433332222111"));
Console.WriteLine("null: " + CreditCard.Validate(null));
Console.WriteLine("Empty: " + CreditCard.Validate(""));

Console.ReadLine();