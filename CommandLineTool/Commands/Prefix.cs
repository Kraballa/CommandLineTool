using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CommandlineTool.Commands
{
    public class Prefix : AbsCommand
    {

        public Prefix()
        {
            Regex = new Regex("prefix");
            Command = "prefix";
            Description = "changes prefix";
            NumParams = 1;
        }

        public override int Execute(string[] parameter)
        {
            if (parameter.Length > 0)
            {
                Program.Prefix = parameter[0];
                Console.WriteLine("new prefix set to " + Program.Prefix);
            }
            else
            {
                Console.WriteLine("current prefix is " + Program.Prefix);
            }
            return DEFAULT;
        }
    }
}
