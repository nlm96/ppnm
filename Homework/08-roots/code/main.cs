using System;
using static System.Math;
using static System.Console;
using System.IO;

class main
{
    static void Main()
    {
        partA();
        partB();

    }

    static void partA(){
        WriteLine("Part A:\n");
        WriteLine("The root-finding routine is tested on some one and two dimensional equations with known analytical roots:\n");
        
        //Test of rootfinder
        // Define the function and starting points for each case
        Func<vector, vector> f1D = (vector x) => new vector(x[0] * x[0] - 4.0);
        Func<vector, vector> f2D = (vector x) => new vector(x[0] * x[0] - 2.0, x[1] * x[1] - 3.0);

        vector x0_1D = new vector(1.0);
        vector x0_2D = new vector(1.0, 1.0);

        // Call the newton function to find the roots
        vector root_1D = rootfinding.newton(f1D, x0_1D);
        vector root_2D = rootfinding.newton(f2D, x0_2D);

        // Print the results for the one-dimensional case
        WriteLine("One-dimensional case:");
        WriteLine("Equation: x^2 - 4 = 0");
        WriteLine("Known analytical root: x = ±2");
        WriteLine($"Initial guess: {x0_1D[0]}");
        WriteLine($"Found root: {root_1D[0]}");
        WriteLine($"Function value at root: {f1D(root_1D)[0]}");
        WriteLine();

        // Print the results for the two-dimensional case
        WriteLine("Two-dimensional case:");
        WriteLine("Equations: x^2 - 2 = 0, y^2 - 3 = 0");
        WriteLine($"Known analytical roots: x = ±{Sqrt(2)}, y = ±{Sqrt(3)}");
        WriteLine($"Initial guess: {x0_2D[0]}, {x0_2D[1]}");
        WriteLine($"Found root: {root_2D[0]}, {root_2D[1]}");
        WriteLine($"Function values at root: ({f2D(root_2D)[0]}, {f2D(root_2D)[1]})");

        WriteLine("\n-------------------------------------------------------------------------------");
        WriteLine("\nThe root finder is now used on Rosenbrock's valley function f(x,y) = (1-x)^2+100(y-x^2)^2");
        WriteLine("To find its extremum points\n");
        //Rosenbrock's valley function
        //f(x,y) = (1-x)^2+100(y-x^2)^2, 
        // Grad(f)= (df/dx,df/dy) = (-2(1-x) + 200*(y-x^2)*(-2x), 200*(y-x^2) ) = (2(x-1) + 400x(x^2-y), 200(y-x^2) )

        WriteLine(" We want to find the roots of Grad(f)=(df/dx, df/dy) = (2(x-1)+400x(x^2-y),200(y-x^2))=0\n");
        Func<vector, vector> gradient = (vector x) => new vector(2*(x[0]-1) + 400*x[0]*(Pow(x[0],2)-x[1]), 200*(x[1]- Pow(x[0],2)) );

        vector x0 = new vector(8.0,8.0);
        vector root = rootfinding.newton(gradient, x0);

        WriteLine($"Initial guess: x_initial={x0[0]}, y_initial={x0[1]}");
        WriteLine($"Found roots: x0={root[0]}, y0={root[1]}");
        WriteLine($"Function values at root: ({gradient(root)[0]}, {gradient(root)[1]})");


    }

