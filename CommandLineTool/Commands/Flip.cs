using System;
using System.Text.RegularExpressions;

namespace CommandLineTool.Commands
{
    public class Flip : AbsCommand
    {
        private Random random;
        public Flip()
        {
            random = new Random();
            Regex = new Regex("flip");
            AllowInLine = true;
            Command = "flip";
            Description = "flips a coin";
        }

        public override string InLineExecute(string[] parameter)
        {
            if (random.NextDouble() >= 0.5)
            {
                return "heads";
            }
            else
            {
                return "tails";
            }
        }

        public override int Execute(string[] parameter)
        {
            Console.WriteLine(InLineExecute(parameter));
            return DEFAULT;
        }
    }
}
