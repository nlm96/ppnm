using System;
using System.Threading;
using static System.Console;
using static System.Math; 
using System.IO;

public static class main{

    
    public static void Main(){
        vector x_data = new vector(new double[] {1, 2, 3, 4, 6, 9, 10, 13, 15}); //Time data
        vector y_data = new vector(new double[] {117, 100, 88, 72, 53, 29.5, 25.2, 15.2, 11.1}); 
        vector dy = new vector(new double[] {5, 5, 5, 4, 4, 3, 3, 2, 2}); //Uncertainties data points

        vector x_plot = fit.linspace(0,20,1000);
        var (y_fit,y_upper,y_lower,lambda,lambda_err,a,a_err) = fit.FitExponential(x_data, y_data, dy, x_plot);

        WriteDataToFile(x_data, y_data, dy, x_plot, y_fit, y_upper, y_lower,"Expfit"); 

        WriteLine("Part A,B,C are all solved at the same time:\n");

        WriteLine($"The best fit is found to be:\n y(x)={a}*exp(-{lambda}*x)");
        WriteLine($"With uncertainties: a={a}±{a_err} , lambda={lambda}±{lambda_err}");

        WriteLine("\npart(A): The best fit is and data with errorbars is plotted in Decay.svg in plots folder");
        WriteLine("Part C): The upper and lower bounds to the fit is plotted in Decay.svg");

        double T_half= Log(2)/lambda;
        double T_half_err = lambda_err*Log(2)/(Pow(lambda,2)); //Error prop

        WriteLine("\n\nThe halflife is found to be (part A and B):");
        WriteLine($"HalfLife = {T_half}±{T_half_err} days");
        WriteLine("The known value of 224Ra is 3.631(2) days \"Bé et al., 2004; DDEP, 2018\", So it does not agree.");





    }

    public static void WriteDataToFile(vector x_data, vector y_data, vector dy, vector x_plot, vector y_fit, vector y_upper, vector y_lower, string filename)
    {
        string path = @"../data/"; // Update with your desired folder location

        // Create a StreamWriter to write to the data file
        using (StreamWriter writer = new StreamWriter(Path.Combine(path, filename + "_data.data")))
        {
            for (int i = 0; i < x_data.size; i++)
            {
                writer.WriteLine($"{x_data[i]} {y_data[i]} {dy[i]}");
            }
        }

        // Create a StreamWriter to write to the plot file
        using (StreamWriter writer = new StreamWriter(Path.Combine(path, filename + "_plot.data")))
        {
            for (int i = 0; i < x_plot.size; i++)
            {
                writer.WriteLine($"{x_plot[i]} {y_fit[i]} {y_upper[i]} {y_lower[i]}");
            }
        }
    }



}