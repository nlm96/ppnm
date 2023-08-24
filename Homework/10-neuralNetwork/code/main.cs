using System;
using static System.Console;
using static System.Math;

class Program {
    public static void Main() {
        WriteLine("Evaluating Artificial Neural Networks\n");

        // Define the number of hidden neurons
        int hiddenNeurons = 6;

        // Define the activation functions
        Func<double, double> activationFunction1 = x => x * Exp(-x * x);
        Func<double, double> activationFunction2 = x => Cos(5 * x - 1) * Exp(-x * x);

        WriteLine($"Total hidden neurons: {hiddenNeurons}");
        WriteLine("Objective: Interpolating f2(x) = Cos(5*x-1)*Exp(-x*x)");
        WriteLine("Using a Gaussian wavelet as the activation function for the neurons\n");

        // Initialize the artificial neural network
        ann network = new ann(hiddenNeurons, activationFunction1);

        // Define the input range
        double lowerBound = -1;
        double upperBound = 1;

        // Define the number of interpolation points
        int numPoints = 25;

        // Initialize arrays to store input points and target values
        vector inputPoints = new vector(numPoints);
        vector targetValues = new vector(numPoints);

        using (var outfile = new System.IO.StreamWriter("..//data/InterpolationTable.data")) {
            // Generate input points and compute target values
            for (int i = 0; i < inputPoints.size; i++) {
                inputPoints[i] = lowerBound + (upperBound - lowerBound) * i / (numPoints - 1);
                targetValues[i] = activationFunction2(inputPoints[i]);
                outfile.Write($"{inputPoints[i]} {targetValues[i]}\n");
            }
            outfile.Write("\n\n");


            // Initialize network parameters for each hidden neuron
            for (int k = 0; k < network.n; k++) {
                network.p[3 * k + 0] = lowerBound + (upperBound - lowerBound) * k / (network.n - 1);
                network.p[3 * k + 1] = 1;
                network.p[3 * k + 2] = 1;
            }
            //WriteLine("Debug");
            // Train the network using the provided input points and target values
            network.train(inputPoints, targetValues);
            

            // Generate responses using the trained network and write to output file
            for (double z = lowerBound; z <= upperBound; z += 1.0 / 64) {
                outfile.Write($"{z} {network.response(z)}\n");
            }
        }

        // Output the number of steps taken during network training
        WriteLine($"Number of steps to minimize the Cost function in the neural network: {network.N}\n");
        WriteLine("Interpolation can be seen in interpolation.svg");
    }
}
