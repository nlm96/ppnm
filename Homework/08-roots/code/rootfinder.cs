using System;
using static System.Math;
using static System.Console;

public class rootfinding
{
    private static vector dx; // Declare dx as a member variable
    private static vector Δx; // Declare Δx as a member variable

    public static vector newton(Func<vector, vector> f, vector x, double ε = 1e-3)
    {
        int n = x.size;
        dx = new vector(n); // Initialize dx here
        Δx = new vector(n); // Initialize Δx here

        //Need to add condition && Δx.norm() >= dx.norm() somehow
        while (f(x).norm() >= ε && Δx.norm() >= dx.norm())
        {
            matrix J = Jacobian(f, x);
            vector Δx = new QRGS(J).solve(-f(x));
            
            double λ = 1.0;
            while (f(x + λ * Δx).norm() >= (1 - λ / 2) * f(x).norm() && λ >= 1.0 / 1024)
            {
                λ /= 2;
            }
            
            x += λ * Δx;
        }
        
        return x;
    }

    /*
   Guide to using rootfinding.newton function:

   Parameters:
   - f: Function to find the root of. Takes a vector as input and returns a vector. So it can be a whole system of functions.
   - x: Initial guess for the root. Same size as input vector of 'f'.
   - ε: Tolerance for convergence. Default is 1e-3.

   Returns:
   - Approximate root of 'f' within 'ε'.

   Note:
   - Define 'f' to return a vector of same size as 'x'.
   - Choose 'x' close to the root you want to find.
   - Smaller 'ε' gives more accurate but slower convergence.
    */





    public static matrix Jacobian(Func<vector, vector> f, vector x)
    {
        int n = x.size;
        int m = f(x).size;
        matrix J = new matrix(m, n);
        vector dx = new vector(n);

        //WriteLine($"{Sqrt(Double.Epsilon)} and {Pow(2,-26)}");

        for (int i = 0; i < m; i++)
        {
            for (int k = 0; k < n; k++)
            {   
                dx[k] = Pow(2,-26) * Abs(x[k]);
                vector x_modified = x.copy();
                x_modified[k] += dx[k];
                J[i, k] = (f(x_modified)[i] - f(x)[i]) / dx[k];
            }
        }

        return J;
    }
}


/*
class main
{
    static void Main()
    {
        // Define your function and starting point
        Func<vector, vector> f = (vector x) => new vector(x[0] * x[0] - 2.0, x[1] * x[1] - 3.0);
        vector x0 = new vector(1.0, 1.0);

        // Call the newton function to find the root
        vector root = rootfinding.newton(f, x0);

        WriteLine($"Root found at: {root[0]}, {root[1]}");
        WriteLine($"Function value at root: {f(root)}");
    }
}
*/
