using System;
using System.IO;

namespace Wordle_BL
{
    public class DataProcessing
    {
        public List<string> words = new();

        public DataProcessing(string filePath)
        {
            words = readFile(filePath);
        }
        public DataProcessing(List<string> data)
        {
            words = data;
        }
        public List<string> processData()
        {
            List<string> processedWords = new();

            foreach (string word in words)
            {
                string? processedWord = processDataPoint(word);
                if(processedWord != null)
                {
                    processedWords.Add(processedWord);
                }
            }

            return processedWords;
        }
        public string? processDataPoint(string data)
        {
            string word = data.ToLower();
            if (word.Length != 5) return null;
            if (word.Distinct().Count() != 5) return null;
            if (words.Where(x => string.Concat(x, word).Distinct().Count() == 5).Count() > 0) return null;
            return word;

        }
        public List<string> readFile(string pathName)
        {
            List<string> lines = new List<string>();

            try
            {
                using (StreamReader sr = new StreamReader(pathName))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        line = line.ToLower();
                        if (line.Length != 5) continue;
                        if (line.Distinct().Count() != 5) continue;
                        if (lines.Where(x => string.Concat(x, line).Distinct().Count() == 5).Count() > 0) continue;
                        lines.Add(line);
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e.ToString());
            }
            return lines;
        }
    }
}

