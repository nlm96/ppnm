using System;
using System.Threading.Tasks;

public class Program
{
    public static void Main(string[] args)
    {
        int nterms = (int)1e8; // Default value for the number of terms

        // Parse command-line arguments to get the number of terms
        foreach (var arg in args)
        {
            // Split the command-line argument into substrings using ':' as the delimiter
            var words = arg.Split(':');

            // Check if the argument is for setting the number of terms
            if (words[0] == "-terms")
            {
                // Update the number of terms based on the value provided in the argument
                nterms = (int)float.Parse(words[1]);
            }
        }

        // Calculate the harmonic sum using Parallel.For
        double sum = 0;
        Parallel.For(1, nterms + 1, () => 0.0, (i, state, localSum) =>
        {
            return localSum + 1.0 / i;
        }, localSum => sum += localSum);


        Console.WriteLine($"Total harmonic sum from 1 to {nterms} terms using Parallel.For: {sum}");
    }
}
