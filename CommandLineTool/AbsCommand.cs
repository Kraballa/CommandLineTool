using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommandlineTool
{
    public abstract class AbsCommand
    {
        public const int UNDEFINED = -2;
        public const int ERROR = -1;
        public const int DEFAULT = 1;

        public bool AllowInLine = false;
        public Regex Regex { get; protected set; }
        public string Description;
        public string Command;

        public abstract int Execute(string[] parameter);

        public virtual string InLineExecute(string[] parameter)
        {
            return "";
        }
    }
}
