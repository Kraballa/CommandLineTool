using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CommandlineTool.Commands
{
    public class VerbalNumber : AbsCommand
    {

        private string[] ones = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        private string[] teens = { "ten", "eleven", "twelve" };
        private string[] teenSuffixes = { "thir", "four", "fif", "six", "seven", "eigh", "nine" };
        private string[] tens = { "twenty", "thirty", "fourty", "fifty", "sixty", "seventy", "eighty", "ninety" };
        private string[] multipleOfTens = { "hundred", "thousand", "million", "billion", "trillion", "quadrillion" };

        public VerbalNumber() : base()
        {
            Regex = new Regex("number");
            AllowInLine = true;
            Command = "number";
            Description = "prints out the number verbally";
            NumParams = 1;
        }

        public override int Execute(string[] parameter)
        {
            Console.WriteLine(LongToString(long.Parse(parameter[0])));
            return DEFAULT;
        }

        public override string InLineExecute(string[] parameter)
        {
            try
            {
                return LongToString(long.Parse(parameter[0]));
            }
            catch (FormatException e)
            {
                return parameter[0] + "\n" + e.StackTrace;
            }
        }

        private string LongToString(long i)
        {
            if (i < 0) { return "i smaller than 0"; }

            if (i < 10)
            {
                return ones[i];
            }
            else if (i < 13)
            {
                return teens[i - 10];
            }
            else if (i < 20)
            {
                return teenSuffixes[i - 13] + "teen";
            }
            else if (i < 100)
            {
                if (i % 10 != 0)
                {
                    return tens[i / 10 - 2] + "-" + LongToString(i % 10);
                }
                return tens[i / 10 - 2];
            }
            else if (i < 1000)
            {
                if (i % 100 != 0)
                {
                    return LongToString(i / 100) + " hundred and " + LongToString(i % 100);
                }
                return LongToString(i / 100) + " hundred";
            }
            else
            {
                long places = i.ToString().Length;
                long dec = (long)Math.Round(Math.Pow(10, places - 1));
                long lastFullDecimal = (long)Math.Round(Math.Pow(10, (places - 1) - ((places - 1) % 3)));
                if (i % (dec / 10) != 0)
                {
                    return LongToString(i / lastFullDecimal) + " " + multipleOfTens[(places - 1) / 3] + " " + LongToString(i % lastFullDecimal);
                }
                return LongToString(i / lastFullDecimal) + " " + multipleOfTens[(places - 1) / 3];
            }
        }
    }
}
