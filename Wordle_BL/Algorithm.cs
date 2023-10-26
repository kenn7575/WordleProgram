using System.Collections.Generic;
using System.ComponentModel;

namespace Wordle_BL;

public class Algorithm
{
    public static List<List<int>> uniqueBinaryCombinations = new();
    public static int progress = 0;
    //public event EventHandler<int> handlePlz;

    public List<List<int>> Run(int[] bits, BackgroundWorker worker)
    {
        List<List<int>> dataToReturn = new();
        int wordCount = bits.Length;

        //Calculate the total number of combinations using the binomial coefficient
       
        int currentProgress = 0;

        void RecursiveCombination(int index, int combinationBit, List<int> currentCombination, BackgroundWorker worker)
        {
            if (currentCombination.Count == 1)
            {
                progress = (int)(((double)index / wordCount) * 100f);
                worker.ReportProgress(progress);
            }
            if (currentCombination.Count == 5)
            {
                dataToReturn.Add(new List<int>(currentCombination));
                //progress = (int)((double)dataToReturn.Count / totalCombinations * 100);
                //UpdateHandlerValue(progress);
                return;
            }
            for (int i = index; i < wordCount; i++)
            {
                if ((combinationBit & bits[i]) == 0)
                {
                    currentCombination.Add(bits[i]);
                    combinationBit |= bits[i];
                    RecursiveCombination(i + 1, combinationBit, currentCombination, worker);
                    currentCombination.RemoveAt(currentCombination.Count - 1);
                    combinationBit ^= bits[i];
                }
            }
        }

        RecursiveCombination(0, 0, new List<int>(), worker);

        return dataToReturn;
    }

    // Calculate the binomial coefficient (n choose k)
    
    protected virtual void UpdateHandlerValue(int input)
    {
        //EventHandler<int> e = handlePlz;
        //if (e!=null)
        //{
        //    e(this, input);
        //}
    }
}

