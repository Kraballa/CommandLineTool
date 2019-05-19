using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace CommandlineTool.Commands
{
    public class Info : AbsCommand
    {
        public Info()
        {
            Regex = new Regex("info");
            Description = "displays basic system and environment information";
        }

        public override int Execute(string[] parameter)
        {
            string[] drives = Directory.GetLogicalDrives();
            for(int i = 0; i < drives.Length; i++)
            {
                DirectoryInfo di = new DirectoryInfo(drives[i]);
                Console.WriteLine(di);
            }
            return DEFAULT;
        }
    }
}
