using System;
using static System.Console;
using static System.Math; 
using System.IO;
using System.Collections.Generic;

public static class main{
    public static void Main(){

        double[] x = new double[]{0,1,2,3,4,5,6,7,8,9,10}; //x-data points.
        double[] y = new double[x.Length];
        double[] z = GenerateZPoints(x,1,1); //Generating z-points list


        for(int i = 0; i<x.Length; i++){
            y[i] = Cos(x[i]); //Using Cos as test function
        } 

        double[] xs = Linspace(0,10,1000); // xs is a list where I want to plot the actual function represented (cos(xs)).
        double[] func = new double[xs.Length];
        double[] int_func = new double[xs.Length];

        for (int i = 0; i < xs.Length; i++)
        {
            func[i] = Cos(xs[i]); // Calculate y-values using cos(x)
            int_func[i] = Sin(xs[i]);
        }

        WriteDataToFile(xs,func,"cos.data");
        WriteDataToFile(xs,int_func,"sin.data");
        WriteDataToFile(x,y,"data.data");

        partA(x,y,z);

        partB();


    }

    public static void partA(double[] x,double[] y, double[] z){
        
        WriteLine("Part A:\n");
        WriteLine($"The indicative plot of the linear spline is in linspline.svg");
        WriteLine("The data is represented by a cosine(x) function");

        double[] linspline = new double[z.Length];
        double[] int_linspline = new double[z.Length];

        for(int i = 0; i<z.Length; i++){
            linspline[i] = LinearSpline.linterp(x,y,z[i]); //Lists with linear interpolated data
            int_linspline[i] = LinearSpline.linterpInteg(x,y,z[i]);
        }

        WriteDataToFile(z,linspline,"linspline.data");
        WriteDataToFile(z,int_linspline,"int_linspline.data");
    }

    public static void partB(){
        WriteLine("\n\npart (B):");

        vector x = new vector("1,2,3,4,5");
        vector z = GenerateZPoints(x,4,1);
        vector y_1 = new vector(x.size);
        vector y_lin = new vector(x.size);
        vector y_square = new vector(x.size);
        vector y_cos = new vector(x.size);

        vector[] yValues = new vector[] { y_1, y_lin, y_square, y_cos };

        for(int i = 0; i<x.size; i++){
            y_1[i] = 1;
            y_lin[i] = x[i];
            y_square[i] = x[i]*x[i];
            y_cos[i] = Cos(x[i]);
        }

        qspline spline1 = new qspline(x, y_1);
        qspline spline2 = new qspline(x, y_lin);
        qspline spline3 = new qspline(x, y_square);
        qspline spline4 = new qspline(x, y_cos);

        List<qspline> splines = new List<qspline> { spline1, spline2, spline3, spline4 };


        string[] filenames = { "qspline_const", "qspline_lin", "qspline_square", "qspline_cos" };

        WriteSplineDataToFiles(splines, x, yValues, z, filenames);


        WriteLine("The three functions and its splines are plotted in qspline.svg");
        WriteLine("The following are the results of calculating b_i and c_i manually, and with my implementation, for the three functions, using my quadratic spline. All are using c_1=0 and only with forward recursion:");
        WriteLine("y=1");
        WriteLine($"i=1: expected c_1 = 0, calculated c_1 = {spline1.c[0]}, expected b_1 = 0, calculated b_1 = {spline1.b[0]}");
        WriteLine($"i=2: expected c_2 = 0, calculated c_2 = {spline1.c[1]}, expected b_2 = 0, calculated b_2 = {spline1.b[1]}");
        WriteLine($"i=3: expected c_3 = 0, calculated c_3 = {spline1.c[2]}, expected b_3 = 0, calculated b_3 = {spline1.b[2]}");
        WriteLine($"i=3: expected c_4 = 0, calculated c_4 = {spline1.c[3]}, expected b_4 = 0, calculated b_4 = {spline1.b[3]}");

        WriteLine("y=x");
        WriteLine($"i=1: expected c_1 = 0, calculated c_1 = {spline2.c[0]}, expected b_1 = 1, calculated b_1 = {spline2.b[0]}");
        WriteLine($"i=2: expected c_2 = 0, calculated c_2 = {spline2.c[1]}, expected b_2 = 1, calculated b_2 = {spline2.b[1]}");
        WriteLine($"i=3: expected c_3 = 0, calculated c_3 = {spline2.c[2]}, expected b_3 = 1, calculated b_3 = {spline2.b[2]}");
        WriteLine($"i=4: expected c_4 = 0, calculated c_4 = {spline2.c[3]}, expected b_4 = 1, calculated b_4 = {spline2.b[3]}");
        
        WriteLine("y=x^2");
        WriteLine($"i=1: expected c_1 = 1, calculated c_1 = {spline3.c[0]}, expected b_1 = 2, calculated b_1 = {spline3.b[0]}");
        WriteLine($"i=2: expected c_2 = 1, calculated c_2 = {spline3.c[1]}, expected b_2 = 4, calculated b_2 = {spline3.b[1]}");
        WriteLine($"i=3: expected c_3 = 1, calculated c_3 = {spline3.c[2]}, expected b_3 = 6, calculated b_3 = {spline3.b[2]}");
        WriteLine($"i=4: expected c_4 = 1, calculated c_4 = {spline3.c[3]}, expected b_4 = 8, calculated b_4 = {spline3.b[3]}");
    


    }


