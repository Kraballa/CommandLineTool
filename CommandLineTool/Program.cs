using CommandlineTool.Commands;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CommandlineTool
{
    /// <summary>
    /// Runs the console and parses inputs.
    /// </summary>
    public class Program
    {
        public static string Prefix = "/";
        public static string WelcomeMessage = "type '" + Prefix + "help' for a list of commands";
        public static bool running = true;
        public static List<AbsCommand> commandList = new List<AbsCommand>
            {
                new Clear(),
                new Help(),
                new Commit(),
                new Flip(),
                new Exit(),
                new Prefix(),
                new Info(),
                new VerbalNumber(),
                new Title(),
                new _8Ball()
            };

        static void Main(string[] args)
        {
            Console.WriteLine(WelcomeMessage);
            while (running)
            {
                string input = Console.ReadLine();
                if (input.StartsWith(Prefix))
                {
                    MatchPatterns(input.Substring(1));
                }
                else
                {
                    MatchInLinePattern(input);
                } 
            }
        }

        /// <summary>
        /// Matches an input that starts with a specified prefix.
        /// </summary>
        /// <param name="input">command name and parameters</param>
        public static void MatchPatterns(string input)
        {
            int returnCode = AbsCommand.UNDEFINED;
            string[] split = input.Split(" ");
            string[] parameters = new string[split.Length-1];
            
            for(int i = 1; i < split.Length; i++)
            {
                parameters[i - 1] = split[i];
            }

            foreach(AbsCommand command in commandList)
            {
                if (command.CheckPattern(split[0]))
                {
                    try
                    {
                        returnCode = command.Execute(parameters);
                    }
                    catch(Exception e)
                    {
                        returnCode = AbsCommand.ERROR;
                        Console.WriteLine(e.StackTrace);
                    }
                    
                }
            }

            switch (returnCode)
            {
                case AbsCommand.ERROR:
                    Console.WriteLine("error during execution");
                    break;

                case AbsCommand.UNDEFINED:
                    Console.WriteLine("unrecognised command, type /help for a list of commands");
                    break;
            }
        }

        /// <summary>
        /// Matches an input surrounded by diamond brackets within lines this allows the embedding of command results 
        /// within sentences.
        /// </summary>
        /// <param name="input">the command and all its parameters</param>
        public static void MatchInLinePattern(string input)
        {
            bool matched = false;
            foreach (AbsCommand command in commandList)
            {
                if (command.AllowInLine)
                {
                    Regex inlineRegex = new Regex("<" + command.Regex + " [ a-zA-Z0-9]*>");
                    Match match = inlineRegex.Match(input);
                    //match every inline command
                    while (match.Success)
                    {
                        matched = true;
                        //extract parameter from call
                        string parameter = match.Value.Replace('<', ' ').Replace('>', ' ');
                        parameter = parameter.Replace(command.Command," ").Trim();
                        string ret = command.InLineExecute(new string[] { parameter });
                        string[] split = input.Split(match.Value, 2);
                        input = split[0] + ret + split[1];

                        match = inlineRegex.Match(input);
                    }
                }
            }
            if (matched)
            {
                Console.WriteLine(input);
            }
        }
    }
}
