using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLineTool.Commands
{
    public class Beep : AbsCommand
    {
        public Beep()
        {
            Command = "beep";
            Regex = new System.Text.RegularExpressions.Regex("beep");
            Description = "beep in the specified frequency and duration";
            NumParams = 2;
        }

        public override int Execute(string[] parameter)
        {
            int hz = int.Parse(parameter[0]);
            int duration = int.Parse(parameter[1]);
            Console.Beep(hz, duration);
            return DEFAULT;
        }
    }
}
