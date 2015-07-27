using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace keywordSearcher
{
    public class FileReader
    {
        private string path;
        private string[] lines;

        /// <param name="path">Путь к файлу</param>
        public FileReader(string path)
        {
            this.path = path;
        }

        private void OutputError()
        {
            Console.WriteLine("Такой файл отсутствует");
            Console.ReadLine();
            Environment.Exit(0);
        }

        private void ReadFromFile()
        {
            if (File.Exists(path))
            {
                lines = File.ReadAllLines(path);
            }
            else
            {
                OutputError();
            }
        }
        /// <summary>
        ///Возвращает массив строк из файла. Если директория указана неверно, вернет Error в 1 строке.>
        /// </summary>
        public string[] GetLines()
        {
            ReadFromFile();
            return lines;
        }

    }
}
