using System;
using static System.Console;
using static System.Math; 

public static class fit {

    public static (vector, matrix, vector) lsfit (Func<double,double>[] fs, vector x_data,vector y_data, vector dy){
        // fs has a general structure like this: var fs = new Func<double,double>[] { z => 1.0, z => z, z => z*z };
        // So the function we're fitting to is c1*fs[0] + c2*fs[1] + c3*fs[2] + ... = c1*1.0 + c2*z + c3*z^2 + ...
        //dy is uncertainties on y-data.

        int m= fs.Length;
        int n= x_data.size;

        matrix A= new matrix(n,m);
        vector b = new vector(y_data.size);

        //Creating matrix A and vector b. See eq. (14) from notes
        for(int i=0; i<n; i++){
            b[i]= y_data[i]/dy[i];

            for (int j=0; j<m; j++){
                A[i,j]=fs[j](x_data[i])/dy[i];
            }
        }

        var qrgs=new QRGS(A);
	    vector c = qrgs.solve(b); //Solves for c, in A*c=b
        //c is vector with equations

        //Calculating covariance matrix and uncertainties/errors for coefficients c
        matrix M = A.T * A;
        var qrgs2=new QRGS(M);
	    matrix Cov = qrgs2.inverse(M);
        vector c_err = new vector(c.size);
        for (int i = 0; i < c_err.size; i++)
        {
            c_err[i] = Sqrt(Cov[i, i]);
        }



        return (c,Cov,c_err);
    }


    public static (vector,vector,vector,double,double,double,double) FitExponential(vector x_data, vector y_data, vector dy, vector x_plot) {
        // Fit exponential data
        // Fitting to function of y(x)=a*exp(-λt)
        vector y_ln = new vector(y_data.size);
        vector dy_ln = new vector(dy.size);
        for (int i = 0; i < y_data.size; i++) {
            y_ln[i] = Log(y_data[i]);
            dy_ln[i] = dy[i] / y_data[i]; // error propagation for ln(y)
        }

        Func<double, double>[] fs_ln = { z => 1.0, z => z }; // Linear functions for ln(y)

        var fit_ln = lsfit(fs_ln, x_data, y_ln, dy_ln); // Use the lsfit constructor
        vector c_ln = fit_ln.Item1;
        matrix cov_ln = fit_ln.Item2;
        vector c_err_ln = fit_ln.Item3;

        // Convert linear fit back to exponential
        double a = Exp(c_ln[0]);
        double lambda = -c_ln[1];

        // Uncertainties using error propagation
        double a_err = c_err_ln[0] * a;
        double lambda_err = c_err_ln[1];

        vector y = new vector(x_plot.size);
        vector y_upper = new vector(x_plot.size); //Upper bound with uncertainties
        vector y_lower = new vector(x_plot.size); //Lower bound uncertainties
        for (int i = 0; i<x_plot.size; i++){
            y[i] = a*Exp(-lambda*x_plot[i]);
            y_upper[i] = Exp(c_ln[0]+c_err_ln[0])*Exp((c_ln[1]+c_err_ln[1])*x_plot[i]);
            // We have made linear fit to ln(y) = c[0] + c[1]*x
            // ln(y_upper) = (c[0] + c_err[0]) + (c[1] + c_err[1]) * x
            // ==> y_upper = exp(c[0] + c_err[0]) * exp((c[1] + c_err[1]) * x)

            y_lower[i] = Exp(c_ln[0]-c_err_ln[0])*Exp((c_ln[1]-c_err_ln[1])*x_plot[i]);
        }
        
        return (y,y_upper,y_lower,lambda,lambda_err,a,a_err);

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

}