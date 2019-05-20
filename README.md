# CommandLineTool
Simple commandline tool toolbox in dotnet core. Add your own commands with ease.

### Adding own commands
Create a new class and extend AbsCommand. In the constructor simply set the following public variables:
* `Regex`, describes the pattern that is being checked against when testing for a match
* `Command`, name of your command because it might differ from Regex
* `Description`, description of your command that gets printed with the `help` command
* `AllowInLine`, boolean that represents whether your command should be able to get called inside a line, false by default
* `NumParams`, the number of parameters the command expects

### "Inline Execution"
Imagine you want the result of a command be embedded into a sentence. That's what Inline Execution is.
Example call:
`Hello, my name is Kraballa and I'm over <number 800> years old. I was born in <number 932>`
will get evaluated and prints:
`Hello, my name is Kraballa and I'm over eight hundred years old. I was born in nine hundred and thirty-two`
where `number` is a command that parses numbers into verbal sentences.
