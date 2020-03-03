using System.Linq;
using Microsoft.Extensions.CommandLineUtils;

namespace findDupl
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new CommandLineApplication();
            app.Name = "FindDuplicates";
            app.Description = "return all duplicate characters from a given string";
        
            app.HelpOption("-?|-h|--help");
        
            var inputString = app.Option("-i|--input <string>",
                    "The input string",
                    CommandOptionType.SingleValue);
        
            app.OnExecute(() =>
            {
                if (!inputString.HasValue()) {
                    app.ShowHelp();
                    return 1;
                }
                string s = inputString.HasValue() ? inputString.Value() : "abcxbdefagbhx";
                System.Console.WriteLine($"Input = {s}");
                
                var dups = GetDuplicateChars(s);
                
                var output = string.IsNullOrEmpty(dups) ? "no duplicates found" : dups;

                System.Console.WriteLine($"multiple chars = {output}");
                return 0;
            });

            app.Execute(args);
        }

        private static string GetDuplicateChars(string s)
        {   
            var result = s.ToCharArray()
                            .GroupBy(c => c)
                            .Where(g => g.Count() > 1)
                            .SelectMany(r => r)
                            .Distinct()
                            .OrderBy(c=>c);                
            
            return string.Concat(result);            
        }
    }
}
