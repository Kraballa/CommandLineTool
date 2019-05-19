using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommandlineTool.Commands
{
    public class Clear : AbsCommand
    {
        public Clear()
        {
            Regex = new Regex(@"clear|cls|reset");
            Command = "clear";
            Description = "clears the console";
        }

        public override int Execute(string[] parameter)
        {
            Console.Clear();
            return DEFAULT;
        }
    }
}
