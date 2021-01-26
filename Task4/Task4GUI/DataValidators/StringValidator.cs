using System.Globalization;
using System.Windows.Controls;

namespace Task4GUI.DataValidators
{
    public class StringValidator : ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string toCheck = (string) value;
            if (toCheck != null && toCheck.Length >= Min && toCheck.Length <= Max)
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                return new ValidationResult(false,"Bad string length");
            }
        }
    }
}