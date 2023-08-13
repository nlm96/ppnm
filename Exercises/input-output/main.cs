using static System.Console;
using static System.Math;

// A class named 'inputoutput'
class inputoutput
{
    // The entry point of the program
    public static int Main(string[] args)
    {
        // Initialize a variable 'result' to keep track of the overall result
        int result = 0;

        // Call exercise1 method and add its result to 'result'
        result += exercise1(args);
        
        // Call exercise2 method and add its result to 'result'
        result += exercise2();
        
        // Call exercise3 method and add its result to 'result'
        result += exercise3(args);
        
        // Return the final result to the operating system
        return result;
    }   

    // Exercise 1: Read numbers from command-line arguments and calculate their sine and cosine
    static int exercise1(string[] args)
    {
        // Print exercise title
        WriteLine("Task 1");
        WriteLine("______________________");

        // Loop through the command-line arguments
        foreach (var arg in args)
        {
            // Split the argument by ':' character
            var words = arg.Split(':');

            // Check if the first part is "-numbers"
            if (words[0] == "-numbers")
            {
                // Get the numbers as a string array by splitting the second part by ','
                var numbers = words[1].Split(',');

                // Loop through each number and calculate its sine and cosine
                foreach (var number in numbers)
                {
                    double x = double.Parse(number);
                    WriteLine($"{x} {Sin(x)} {Cos(x)}");
                }
            }
        }
        WriteLine(""); // Print an empty line
        return 0;
    }

    // Exercise 2: Read numbers from standard input and calculate their sine and cosine
    static int exercise2()
    {
        // Print exercise title
        WriteLine("Task 2");
        WriteLine("______________________");

        // Define the split delimiters (space, tab, and newline) and options
        char[] split_delimiters = {' ','\t','\n'};
        var split_options = System.StringSplitOptions.RemoveEmptyEntries;
        string line;

        // Read lines from standard input until 'end' is entered
        while ((line = ReadLine()) != null && line != "end")
        {
            // Split the line by delimiters and get numbers as a string array
            var numbers = line.Split(split_delimiters, split_options);

            // Loop through each number and calculate its sine and cosine
            foreach(var number in numbers)
            {
                double x = double.Parse(number);
                WriteLine($"{x} {Sin(x)} {Cos(x)}");
            }
        }
        WriteLine(""); // Print an empty line
        return 0;
    }

    // Exercise 3: Read numbers from an input file, calculate their sine and cosine, and write the results to an output file
    static int exercise3(string[] args)
    {
        // Initialize variables to store input and output file names
        string infile = null, outfile = null;

        // Loop through command-line arguments to find input and output file names
        foreach (var arg in args)
        {
            var words = arg.Split(':');
            if (words[0] == "-input") infile = words[1];
            if (words[0] == "-output") outfile = words[1];
        }

        // Check if both input and output file names are provided
        if (infile == null || outfile == null)
        {
            // If not, print an error message and return 1 (indicating failure)
            Error.WriteLine("wrong filename argument");
            return 1;
        }

        // Open input and output streams for reading and writing respectively
        var instream = new System.IO.StreamReader(infile);
        var outstream = new System.IO.StreamWriter(outfile, append: true);

        // Write exercise title to the output file
        outstream.WriteLine("Task 3");
        outstream.WriteLine("______________________");

        // Read numbers from the input file, calculate their sine and cosine, and write results to the output file
        for (string line = instream.ReadLine(); line != null; line = instream.ReadLine())
        {
            double x = double.Parse(line);
            outstream.WriteLine($"{x} {Sin(x)} {Cos(x)}");
        }

        // Close the input and output streams
        instream.Close();
        outstream.Close();

        // Return 0 (indicating success)
        return 0;
    }
}
