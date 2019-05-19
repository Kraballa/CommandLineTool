using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace CommandlineTool.Commands
{
	public class _8Ball : AbsCommand
	{
		private Random Random;

		private string[] messages;

		public _8Ball()
		{
			Random = new Random();
			Regex = new Regex("8ball");
			Command = "8ball";
			Description = "ask the magic 8ball a yes/no question and he will decide";

			messages = new string[] {"Yes",
                "Yes but in blue",
                "Definitely",
                "I'm sure of it",
                "No",
                "Not necessarily",
                "No... or is it?",
                "Do I look like 12?",
                "Are you gay?",
                "what was the question again?",
                "Uuh sorry I wasn't listening.",
                "idk",
                "ask your mum",
                "/shrug"
		    };
		}

		public override int Execute(string[] parameter)
        {
            if(parameter.Length < 1)
            {
                Console.WriteLine("you didn't even ask a question");
                return ERROR;
            }


			if (!parameter[parameter.Length-1].EndsWith('?'))
			{
				Console.WriteLine("that's not a question");
				return ERROR;
			}

			int index = Random.Next(messages.Length);
			Console.WriteLine(messages[index]);
			return DEFAULT;
		}
	}
}
