using System.Text.RegularExpressions;

namespace CommandlineTool.Commands
{
    public class Exit : AbsCommand
    {
        public Exit()
        {
            Regex = new Regex("exit");
            Command = "exit";
            Description = "exits the program";
            AllowInLine = true;
        }

        public override string InLineExecute(string[] parameter)
        {
            Execute(parameter);
            return "exit";
        }

        public override int Execute(string[] parameter)
        {
            Program.running = false;
            return DEFAULT;
        }
    }
}
