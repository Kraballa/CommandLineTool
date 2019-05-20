using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CommandLineTool.Commands
{
    public class Title : AbsCommand
    {
        public Title()
        {
            Regex = new Regex("title");
            Description = "edit the consoles title";
            Command = "title";
        }

        public override int Execute(string[] parameter)
        {
            Console.Title = string.Join(" ",parameter);
            return DEFAULT;
        }
    }
}
