using System;
using static System.Console;
using static System.Math; 

public class LinearSpline{

    public static double linterp(double[] x, double[] y, double z){
        int i=binsearch(x,z);
        double dx=x[i+1]-x[i]; if(!(dx>0)) throw new Exception("uups...");
        double dy=y[i+1]-y[i];
        return y[i]+dy/dx*(z-x[i]);
        // Calculate and return the linearly interpolated value at z
        // The linear interpolation formula: y = y_i + p_i * (z - x_i)
        // where p_i is the slope dy/dx within the interval
        // So (x,y) are lists with data points, and z is a x-value where data gets approximated by linear function by interpolation
        }


    public static int binsearch(double[] x, double z)
	{/* locates the interval for z by bisection */ // Find the index of the interval where z lies using binary search
        if(!(x[0]<=z && z<=x[x.Length-1])) throw new Exception("binsearch: bad z");
        int i=0, j=x.Length-1;
        while(j-i>1){
            int mid=(i+j)/2;
            if(z>x[mid]) i=mid; else j=mid;
            }
        return i;
	}

    public static double linterpInteg(double[] x, double[] y, double z) {
        int index = binsearch(x, z);  // Find the index of the interval where z lies
        double integral = 0;
        double[] delta_x = new double[x.Length - 1];  // Array to store ∆x values
        double[] delta_y = new double[x.Length - 1];  // Array to store ∆y values
        double[] p_values = new double[x.Length - 1];   // Array to store p_i values

        // Calculate ∆x, ∆y, and p_i for each interval
        for (int j = 0; j < x.Length - 1; j++) {
            delta_x[j] = x[j + 1] - x[j];  // ∆x_j = x_(j+1) - x_j
            if (!(delta_x[j] > 0)) throw new Exception("∆x_i not larger than 0");
            delta_y[j] = y[j + 1] - y[j];  // ∆y_j = y_(j+1) - y_j
            p_values[j] = delta_y[j] / delta_x[j];     // p_j = ∆y_j / ∆x_j
        }

        // Calculate the integral using the linear spline formula
        for (int k = 0; k <= index; k++) {
            if (k != index) {
                // For intervals before z: Add the contribution of y_k * ∆x_k + p_k * ∆x_k^2 / 2
                integral += y[k] * delta_x[k] + p_values[k] * delta_x[k] * delta_x[k] / 2;
            } 
            else {
                // For the interval containing z: Add the contribution of y_k * (z - x_k) + p_k * (z - x_k)^2 / 2
                integral += y[k] * (z - x[index]) + p_values[k] * (z - x[index]) * (z - x[index]) / 2;
            }
        }

        return integral;
    }   



}