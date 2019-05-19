using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommandlineTool.Commands
{
    public class Help : AbsCommand
    {
        public Help()
        {
            Regex = new Regex("help");
            Command = "help";
            Description = "prints a list of all commands";
        }

        public override int Execute(string[] parameter)
        {
            Console.WriteLine("inline execution means you can embed commands into plain text by surrounding the call with <>");

            int length = 0;
            foreach(AbsCommand c in Program.commandList)
            {
                string current = Program.Prefix + c.Regex;
                if (length < current.Length)
                    length = current.Length;
            }
            length += 3;
            foreach (AbsCommand command in Program.commandList)
            {
                string current = Program.Prefix + command.Regex;
                string add = "";
                for(int i = 0; i < length-current.Length; i++)
                {
                    add += " ";
                }
                current += add;
                if (command.AllowInLine)
                {
                    Console.WriteLine(current + command.Description + " (allows inline execution)");
                }
                else
                {
                    Console.WriteLine(current + command.Description);
                }
                
            }

            return DEFAULT;
        }
    }
}
