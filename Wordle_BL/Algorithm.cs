namespace Wordle_BL;

public class Algorithm
{
    public static List<List<int>> uniqueBinaryCombinations = new();

    static List<List<int>> Run(int[] bits, int numOfThreads, int threadIndex)
    {

        int wordCount = bits.Length;

        void RecursiveCombination(int index, int combinationBit, List<int> currentCombination)
        {
            if (currentCombination.Count == 5)
            {
                uniqueBinaryCombinations.Add(new List<int>(currentCombination));
                return;
            }

            for (int i = index; i < wordCount; i++)
            {
                if ((combinationBit & bits[i]) == 0)
                {
                    currentCombination.Add(bits[i]);
                    combinationBit |= bits[i];
                    RecursiveCombination(i + 1, combinationBit, currentCombination);
                    currentCombination.RemoveAt(currentCombination.Count - 1);
                    combinationBit ^= bits[i];
                }
            }
        }
        RecursiveCombination(0, 0, new List<int>());
        return uniqueBinaryCombinations;
    }
}

