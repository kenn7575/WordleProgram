using System;
namespace Wordle_BL
{
	public class Binary
	{
        public Dictionary<int, string> bitsDict;
        public int[] bitsWords = { };

        public Binary(List<string> words)
        {
            int wordCount = words.Count();
            bitsDict = new Dictionary<int, string>();

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
    }
}

