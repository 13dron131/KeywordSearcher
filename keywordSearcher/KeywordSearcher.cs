using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keywordSearcher
{
    class KeywordSearcher
    {
        private string[] keywords = {"abstract ", "as ", "base ", "bool ", "break ", "byte ", "case ", "catch ", "char ", "checked ", "class ", "const ", "continue ",
                                     "decimal ", "default ", "delegate ", "do ", "double ", "else ", "enum ", "event ", "explicit ", "extern ", "false ", "finally ",
                                     "fixed ", "float ", "for ", "foreach ", "goto ", "if ", "implict ", "in ", "int ", "interface ", "internal ", "is ", "lock ",
                                     "long ", "namespace ", "new ", "null ", "object ", "operator ", "out ", "override ", "params ", "private ", "protected ", "public ",
                                     "readonly ", "ref ", "return ", "sbyte ", "sealed ", "short ", "sizeof ", "stackalloc ", "static ", "string ", "struct ", "switch ",
                                     "this ", "throw ", "true ", "try ", "typeof ", "uint ", "ulong ", "unchecked ", "unsafe ", "ushort ", "using ", "virtual ", "void ",
                                     "volatile ", "while ", "add ", "alias ", "ascending ", "async ", "await ", "descending ", "dynamic ", "from ", "get ", "global ",
                                     "group ", "into ", "join ", "let ", "orderby ", "partial ", "remove ", "select ", "set ", "value ", "var ", "where ", "yield "};
        private string[] lines;
        private Hashtable keywordsWithCount = new Hashtable();
        private int topCount;
        private FileReader fileReader;

        public KeywordSearcher(string path, int topCount)
        {
            fileReader = new FileReader(path);
            this.topCount = topCount;
            lines = fileReader.GetLines();
            DeleteComment(ref lines);
            SetKeywords();
        }

        private void DeleteComment(ref string[] lines)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].IndexOf("//") != -1) 
                {
                    lines[i] = lines[i].Remove(lines[i].IndexOf("//"));
                }
                if (lines[i].IndexOf("///") != -1)
                {
                    lines[i] = lines[i].Remove(lines[i].IndexOf("///"));
                }
            }
        }

        private int CountWordsInLine(string s, string s0)
        {
            int count = (s.Length - s.Replace(s0, "").Length) / s0.Length;
            return count;
        }

        private int CalculateOccurencess(string keyword)
        {
            int occurencess = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                    occurencess += CountWordsInLine(lines[i], keyword);
            }
            return occurencess;
        }

        private void SetKeywords()
        {
            for (int i = 0; i < keywords.Length; i++)
            {
                keywordsWithCount.Add(keywords[i], CalculateOccurencess(keywords[i]));
            }
        }

        private void SortTwoArrays(ref int[] array1, ref string[] array2, int size)
        {
            for (int i = 0; i < array1.Length; i++)
            {
                for (int j = i + 1; j < array1.Length; j++)
                {
                    if (array1[i] < array1[j])
                    {
                        int temp = array1[i];
                        array1[i] = array1[j];
                        array1[j] = temp;

                        string key = array2[i];
                        array2[i] = array2[j];
                        array2[j] = key;
                    }
                }
            }
        }

        public void OutputAllKeywords()
        {
            IDictionaryEnumerator enumerator = keywordsWithCount.GetEnumerator();
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("{0,-20:0}{1}", "ключевое слово", "| повторений");
            Console.WriteLine("----------------------------------------------------");
            while (enumerator.MoveNext())
            {
                Console.WriteLine("{0,-20:0}{1}", enumerator.Key, "| "+enumerator.Value);
            }
        }

        public void OutputTopKeywords()
        {
            int[] topKeywordsCounts = new int[keywords.Length];
            string[] topKeywords = new string[keywords.Length];
            IDictionaryEnumerator enumerator = keywordsWithCount.GetEnumerator();
            for (int i = 0; i < topKeywordsCounts.Length; i++)
            {
                while (enumerator.MoveNext())
                {
                    topKeywordsCounts[i] = Convert.ToInt32(enumerator.Value);
                    topKeywords[i] = Convert.ToString(enumerator.Key);
                    break;
                }
            }

            SortTwoArrays(ref topKeywordsCounts, ref topKeywords, topKeywords.Length);

            Console.WriteLine("Топ {0} слов:", topCount);
            for (int i = 0; i < topCount; i++)
            {
                
                Console.WriteLine("{0}. {1}", i + 1, topKeywords[i]);
            }
            
        }
    }
}