    static void partB(){

        //int numPoints = 50; // Number of data points for each parameter sweep

        // Investigate convergence of E0 with respect to rmin
        using (StreamWriter writer = new StreamWriter("../data/rmin_data.data"))
        {
            vector rmin_values = linspace(0.001, 0.3, 50); // Vary rmin values
            for (int i = 0; i < rmin_values.size; i++)
            {
                double rmin = rmin_values[i];
                (vector r_vals, vector f_E, double E0) = E0finder(rmin, 8, 0.01, 0.01);
                writer.WriteLine($"{rmin} {E0}");
            }
        }

        // Investigate convergence of E0 with respect to rmax
        using (StreamWriter writer = new StreamWriter("../data/rmax_data.data"))
        {
            vector rmax_values = linspace(1, 8, 60); // Vary rmax values
            for (int i = 0; i < rmax_values.size; i++)
            {
                double rmax = rmax_values[i];
                (vector r_vals, vector f_E, double E0) = E0finder(0.0001, rmax, 0.01, 0.01);
                writer.WriteLine($"{rmax} {E0}");
            }
        }

        // Investigate convergence of E0 with respect to acc
        using (StreamWriter writer = new StreamWriter("../data/acc_data.data"))
        {
            vector acc_values = linspace(0.000001, 0.1, 30); // Vary acc values
            for (int i = 0; i < acc_values.size; i++)
            {
                double acc = acc_values[i];
                (vector r_vals, vector f_E, double E0) = E0finder(0.001, 8, acc, 0.01);
                writer.WriteLine($"{acc} {E0}");
            }
        }

        // Investigate convergence of E0 with respect to eps
        using (StreamWriter writer = new StreamWriter("../data/eps_data.data"))
        {
            vector eps_values = linspace(0.001, 0.1, 30); // Vary eps values
            for (int i = 0; i < eps_values.size; i++)
            {
                double eps = eps_values[i];
                (vector r_vals, vector f_E, double E0) = E0finder(0.001, 8, 0.01, eps);
                writer.WriteLine($"{eps} {E0}");
            }
        }


        //Writing solution .data files

        (vector r_result, vector f_E_result, double E0_result) = E0finder(0.001, 8, 0.001, 0.001);
        
        saveDataToFile("../data/f_E_data.data", r_result, f_E_result,8);
      
        Console.WriteLine($"Applying the shooting method reveals the binding energy of the lowest bound S-electron in a hydrogen atom as {E0_result}. This aligns with the anticipated value of -0.5.");
        Console.WriteLine($"Visualizing the results, the wavefunction is depicted in solution.svg, mirroring the analytical solution very closely.");
        Console.WriteLine("Additionally, the convergence assessments for rmax, rmin, acc, and eps are graphed in the convergence.svg file.");

    
    
    }
    
    public static (vector,vector,double) E0finder(double rmin, double rmax, double acc, double eps){
        

        Func<vector, vector> ME = (vector E) => new vector(F_E(E, rmin, rmax, acc, eps).Item3);
        
        vector E0_guess = new vector(-0.6);
        vector E0 = rootfinding.newton(ME,E0_guess,0.00000000001);


       

        return (F_E(E0, rmin, rmax, acc, eps).Item1, F_E(E0, rmin, rmax, acc, eps).Item2, E0[0]);


    }
    public static (vector,vector,double) F_E(vector E, double rmin = 0.001, double rmax = 8, double acc = 0.01, double eps = 0.01){
        //return (r,f_E(r),f_E(rmax))
        
        Func<double, vector, vector> SchrodingerODE = (r, y) =>
        {
             // The ODE reads: −(1/2)f′′−(1/r)f=Ef
        
            // If y0=f and y1=f'
            double y0 = y[0]; //f
            double y1 = y[1]; //f'

            //Then we can rewrite it to two 1st order ODE:
            // y0'=y1 and y1'=-2(1/r + E)y0

            double y0_prime = y1;
            double y1_prime = -2*(E[0] + (1 / r)) * y0; //E[0] is the energy

            return new vector(y0_prime, y1_prime);
        };


            //initial values (boundary conditions); at r_min, y0 = f(rmin) = r_min-r_min^2, y1 = f' = 1-2*r_min
            vector y_init = new vector(rmin - rmin*rmin, 1-2*rmin);

             var xlist = new genlist<double>();
             var ylist = new genlist<vector>(); //To ensure we get all results and not just final endpoints. This is inserted in driver.


            (genlist<double> r_values, genlist<vector> y_values) = odesolver.AdaptiveDriver(SchrodingerODE, rmin, y_init, rmax, 0.001, acc, eps, xlist:xlist,ylist:ylist);
            
            vector y_vals = new vector(y_values.size);
            vector r_vals = new vector(r_values.size);
            for (int i = 0; i < y_values.size; i++)
            {
                y_vals[i] = y_values[i][0];
                r_vals[i] = r_values[i]; //Storing the values in vectors instead.
            }
            
            
            var f_E = y_vals; // Solution to schrodinger equation (y-values) 
            var M_E= y_vals[r_vals.size-1]; //f_E(rmax)=M_E, which is the one we should minimize to zero 
            return (r_vals,f_E,M_E);

    }

    static vector linspace(double start, double end, int numPoints)
    {
        vector values = new vector(numPoints);
        double step = (end - start) / (numPoints - 1);
        for (int i = 0; i < numPoints; i++)
        {
            values[i] = start + i * step;
        }
        return values;
    }

    // Function to save data to .data file
    public static void saveDataToFile(string filename, vector x, vector y, int step = 5)
    {
        using (var writer = new StreamWriter(filename))
        {
            for (int i = 0; i < x.size; i += step)
            {
                writer.WriteLine($"{x[i]} {y[i]}");
            }
            writer.WriteLine($"{x[x.size-1]} {y[x.size-1]}");
        }
    }

}
