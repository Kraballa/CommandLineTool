using CommandlineTool;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CommandLineTool.Commands
{
    public class OpenUrl : AbsCommand
    {
        public OpenUrl()
        {
            Regex = new System.Text.RegularExpressions.Regex("open");
            Command = "open";
            Description = "evalues an url and opens default programs";
            NumParams = 1;
        }

        public override int Execute(string[] parameter)
        {
            Process.Start(parameter[0]);
            return DEFAULT;
        }
    }
}
