using System;
using System.Threading;

// Create a data class to hold the start and end indices of the range for each thread
// and the partial sum calculated by each thread
public class data
{
    public int a, b;   // Start and end indices of the range for the thread
    public double sum; // Partial sum calculated by the thread
}

public class Program
{
    // The function to be run in each thread
    public static void harmonic(object obj)
    {
        var local = (data)obj; // Cast the object back to the data class
        local.sum = 0;         // Initialize the partial sum to zero

        // Calculate the harmonic sum for the range [a, b)
        for (int i = local.a; i < local.b; i++)
            local.sum += 1.0 / i;
    }

    public static void Main(string[] args)
    {
        int nthreads = 1, nterms = (int)1e8; // Default values for number of threads and terms

        // Parse command-line arguments to get the number of threads and terms
        foreach (var arg in args)
        {
            // Split the command-line argument into substrings using ':' as the delimiter
            var words = arg.Split(':');

            // Check if the argument is for setting the number of threads
            if (words[0] == "-threads")
            {
                // Update the number of threads based on the value provided in the argument
                nthreads = int.Parse(words[1]);
            }

            // Check if the argument is for setting the number of terms
            if (words[0] == "-terms")
            {
                // Update the number of terms based on the value provided in the argument
                nterms = (int)float.Parse(words[1]);
            }
        }


        // Create an array of data objects to be used in each thread
        data[] x = new data[nthreads];
        for (int i = 0; i < nthreads; i++)
        {
            x[i] = new data();
            x[i].a = 1 + nterms / nthreads * i;               // Calculate the start index for the thread
            x[i].b = 1 + nterms / nthreads * (i + 1);         // Calculate the end index for the thread
        }
        x[x.Length - 1].b = nterms + 1; // Adjust the endpoint of the last thread to cover the remaining terms

        // Create an array to hold the threads
        var threads = new Thread[nthreads];

        // Start each thread with its corresponding data object
        for (int i = 0; i < nthreads; i++)
        {
            threads[i] = new Thread(harmonic); // Create a thread
            threads[i].Start(x[i]);           // Run it with x[i] as an argument to "harmonic"
        }

        // Wait for each thread to finish
        for (int i = 0; i < nthreads; i++)
        {
            threads[i].Join();
        }

        // Calculate the total sum from all threads
        double total = 0;
        for (int i = 0; i < nthreads; i++)
        {
            total += x[i].sum;
        }

        Console.WriteLine($"Total harmonic sum from 1 to {nterms} terms using {nthreads} threads: {total}");
    }
}
