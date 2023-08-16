using System;
using static System.Console;
using static System.Math;
using special.functions;
using System.IO;

class Program
{

    static void Main()
    {
        partA();
        partB();
    }

    static void partA()
    {
        WriteLine("Part (A):\n\n");

        WriteLine("A test with the following integrals is done:\n");
        double result1 = integration.integrate(x => Sqrt(x), 0, 1);
        WriteLine($"∫[0,1] √x dx = {result1} (Expected: 2/3)");

        double result2 = integration.integrate(x => 1 / Sqrt(x), 0, 1);
        WriteLine($"∫[0,1] 1/√x dx = {result2} (Expected: 2)");

        double result3 = integration.integrate(x => 4 * Sqrt(1 - x * x), 0, 1);
        WriteLine($"∫[0,1] 4√(1-x^2) dx = {result3} (Expected: π)");

        double result4 = integration.integrate(x => Log(x) / Sqrt(x), 0, 1);
        WriteLine($"∫[0,1] ln(x)/√x dx = {result4} (Expected: -4)");

        vector x_plot = linspace(-5,5,1000);


        vector erf_values = new vector(x_plot.size);
        vector erfIntegral_values = new vector(x_plot.size);

        for (int i = 0; i < x_plot.size; i++)
        {
            erf_values[i] = sfuns.erf(x_plot[i]);
            erfIntegral_values[i] = sfuns.erfIntegral(x_plot[i]);
        }

        SaveDataToFile("erf", x_plot, erf_values);
        SaveDataToFile("erfIntegral", x_plot, erfIntegral_values);
        
        Console.WriteLine("The error function (erf(x)) has been implemented using its integral representation.");
        Console.WriteLine("The comparison between the computed result and the approximation from the plot exercise is displayed in erf.svg.");
        Console.WriteLine("Upon observation at the plotted scale, no significant distinction is evident.");

    }

    static void partB()
    {
        WriteLine("\n\nPart B:\n");
        WriteLine("Testing Clenshaw Curtis transformation:");


        double B_result1 = integration.ClenshawCurtisIntegrate(x => 1 / Sqrt(x), 0, 1);
        int evals1 = integration.evals; 

        double B_result1_norm = integration.integrate(x => 1 / Sqrt(x), 0, 1);
        int evals1_norm = integration.evals;

        WriteLine($"With Clenshaw Curtis: ∫[0,1] 1/√x dx = {B_result1} (Expected: 2), Evaluations: {evals1}");
        WriteLine($"With normal integrate: ∫[0,1] 1/√x dx = {B_result1_norm} (Expected: 2), Evaluations: {evals1_norm}");


        double B_result2 = integration.ClenshawCurtisIntegrate(x => Log(x) / Sqrt(x), 0, 1);
        int evals2 = integration.evals;

        double B_result2_norm = integration.integrate(x => Log(x) / Sqrt(x), 0, 1);
        int evals2_norm = integration.evals;
        
        WriteLine($"With Clenshaw Curtis: ∫[0,1] ln(x)/√x dx = {B_result2} (Expected: -4), Evaluations: {evals2}");
        WriteLine($"With normal integrate: ∫[0,1] ln(x)/√x dx = {B_result2_norm} (Expected: -4), Evaluations: {evals2_norm}");

        WriteLine();
        WriteLine("Compared to python they find the results, with the same tolerance, in 231 and 315 evaluations respectivly");
    }



    public static vector linspace(double x_start, double x_end, int numPoints)
    {
        if (numPoints <= 1)
            throw new ArgumentException("Number of points must be greater than 1.");

        vector x_plot = new vector(numPoints);

        double stepSize = (x_end - x_start) / (numPoints - 1);

        for (int i = 0; i < numPoints; i++)
        {
            x_plot[i] = x_start + i * stepSize;
        }

        return x_plot;
    }

    public static void SaveDataToFile(string filename, vector x, vector y)
    {
        using (StreamWriter writer = new StreamWriter($"../data/{filename}.data"))
        {
            for (int i = 0; i < x.size; i++)
            {
                writer.WriteLine($"{x[i]} {y[i]}");
            }
        }
    }
}
