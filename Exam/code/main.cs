using System;
using static System.Math;
using static System.Console;
using System.IO;

class Program
{
    static void Main()
    {
        WriteLine("\nExam Q12: Bare Bones Particle Swarm Optimization\n\n");
        simpletest();
        hardertest();
        Rosenbrock();
        convergence();

        WriteLine("\n\n-----------------------------------------------------------------------------------------");
        WriteLine("A gif tracking the particles motion during the algorithm can be seen in particle_movement.gif");
        WriteLine("The gif represents the finding of global minima on (x-2)^2 + (y-2)^2 +2*sin(3pi/4 *x) + 2*sin(3pi/4 *y)");
        Func<vector, double> hardFunc = v => Pow(v[0] - 2, 2) + Pow(v[1] - 2, 2) + 2 * Sin(3 * PI / 4 * v[0]) + 2 * Sin(3 * PI / 4 * v[1]);

        // Define the search space bounds [a, b]
        vector a = new vector(-1000, -1000);
        vector b = new vector(1000, 1000);

        // Run BBPSO algorithm on the Rosenbrock's valley function
        (vector estimatedGlobalMinimum_point, int iterations) = BBPSO.barebones(hardFunc, a, b,100,tracking:true,filename:"../data/rosenbrockTracking.data");

    }

    static void convergence(){

        // Define the function using a lambda expression
        Func<vector, double> rosenbrockFunc = v => Pow(1 - v[0], 2) + 100 * Pow(v[1] - v[0] * v[0], 2);

        // Define the search space bounds [a, b]
        vector a = new vector(-300, -300);
        vector b = new vector(300, 300);

        // Define parameter ranges and number of points
        vector maxIterationsRange = linspace(1, 10000, 1000);
        vector accRange = linspace(1e-10, 3, 30);
        vector swarmSizeRange = linspace(2, 30, 28);

        // Create separate .data files for each parameter
        using (StreamWriter maxIterWriter = new StreamWriter("../data/maxIterations.data"))
        {
            for (int i = 0; i < maxIterationsRange.size; i++)
            {
                int maxIterations = (int)maxIterationsRange[i];

                // Run BBPSO algorithm on the Rosenbrock's valley function
                (vector estimatedGlobalMinimum_point, int iterations) = BBPSO.barebones(rosenbrockFunc, a, b, maxIterations: maxIterations, printConvergence:false);

                // Calculate the functional value at the estimated global minimum
                double functionalValue = rosenbrockFunc(estimatedGlobalMinimum_point);

                // Write data to file
                maxIterWriter.WriteLine($"{maxIterations}\t{iterations}\t{functionalValue}");
            }
        }

        using (StreamWriter accWriter = new StreamWriter("../data/acc.data"))
        {
            for (int i = 0; i < accRange.size; i++)
            {
                double acc = accRange[i];

                // Run BBPSO algorithm on the Rosenbrock's valley function
                (vector estimatedGlobalMinimum_point, int iterations) = BBPSO.barebones(rosenbrockFunc, a, b, acc: acc,printConvergence:false);

                // Calculate the functional value at the estimated global minimum
                double functionalValue = rosenbrockFunc(estimatedGlobalMinimum_point);

                // Write data to file
                accWriter.WriteLine($"{acc}\t{iterations}\t{functionalValue}");
            }
        }

        using (StreamWriter swarmSizeWriter = new StreamWriter("../data/swarmSize.data"))
        {
            for (int i = 0; i < swarmSizeRange.size; i++)
            {
                int swarmSize = (int)swarmSizeRange[i];

                // Run BBPSO algorithm on the Rosenbrock's valley function
                (vector estimatedGlobalMinimum_point, int iterations) = BBPSO.barebones(rosenbrockFunc, a, b, swarmSize: swarmSize, printConvergence:false);

                // Calculate the functional value at the estimated global minimum
                double functionalValue = rosenbrockFunc(estimatedGlobalMinimum_point);

                // Write data to file
                swarmSizeWriter.WriteLine($"{swarmSize}\t{iterations}\t{functionalValue}");
            }
        }

        WriteLine("\n\n-------------------------------------------------------------------------------------------");
        WriteLine("Convergence tests has been done on the rosenbrockFunc, by trying to vary the input parameters");
        WriteLine("I have plotted the functional value evaluated at the estimated global minimum as function of changing input parameter");
        WriteLine("I have additionally tracked the number of iterations used");
        WriteLine("The convergence tests can be seen in the plot folder");
        WriteLine("However, it seems that the algorithm finds the minmum relatively fast.");
        WriteLine("Since the particles start position is generated randomly, each run has different starting points, which makes it difficult to compare");
        WriteLine("So to truly make a correct test, one would have to make a statistical analysis for the convergence");

    }

