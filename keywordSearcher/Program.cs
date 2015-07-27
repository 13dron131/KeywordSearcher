using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace keywordSearcher
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string path = "";
            Console.WriteLine("Ввежите путь к файлу");
            path = @Console.ReadLine();
            KeywordSearcher keywordSearcher = new KeywordSearcher(path, 5);
            keywordSearcher.OutputAllKeywords();
            keywordSearcher.OutputTopKeywords();
            Console.ReadLine();
        }
    }
}