    public static double[] GenerateZPoints(double[] x, int numPointsPerInterval, double proximityFactor)
    {
        if (x.Length < 2 || numPointsPerInterval < 1)
        {
            throw new ArgumentException("Invalid input parameters.");
        }

        List<double> zList = new List<double>();

        for (int i = 0; i < x.Length - 1; i++)
        {
            double interval = (x[i + 1] - x[i]) / (numPointsPerInterval + 1);

            // Generate z-points proportionally within the interval
            for (int j = 1; j <= numPointsPerInterval; j++)
            {
                double z = x[i] + j * interval * proximityFactor;

                // Adjust z to ensure it's within the bounds of the interval
                z = Math.Max(z, x[i] + interval / 2.0);
                z = Math.Min(z, x[i + 1] - interval / 2.0);

                zList.Add(z);
            }
        }

        return zList.ToArray();
    }






    public static double[] Linspace(double start, double end, int numPoints)
    {
        if (numPoints < 2)
        {
            throw new ArgumentException("Invalid number of points.");
        }

        double[] linspace = new double[numPoints];
        double step = (end - start) / (numPoints - 1);

        for (int i = 0; i < numPoints; i++)
        {
            linspace[i] = start + i * step;
        }

        return linspace;
    }

    public static void WriteDataToFile(double[] x, double[] y, string filename)
    {
        string folderPath = "../data/"; // Replace with the actual folder path
        string filePath = Path.Combine(folderPath, filename);

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            for (int i = 0; i < x.Length; i++)
            {
                writer.WriteLine($"{x[i]} {y[i]}");
            }
        }
    }

    public static void WriteSplineDataToFiles(List<qspline> splines, vector x, vector[] yValues, vector z, string[] filenames)
    {
        string folderPath = "../data/";

        for (int i = 0; i < splines.Count; i++)
        {
            using (StreamWriter writer = new StreamWriter(Path.Combine(folderPath, filenames[i] + ".data")))
            {
                // Write column headers
                writer.WriteLine("x y z qspline int_qspline der_qspline");

                for (int j = 0; j < z.size; j++)
                {
                    double splineEval = j < z.size ? splines[i].evaluate(z[j]) : double.NaN;
                    double intEval = j < z.size ? splines[i].integral(z[j]) : double.NaN;
                    double derEval = j < z.size ? splines[i].derivative(z[j]) : double.NaN;

                    string xValue = j < x.size ? x[j].ToString() : double.NaN.ToString();
                    string yValue = i < yValues.Length && j < yValues[i].size ? yValues[i][j].ToString() : double.NaN.ToString();

                    string line = string.Format("{0} {1} {2} {3} {4} {5}", xValue, yValue, z[j], splineEval, intEval, derEval);
                    writer.WriteLine(line);
                }
            }
        }
    }






}