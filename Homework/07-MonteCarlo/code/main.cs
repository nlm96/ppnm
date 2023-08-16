using System;
using System.IO;
using static System.Console;
using static System.Math;

class Program
{
    static (double, double) unitcircle(StreamWriter outputFile, int N)
    {

        // Define the integration limits for radius (r) and angle (theta)
        double r_min = 0;
        double r_max = 1;
        double theta_min = 0;
        double theta_max = 2 * PI;

        vector a = new vector(r_min, theta_min); //start integral
        vector b = new vector(r_max, theta_max);

        // Define the function in polar coordinates
        Func<vector, double> f = (vector rTheta) => rTheta[0]; //∫_A f dA = ∫[0,1]dr∫[0,2pi]r dtheta => f=r = rTheta[0]. (rTheta[1]=theta)

        // Calculate the integral and estimated error
        var result = montecarlo.plainmc(f, a, b, N);
        var result2 = montecarlo.quasimc(f,a,b,N);
        double integral = result.Item1;
        double qIntegral = result2.Item1;
        double estimatedError = result.Item2;
        double qEstimatedError = result2.Item2;

        // Calculate the actual error
        double actualError = Abs(integral - PI);
        double qActualError = Abs(qIntegral - PI);

        // Calculate 1/sqrt(N)
        double oneOverSqrtN = 1.0 / Sqrt(N);

        // Write the results to the data file
        outputFile.WriteLine($"{N} {integral} {estimatedError} {actualError} {oneOverSqrtN} {qIntegral} {qEstimatedError} {qActualError}");

        return (integral, estimatedError);
    }

    static void partA()
    {
        double highestSamplingArea = 0;
        double highestSamplingError = 0;

        using (StreamWriter outputFile = new StreamWriter("../data/unicircle.data"))
        {
            int[] samplingSizes = { 10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000, 20000, 50000, 100000, 200000, 500000, 1000000, 2000000, 5000000, 10000000 };

            foreach (int N in samplingSizes)
            {
                var (area, error) = unitcircle(outputFile, N);

                // Store the area and error with the highest sampling size
                highestSamplingArea = area;
                highestSamplingError = error;
            }
        }

        WriteLine($"Integral over r^2*sin(theta) with r from 0 to 1 and theta from 0 to 2*pi is calculated as: {highestSamplingArea} +- {highestSamplingError}");
        WriteLine($"It should be pi = {PI}");
        WriteLine("The error as a function of sampling points are plotted in uni_error.svg");
        WriteLine("The area as a function of sampling points are plotted in unicircle.svg");

        var (integralwi,errorwi) = weirdIntegral(10000000);
        WriteLine($"\nIntegral over 1-(cos(x)*cos(y)*cos(z))^-1 is calculated to be {integralwi} +- {errorwi}");
        WriteLine($"It should be Gamma(1/4)^4 / (5*PI^3) = 1.3932039296856768591842462603255");
    }

    static (double, double) weirdIntegral(int N)
    {

        // Define the integration limits for radius (r) and angle (theta)
        double x_min = 0, y_min=0, z_min=0;
        double x_max = PI, y_max= PI, z_max=PI;

        vector a = new vector(x_min, y_min, z_min); //start integral
        vector b = new vector(x_max, y_max, z_max);

        // Define the function in polar coordinates
        Func<vector, double> f = (vector x) => 1/Pow(PI,3) * 1/(1-Cos(x[0])*Cos(x[1])*Cos(x[2])); 
        //∫_A f dA = ∫[0,pi]dx∫[0,pi]dy∫[0,pi]dz 1/pi^3 [1-cos(x)cos(y)cos(z)]^-1 
        // => f=1/pi^3 1/(1-cos(x)cos(y)cos(z)). (x[0]=x, x[1]=y, x[2]=z)

        // Calculate the integral and estimated error
        var resultwi = montecarlo.plainmc(f, a, b, N);
        double integralwi = resultwi.Item1;
        double errorwi = resultwi.Item2;

        return (integralwi, errorwi);
    }


    static void Main()
    {
        partA();

        //Part B();
        WriteLine("\n\nPart B:");
        WriteLine(" A multidimensional quasi-random Monte-Carlo integrator was created.");
        WriteLine(" The scaling of the error is compared to the pseudo-random Monte-Carlo integrator with area of unit circle");
        WriteLine(" in compare.svg.");
        WriteLine(" The calculated area of the unit circle using quasi-random Monte-Carlo is plotted in");
        WriteLine(" UnitCircle.svg.");
    }
}
