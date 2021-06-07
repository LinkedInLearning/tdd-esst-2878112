using System;
using System.Collections.Generic;

namespace FizzBuzzKata
{
    public class FizzBuzzConverter : INumberConverter
    {
        INumberConverter[] converters;
        public FizzBuzzConverter(params INumberConverter[] converters)
        {
            this.converters = converters;
        }

        public string Convert(int value)
        {
            string result = string.Empty;

            foreach (var converter in converters)
            {
                result += converter.Convert(value);
            }

            if (result != string.Empty)
                return result;

            return value.ToString();
        }
    }

    public interface INumberConverter
    {
        string Convert(int value);
    }

    public class FizzConverter : INumberConverter
    {
        public string Convert(int value)
        {
            string result = string.Empty;

            if (value % 3 == 0)
                result = "Fizz";

            return result;
        }
    }

    public class BuzzConverter : INumberConverter
    {
        public string Convert(int value)
        {
            string result = string.Empty;

            if (value % 5 == 0)
                result = "Buzz";

            return result;
        }
    }
}
