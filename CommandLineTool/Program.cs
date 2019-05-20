using CommandLineTool.Commands;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CommandLineTool
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
            string[] split = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string[] parameters = new string[split.Length-1];
            
            for(int i = 1; i < split.Length; i++)
            {
                parameters[i - 1] = split[i];
            }

            foreach(AbsCommand command in commandList)
            {
                if (command.Regex.Match(split[0]).Success)
                {
                    if(command.NumParams == parameters.Length)
                    {
                        try
                        {
                            returnCode = command.Execute(parameters);
                        }
                        catch (Exception e)
                        {
                            returnCode = AbsCommand.ERROR;
                            Console.WriteLine(e.StackTrace);
                        }
                    }
                    else
                    {
                        returnCode = AbsCommand.UNDEFINED;
                        Console.WriteLine("error, required " + command.NumParams + " parameters but was given " + parameters.Length);
                    }
                }
            }

            switch (returnCode)
            {
                case AbsCommand.ERROR:
                    Console.WriteLine("execution failed");
                    break;

                case AbsCommand.UNDEFINED:
                    Console.WriteLine("unrecognised command, type " + Prefix + "help for a list of commands");
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
                    Regex inlineRegex = new Regex("<" + command.Regex + "[ a-zA-Z0-9]*>");
                    Match match = inlineRegex.Match(input);
                    //match every inline command
                    while (match.Success)
                    {
                        matched = true;
                        //extract parameter from call
                        string[] splitCall = match.Value.Replace('<', ' ').Replace('>', ' ').Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                        string[] parameters = new string[splitCall.Length - 1];
                        for (int i = 1; i < splitCall.Length; i++)
                        {
                            parameters[i - 1] = splitCall[i];
                        }

                        string[] splitInputString = input.Split(match.Value, 2);
                        //if parameters don't comfort to the specified amount, remove command call
                        if (parameters.Length == command.NumParams)
                        {
                            string ret = command.InLineExecute(parameters);
                            input = splitInputString[0] + ret + splitInputString[1];
                        }
                        else
                        {
                            input = splitInputString[0] + splitInputString[1];
                        }

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