    static void Rosenbrock(){
        WriteLine("---------------------------------------------------------------------------------------------------");
        WriteLine("The function being tested is  Rosenbrock's valley function: f(x,y) = (1-x)^2 + 100(y-x^2)^2");
        WriteLine("This function has a clear global minimum around (1,1) but a narrow valley\n");

        // Define the function using a lambda expression
        Func<vector, double> rosenbrockFunc = v => Pow(1 - v[0], 2) + 100 * Pow(v[1] - v[0] * v[0], 2);

        // Define the search space bounds [a, b]
        vector a = new vector(-1000, -1000);
        vector b = new vector(1000, 1000);
        WriteLine($"Using lower search bound: ({a[0]},{a[1]})");
        WriteLine($"Using upper search bound: ({b[0]},{b[1]})\n");

        // Run BBPSO algorithm on the Rosenbrock's valley function
        (vector estimatedGlobalMinimum_point, int iterations) = BBPSO.barebones(rosenbrockFunc, a, b,10000);

        // Calculate the true global minimum for comparison
        vector trueGlobalMinimum_point = new vector(1, 1); // Theoretical
        double trueGlobalMinimumValue = 0;

        // Print the results
        WriteLine("\nEstimated Global Minimum:");
        WriteLine($"Coordinates (x0,y0): ({estimatedGlobalMinimum_point[0]}, {estimatedGlobalMinimum_point[1]})");
        WriteLine($"Functional value f(x0,y0): {rosenbrockFunc(estimatedGlobalMinimum_point)}");
        WriteLine($"Number of iterations used: {iterations}");

        WriteLine("\nTrue Global Minimum:");
        WriteLine($"Coordinates (x0,y0): ({trueGlobalMinimum_point[0]}, {trueGlobalMinimum_point[1]})");
        WriteLine($"Functional value f(x0,y0): {trueGlobalMinimumValue}\n\n");
    }



    static void simpletest(){
        WriteLine("---------------------------------------------------------------------------------------------------");
        WriteLine("Simple test of algorithm:\n");
        // Define the test function equation explicitly
        WriteLine("The function being tested is: f(x, y) = (x - 2)^2 + y^2\n");

        // Define the function using a lambda expression
        Func<vector, double> testFunction = (v) => Pow(v[0] - 2, 2) + Pow(v[1], 2);

        // Define the search space bounds [a, b]
        vector a = new vector(-100, -100);
        vector b = new vector(100, 100);

        // Run BBPSO algorithm on the test function
        (vector estimatedGlobalMinimum_point, int iterations) = BBPSO.barebones(testFunction, a, b);

        // Calculate the true global minimum for comparison
        vector trueGlobalMinimum_point = new vector(2, 0); //Theoretical
        double trueGlobalMinimumValue = testFunction(trueGlobalMinimum_point);

        // Print the results
        WriteLine("\nEstimated Global Minimum:");
        WriteLine($"Coordinates (x0,y0): ({estimatedGlobalMinimum_point[0]}, {estimatedGlobalMinimum_point[1]})");
        WriteLine($"Functional value f(x0,y0): {testFunction(estimatedGlobalMinimum_point)}");

        WriteLine("\nTrue Global Minimum:");
        WriteLine($"Coordinates (x0,y0): ({trueGlobalMinimum_point[0]}, {trueGlobalMinimum_point[1]})");
        WriteLine($"Functional value f(x0,y0): {trueGlobalMinimumValue}");

    }

    static void hardertest(){
        
        WriteLine("---------------------------------------------------------------------------------------------------");
        WriteLine("test of algorithm:\n");
        // Define the test function equation explicitly
        WriteLine("The function being tested is: f(x,y) = (x-2)^2+(y-2)^2+2sin(3*pi/4 *x)+2sin(3*pi/4* y)");
        WriteLine("This function has a clear global minimum around (2,2) but a lot of local minima too\n");

        // Define the function using a lambda expression
        Func<vector, double> hardFunc = v => Pow(v[0] - 2, 2) + Pow(v[1] - 2, 2) + 2 * Sin(3 * PI / 4 * v[0]) + 2 * Sin(3 * PI / 4 * v[1]);

        
        // Define the search space bounds [a, b]
        vector a = new vector(-1000, -1000);
        vector b = new vector(1000, 1000);
        WriteLine($"Using lower search bound: ({a[0]},{a[1]})\nUsing upper search bound: ({b[0]},{b[1]})\n");

        // Run BBPSO algorithm on the test function
        (vector estimatedGlobalMinimum_point, int iterations) = BBPSO.barebones(hardFunc, a, b,10000);

        // Calculate the true global minimum for comparison
        vector trueGlobalMinimum_point = new vector(2, 2); //Theoretical
        double trueGlobalMinimumValue = -4;

        // Print the results
        WriteLine("\nEstimated Global Minimum:");
        WriteLine($"Coordinates (x0,y0): ({estimatedGlobalMinimum_point[0]}, {estimatedGlobalMinimum_point[1]})");
        WriteLine($"Functional value f(x0,y0): {hardFunc(estimatedGlobalMinimum_point)}");

        WriteLine("\nTrue Global Minimum:");
        WriteLine($"Coordinates (x0,y0): ({trueGlobalMinimum_point[0]}, {trueGlobalMinimum_point[1]})");
        WriteLine($"Functional value f(x0,y0): {trueGlobalMinimumValue}\n\n");

        

    }



    public static vector linspace(double start, double end, int numPoints)
    {
        vector result = new vector(numPoints);
        double step = (end - start) / (numPoints - 1);
        double value = start;
        for (int i = 0; i < numPoints; i++)
        {
            result[i] = value;
            value += step;
        }
        return result;
    }
}