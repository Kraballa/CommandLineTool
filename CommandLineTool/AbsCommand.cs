using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommandLineTool
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
        public int NumParams { get; protected set; }

        /// <summary>
        /// Details what happens when the command is executed.
        /// </summary>
        /// <param name="parameter">Parameter list if it needs any. Set <code>NumParams</code> before using!</param>
        /// <returns>return code constants defined in <code>AbsCommand</code></returns>
        public abstract int Execute(string[] parameter);

        /// <summary>
        /// Similar to <code>Execute</code> however implementation may differ. Only use if applicable.
        /// </summary>
        /// <param name="parameter">Parameter list if it needs any. Set <code>NumParams</code> before using!</param>
        /// <returns>Returns the string that represents the result of the execution. This will replace the command call 
        /// in the final output string</returns>
        public virtual string InLineExecute(string[] parameter)
        {
            throw new InvalidOperationException("error, inline execution not supported");
        }
    }
}
