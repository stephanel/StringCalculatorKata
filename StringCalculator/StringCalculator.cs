using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace StringCalculator
{
    public class StringCalculator
    {
        public const string NumberSeparator = ",";
        public const int Thousand = 1000;
        public const int Zero = 0;

        private string CleaningNumbersStringValuePattern = @"[/;\n\[\]%a-zA-Z]|[/]{2}|[*]+";
        private string NegativeNumbersErrorMessage = "error: negatives not allowed: ";

        public int Add(string stringContainingNumbersToAdd)
        {
            if(string.IsNullOrWhiteSpace(stringContainingNumbersToAdd))
            {
                return Zero;
            }

            stringContainingNumbersToAdd = CleanStringValue(stringContainingNumbersToAdd);

            var numbersAsStringArray = SplitStringValue(stringContainingNumbersToAdd);

            ThrowExceptionIfNegativeNumbersFound(numbersAsStringArray);           

            numbersAsStringArray = ExcludeValuesBiggerThan1000(numbersAsStringArray);

            return SumNumbersAsString(numbersAsStringArray);
        }

        int SumNumbersAsString(string[] numbersAsStringArray)
        {
            int total = 0;

            foreach (var value in numbersAsStringArray)
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    total += int.Parse(value);
                }
            }

            return total;
        }

        void ThrowExceptionIfNegativeNumbersFound(string[] values)
        {
            var negativeValues = GetNegativeValuesAsString(values);
            if (negativeValues.Count() > 0)
            {
                ThrowExceptionAboutNegativeValues(negativeValues);
            }
        }

        string CleanStringValue(string value)
        {
            var regex = new Regex(CleaningNumbersStringValuePattern);
            return regex.Replace(value, NumberSeparator);
        }

        string[] SplitStringValue(string value)
        {
            return value
                .Split(NumberSeparator)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();
        }

        string[] GetNegativeValuesAsString(string[] values)
        {
            return values
                .Where(x => int.Parse(x) < 0)
                .ToArray();
        }

        void ThrowExceptionAboutNegativeValues(string[] negativeValues)
        {
            var invalideValues = string.Join(' ', negativeValues);
            var errorMessage = $"{NegativeNumbersErrorMessage}{invalideValues}";
            throw new Exception(errorMessage);
        }

        string[] ExcludeValuesBiggerThan1000(string[] values)
        {
            return values
                .Where(x => int.Parse(x) < Thousand)
                .ToArray();
        }
    }
}
