using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommandlineTool.Commands
{
    public class Commit : AbsCommand
    {
        public Commit()
        {
            Regex = new Regex(@"commit|motd");
            Command = "commit";
            Description = "pulls a random commit message";
        }

        public override int Execute(string[] parameter)
        {

            string message = Fetch().GetAwaiter().GetResult();
            Console.WriteLine(message);
            return DEFAULT;
        }

        private async Task<string> Fetch()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("http://whatthecommit.com/index.txt");
            var content = await response.Content.ReadAsStringAsync();
            client.Dispose();
            return content;
        }
    }
}
