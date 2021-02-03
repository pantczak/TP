using System;
using System.Globalization;
using System.Windows.Controls;

namespace Task4GUI.DataValidators
{
    public class DecimalValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string toCheck = (string) value;
            decimal d;
            try
            {
                d = Decimal.Parse(toCheck ?? throw new InvalidOperationException(), cultureInfo);
            }
            catch (Exception)
            {
               
                return new ValidationResult(false,"Incorrect Decimal Value");
            }

            return d >= 0.0m ? ValidationResult.ValidResult : new ValidationResult(false, " Decimal Value must be positive");
        }
    }
}