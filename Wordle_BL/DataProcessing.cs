using System;
namespace Wordle_BL
{
    public class DataProcessing
    {
        public List<string> words = new();

        public DataProcessing(string filePath)
        {

        }
        public DataProcessing()
        {

        }
        public List<string> processData(List<string> data)
        {
            List<string> processedWords = new();

            foreach (string _word in words)
            {

                string word = _word.ToLower();
                if (word.Length != 5) continue;
                if (word.Distinct().Count() != 5) continue;
                if (word.Where(x => string.Concat(x, word).Distinct().Count() == 5).Count() > 0) continue;
                processedWords.Add(word);
            }
            return processedWords;
        }
    }
}

