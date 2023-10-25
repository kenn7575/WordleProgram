using System.Collections.Generic;

namespace Wordle_BL;

public class Algorithm
{
    public static List<List<int>> uniqueBinaryCombinations = new();
    public static int progress = 0;
    public event EventHandler<int> handlePlz;

    public List<List<int>> Run(int[] bits)
    {
        List<List<int>> dataToReturn = new();
        int wordCount = bits.Length;

        //Calculate the total number of combinations using the binomial coefficient
        int totalCombinations = CalculateBinomialCoefficient(wordCount, 5);
        int currentProgress = 0;

        void RecursiveCombination(int index, int combinationBit, List<int> currentCombination)
        {
            if (currentCombination.Count == 5)
            {
                dataToReturn.Add(new List<int>(currentCombination));
                //progress = (int)((double)dataToReturn.Count / totalCombinations * 100);
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

        return dataToReturn;
    }

    // Calculate the binomial coefficient (n choose k)
    private static int CalculateBinomialCoefficient(int n, int k)
    {
        if (k < 0 || k > n)
        {
            return 0;
        }

        int result = 1;
        for (int i = 1; i <= k; i++)
        {
            result *= (n - i + 1);
            result /= i;
        }
        return result;
    }
    protected virtual void UpdateHandlerValue(int input)
    {
        EventHandler<int> e = handlePlz;
        if (e!=null)
        {
            e(this, input);
        }
    }
}

