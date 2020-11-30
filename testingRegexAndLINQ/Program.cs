using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace testingRegexAndLINQ
{
    class Program
    {
        private static readonly string quotesExpressionPattern = "(\\s*\"[\\s\\S]+?\"\\s*)";
        private static readonly string escapeExpressionsPattern = @"((\w+\\\s+)+\w+)";
        private static readonly string spaceSplitterPattern = @"(\s+)";
        private static Regex regExp;

        private static string[] UnpackArguments(string v)
        {
            string[] arr = regExp.Split(v);

            //Print(arr);

            var reg = new Regex(@"^(\w+\\)*\s+$"); // Костыль связанный с escapeExpressionsPattern

            // ДА LINQ. ДА гранатаметом по мухам. НУ А ЧО
            var scoreQuery = from arg in arr
                             where !reg.IsMatch(arg) && arg!=""
                             select arg.Trim(new char[]{' ','"'});

            return scoreQuery.ToArray();
        }
        public static void Print(string[] a)
        {
            foreach (var x in a)
                Console.WriteLine(x);
            Console.WriteLine(new string('=',90));
        }
        
        public static void Main(string[] args)
        {
            Initialize();

            while (true)
            {
                WritePrompt("C:\\Users\\You> ");
                Print(UnpackArguments(Console.ReadLine()));
            }
        }
        private static void Initialize()
        {
            string pattern = $"{quotesExpressionPattern}|{escapeExpressionsPattern}|{spaceSplitterPattern}";
            regExp = new Regex(pattern, RegexOptions.Compiled);
        }

        private static void WritePrompt(string v) => Console.Write(v);
    }
}
