using Wordle_BL;

//setup
var stopWatch = new System.Diagnostics.Stopwatch();
stopWatch.Start();
DataProcessing DP = new("alpha.txt");
Binary B = new(DP.words);
Algorithm A = new();

//run algorithm
List<List<int>> resultInBinary = A.Run(B.bitsWords);

//convert back to strings
List<List<string>> resultsInString = B.ConvertBitWord(resultInBinary);
stopWatch.Stop();
//print result
Console.Clear();
Console.WriteLine("Minutes: {0}", stopWatch.Elapsed.Minutes);
Console.WriteLine("Seconds: {0}", stopWatch.Elapsed.Seconds);
Console.WriteLine("Milliseconds: {0}", stopWatch.Elapsed.Milliseconds);
Console.WriteLine("Microseconds: {0}", stopWatch.Elapsed.Microseconds);


foreach (List<string> list in resultsInString)
{
    foreach (string word in list)
    {
        Console.Write(word + " ");
    }
    Console.WriteLine();
}
Console.ReadKey();

