using System;
namespace Wordle_BL
{
	public class Binary
	{
        public Dictionary<int, string> bitsDict;
        public int[] bitsWords;
        

        public Binary(List<string> words)
        {
            int wordCount = words.Count();
            bitsDict = new Dictionary<int, string>();
            bitsWords = new int[wordCount];

            for (int i = 0; i < wordCount; i++)
            {
                var bit = 0;
                foreach (var ch in words[i])
                {
                    bit |= 1 << (ch - 'a');
                }
                bitsDict.TryAdd(bit, words[i]);
                bitsWords[i] = bit;
            }
        }

        public string ConvertBitWord(int key)
        {
            try
            {
                if (bitsDict.Count() == 0) return "";
                return bitsDict[key];
            }
            catch
            {
                return "";
            }
        }
        public List<List<string>>? ConvertBitWord(List<List<int>> keyLists)
        {
            List<List<string>> wordLists = new();
            try
            {
                if (bitsDict.Count() == 0) return null;

                foreach (List<int> combination in keyLists)
                {
                    List<string> wordList = new();
                    foreach (int key in combination)
                    {
                        wordList.Add(bitsDict[key]);
                    }
                    wordLists.Add(wordList);
                }
                return wordLists;
            }
            catch
            {
                return null;
            }
        }
    }
}

