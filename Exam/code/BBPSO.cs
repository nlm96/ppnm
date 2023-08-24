using System;
using static System.Math;
using System.Collections.Generic;
using System.IO;
using System.Linq;

//The method is created by adapting the PSO algorithm to a bare bones PSO algorithm
public static class BBPSO //Bare Bones Particle Swarm Optimization. Returns coordinates for estimated global minimum inside box V[a,b]
{ 
    public static (vector,int) barebones( //Input parameters
        Func<vector, double> f, // Function to find global minima (inside volume)
        vector a,               // a: The lower bound of the search space for each dimension.
        vector b,               //   b: The upper bound of the search space for each dimension.
        int maxIterations = 10000,   //   maxIterations: Maximum number of iterations to perform.
        double acc = 1e-5 ,         //   acc: Convergence threshold, determines when the algorithm should stop.
        int swarmSize = 30 ,         //   swarmSize: Number of particles in the swarm.
        bool printConvergence = true,  //Set to false If you don't want it to print if it converged or not
        bool tracking = false,
        string filename = "tracking"
        )

        //
    {
        int n = a.size; // Dimensionality of the problem

        // Initialize uniform unit random number generator
        var random = new Random();

        // Initialize particle positions randomly within given rectangular volume
        var x = new vector[swarmSize];
        for (int i = 0; i < swarmSize; i++)
        {
            x[i] = new vector(n);
            for (int j = 0; j < n; j++)
            {
                x[i][j] = a[j] + (b[j] - a[j]) * random.NextDouble();
            }
        }

        // Initialize local best positions
        var p = new vector[swarmSize];
        for (int i = 0; i < swarmSize; i++)
        {
            p[i] = x[i].copy();
        }

        // Initialize global best position
        vector g = p[0];
        double gBest = f(g);
        for (int i = 1; i < swarmSize; i++)
        {
            double fi = f(p[i]);
            if (fi < gBest)
            {
                gBest = fi;
                g = p[i].copy();
            }
        }

        
        
        int iterationsUsed = 0; // Count the number of iterations
        bool converged = false; 
        using (StreamWriter file = new StreamWriter(filename)) // Open the file here
        for (int iter = 0; iter < maxIterations; iter++)
        {
            
            if (tracking)
            {
                {

                    
                    file.Write(iter + " ");
                    for (int particleIndex = 0; particleIndex < swarmSize; particleIndex++)
                    {
                        file.Write($"{x[particleIndex][0]} {x[particleIndex][1]} ");
                    }
                    file.WriteLine();
                    
                }
            }
            


            for (int i = 0; i < swarmSize; i++)
            {
                // Generating random values to update particle positions
                var u = new vector(n);
                for (int j = 0; j < n; j++)
                {
                    for (int k = 0; k < 12; k++)
                    {
                        u[j] += random.NextDouble();
                    }
                    u[j] -= 6; //Numbers from irwin hall distribution.
                    // approximating a random number pulled from a normal distribution between [0,1]
                }

                // Updating particle position based on BBPSO equation
                var new_x = (p[i] + g) / 2.0 + u * ((p[i] - g).norm()); 
                                                                                        /* Here x_ij = (p_{ij} + g_i)/2 + |p_{ij} - g_i|*N(0,1), where N(0,1) is a random number from a normal distribution
                                                                                        //It was not clear from the textbook, how to interpret the rule G( (p_i + g)/2 , ||p_i - g|| ), so I have deduced this from various sources. */
                x[i] = new_x;

                

                var fi = f(new_x);
                if (fi < f(p[i]))
                {
                    p[i] = new_x.copy();
                    if (fi < gBest)
                    {
                        gBest = fi;
                        g = new_x.copy();
                    }
                }

            } //end i

           


            // Check for convergence
            bool localConverged = true;
            for (int i = 0; i < swarmSize; i++)
            {
                if ((p[i] - g).norm() > acc) // Replace 'acc' with your desired convergence threshold
                {
                    localConverged = false;
                    break;
                }
            }
            if (localConverged)
            {
                converged = true;
                break; // Break out of the loop if converged
            }

            iterationsUsed = iter + 1; // Update the iteration count

        } //end Iterations

        if (printConvergence)
        {
            if (converged)
            {
                Console.WriteLine($"Algorithm converged before reaching max iterations. Iterations used:  {iterationsUsed}");
            }
            else
            {
                Console.WriteLine("Algorithm reached max iterations without convergence.");
            }
        }

        return (g,iterationsUsed); // Return the coordinates for estimated global minimum

    }//end barebones




}
