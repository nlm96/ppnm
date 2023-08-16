using static System.Math;
using System;
using static System.Console;

public partial class montecarlo
{
    // Regular Monte Carlo integration using random points
    // This method estimates the integral of a function over a hyperrectangle using random points.
    // Parameters:
    // - f: The function to integrate.
    // - a: The lower bounds of the hyperrectangle.
    // - b: The upper bounds of the hyperrectangle.
    // - N: The number of random points to use for the estimation.
    // Returns: A tuple containing the estimated integral value and the estimated error.
    public static (double, double) plainmc(Func<vector, double> f, vector a, vector b, int N)
    {
        int dim = a.size;
        var rand = new Random();
        double V = 1.0;

        // Calculate the volume of the integration region
        for (int i = 0; i < dim; i++)
        {
            V *= b[i] - a[i];
        }

        double sum = 0.0;
        double sum2 = 0.0;
        var x = new vector(dim);

        // Generate N random points and evaluate the function at each point
        for (int n = 0; n < N; n++)
        {
            for (int k = 0; k < dim; k++)
            {
                x[k] = a[k] + rand.NextDouble() * (b[k] - a[k]);
            }
            double fx = f(x);
            sum += fx;
            sum2 += fx * fx;
        }

        double mean = sum / N;
        double sigma = Sqrt(sum2 / N - mean * mean);
        var result = (mean * V, sigma * V / Sqrt(N));
        return result;
    }

    // Quasi-Monte Carlo integration using the Halton sequence
    // This method estimates the integral of a function over a hyperrectangle using the Halton sequence.
    public static (double, double) quasimc(Func<vector, double> f, vector a, vector b, int N)
    {
        int dim = a.size;
        double V = 1;

        // Calculate the volume of the integration region
        for (int i = 0; i < dim; i++)
        {
            V *= b[i] - a[i];
        }

        double sum = 0;
        double sum2 = 0;
        var x = new vector(dim);
        var x2 = new vector(dim);

        // Generate N quasi-random points using the Halton sequence and evaluate the function at each point
        for (int n = 1; n < N; n++)
        {
            var halton1 = halton(n, dim);  // Regular Halton sequence
            var halton2 = halton(n, dim + 1);  // Slightly shifted Halton sequence

            for (int k = 0; k < dim; k++)
            {
                x[k] = a[k] + halton1[k] * (b[k] - a[k]); //
                x2[k] = a[k] + halton2[k] * (b[k] - a[k]);
            }
            
            sum += f(x);
            sum2 += f(x2);
        }

        double mean = sum / N;
        double mean2 = sum2 / N;

        // Estimate error as the difference between two means
        var result = (mean * V, V * Abs(mean - mean2));
        return result;
    }

    // Corput sequence for quasi-random number generation
    // This method generates the n-th element of the Corput sequence in base b.
    public static double corput(int n, int b)
    {
        double q = 0;
        double bk = 1.0 / b;

        // Convert n to the b-base representation
        while (n > 0)
        {
            q += (n % b) * bk;
            n /= b;
            bk /= b;
        }

        return q;
    }

    // Halton sequence generator for quasi-random number generation
    // This method generates the n-th element of the Halton sequence in d dimensions.
    // Parameters:
    // - n: The index of the element to generate.
    // - d: The number of dimensions of the sequence.
    // Returns: A vector containing the n-th element of the Halton sequence in d dimensions.
    public static vector halton(int n, int d)
    {
        vector x = new vector(d);
        int[] b = new int[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61 };

        if (d > b.Length)
        {
            throw new Exception("d is larger than the dimension of b.");
        }

        // Generate the Halton sequence for each dimension
        for (int i = 0; i < d; i++)
        {
            x[i] = corput(n, b[i]);
        }

        return x;
    }
}
