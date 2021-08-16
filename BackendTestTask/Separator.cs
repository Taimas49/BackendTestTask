using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Npgsql;

namespace BackendTestTask
{
    class Separator
    {
        private readonly char[] delimitersChars = { ' ', ',', '.', '!', '?', '"', ';', ':', '[', ']', '(', ')', '\n', '\r', '\t' };
        private void ShowStatistics(IEnumerable<dynamic> uniqueWords)
        {
            Console.WriteLine("СТАТИСТИКА ПОВТОРЯЮЩИХСЯ СЛОВ");
            foreach (var item in uniqueWords)
            {
                Console.WriteLine($"Слово: {item.Word} Количество повторов: {item.Frequency}");
            }
        }
        public void CreateWords(string text)
        {

            string[] words = text.Split(delimitersChars, StringSplitOptions.RemoveEmptyEntries);
            var uniqueWords = words.GroupBy(x => x)
                              .Where(x => x.Count() > 1)
                              .Select(x => new { Word = x.Key, Frequency = x.Count() });
            ShowStatistics(uniqueWords);

            DBManager.SaveStatisticsToDB(uniqueWords);

        }
    }
}
