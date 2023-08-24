using System;
using static System.Console;
using System.Collections.Generic;
using System.IO;


class main
{
    static void Main()
    {
        WriteLine("\nPart A:\n");
        partA();
        WriteLine("\n\n-------------------------------------------------------------------------------");
        WriteLine("\nPart B:\n");
        partB();
    }

    static void partA(){
        Console.WriteLine("Finding minimum of Rosenbrock's valley function:");
        Func<vector, double> rosenbrock = x => Math.Pow(1 - x[0], 2) + 100 * Math.Pow(x[1] - x[0] * x[0], 2);
        vector rosenbrockStart = new vector(2, 2); // Starting point
        double rosenbrockAcc = 1e-6; // Accuracy goal
        (vector rosenbrockResult, int rosenbrockNSteps) = minimization.qnewton(rosenbrock, rosenbrockStart, rosenbrockAcc);
        WriteLine($"guess values: {rosenbrockStart[0]}, {rosenbrockStart[1]}");
        Console.WriteLine($"Coordinates for minimum is found to be: ({rosenbrockResult[0]}, {rosenbrockResult[1]})");
        Console.WriteLine($"Minimum found: f({rosenbrockResult[0]}, {rosenbrockResult[1]}) = {rosenbrock(rosenbrockResult)}");
        Console.WriteLine($"Expected minimum: f(1, 1) = 0");
        Console.WriteLine($"Number of steps taken: {rosenbrockNSteps}");
        Console.WriteLine();

        Console.WriteLine("Finding minimum of Himmelblau's function:");
        Func<vector, double> himmelblau = x => Math.Pow(x[0] * x[0] + x[1] - 11, 2) + Math.Pow(x[0] + x[1] * x[1] - 7, 2);
        vector himmelblauStart = new vector(1, 1); // Starting point
        double himmelblauAcc = 1e-6; // Accuracy goal
        (vector himmelblauResult, int himmelblauNSteps) = minimization.qnewton(himmelblau, himmelblauStart, himmelblauAcc);
        WriteLine($"guess values: {himmelblauStart[0]}, {himmelblauStart[1]}");
        Console.WriteLine($"Minimum found: f({himmelblauResult[0]}, {himmelblauResult[1]}) = {himmelblau(himmelblauResult)}");
        Console.WriteLine($"Expected minimums: f(3, 2) = {himmelblau(new vector(3.0, 2.0))}, f(-2.805118, 3.131312) = {himmelblau(new vector(-2.805118, 3.131312))}, f(-3.779310, -3.283186) = {himmelblau(new vector(-3.779310, -3.283186))}, f(3.584428, -1.848126) = {himmelblau(new vector(3.584428, -1.848126))}");
        Console.WriteLine($"Number of steps taken: {himmelblauNSteps}");

    }

    static void partB(){

        WriteLine("Higgs Boson Fitting");
        WriteLine("===================");

        var energyList = new List<double>();
        var signalList = new List<double>();
        var errorList = new List<double>();
        var separators = new char[] { ' ', '\t' };
        var options = StringSplitOptions.RemoveEmptyEntries;
        while (true)
        {
            string line = Console.In.ReadLine();
            if (line == null) break;
            string[] words = line.Split(separators, options);
            energyList.Add(double.Parse(words[0]));
            signalList.Add(double.Parse(words[1]));
            errorList.Add(double.Parse(words[2]));
        }

        WriteLine("Performing Breit-Wigner Fit to Higgs Data");
        vector initialParams = new vector(new double[3] { 16, 126, 2 }); // initial parameter guesses
        vector fittedParams = minimization.FitBreitWigner(BreitWignerFunction, initialParams, energyList, signalList, errorList, 1e-4);
        WriteLine($"Fit results: A = {fittedParams[0]}, m = {fittedParams[1]}, Γ = {fittedParams[2]}");
        WriteLine("Plot of the fit can be found in BreitWigner_fit.svg");

        string dataToWrite = "";
        for (int i = 0; i < 1000; i++)
        {
            double xValue = energyList[0] + (energyList[energyList.Count - 1] - energyList[0]) * i / 1000.0;
            vector paramVector = new vector(new double[4] { xValue, fittedParams[0], fittedParams[1], fittedParams[2] });
            double yValue = BreitWignerFunction(paramVector);
            dataToWrite += $"{xValue}\t{yValue}\n";
        }
        File.WriteAllText("../data/fitted_data.txt", dataToWrite);
        WriteLine("===================");
    }

    public static double BreitWignerFunction(vector par)
    {
        double xValue = par[0];
        double scaleFactor = par[1];
        double mass = par[2];
        double width = par[3];
        return scaleFactor / ((xValue - mass) * (xValue - mass) + width * width / 4);
    }


}
