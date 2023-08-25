using System;
using static System.Console;
using static System.Math;
using special.functions;
using special;

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








        //Part B:

        using (var outfile1stD = new System.IO.StreamWriter("..//data/FirstD.data"))
        using (var outfile2ndD = new System.IO.StreamWriter("..//data/SecondD.data"))
        using (var outfileAntiD = new System.IO.StreamWriter("..//data/AntiD.data"))
        {
            // ... (previous code)

            // Generate responses using the trained network and write to output files
            for (double z = -1; z <= 1; z += 1.0 / 64)
            {
                double d_response = network.d_response(z); // Computed derivative
                double dd_response = network.dd_response(z); // Computed second derivative
                double antiderivative_response = network.antiderivative_response(z); // Computed antiderivative

                // Analytical values
                double analytical_d_response = -2 * z * Cos(5 * z - 1) * Exp(-z * z) - 5 * Sin(5 * z - 1) * Exp(-z * z);
                double analytical_dd_response = Exp(-z*z)*((4*z*z-27)*Cos(5*z-1) + 20*z*Sin(5*z-1));
                double analytical_antiderivative_response = -0.5 * sfuns.erf(z);

                // Write to separate output files
                outfile1stD.Write($"{z} {d_response} {analytical_d_response}\n");
                outfile2ndD.Write($"{z} {dd_response} {analytical_dd_response}\n");
                outfileAntiD.Write($"{z} {antiderivative_response} {intg(z,0)}\n");
            }
        }

        WriteLine("\n\n Part b:\n");
        WriteLine("The first and second derivatives and the anti-derivative");
		WriteLine("can be seen in Plot.svg along with the analytical solutions.");





    }

    public static double Wavelet(double x){
        return Cos(5*x-1)*Exp(-Pow(x,2)); 
    }
	

    public static double intg(double x, double a){
        return integration.integrate(Wavelet, a, x);
    }
}
