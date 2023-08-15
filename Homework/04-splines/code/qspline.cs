using System;

public class qspline
{
    public vector x, y, b, c;

    public qspline(vector xs, vector ys)
    {
        // Copy input data for x and y
        x = xs.copy();
        y = ys.copy();
        int n = xs.size; // Number of data points

        vector dx = new vector(n - 1); // Differences between adjacent x points
        vector p = new vector(n - 1); // Slope of the linear interpolants

        // Calculate differences (dx) and slopes (p) between adjacent x points
        for (int i = 0; i < n - 1; i++)
        {
            dx[i] = x[i + 1] - x[i];
            p[i] = (y[i + 1] - y[i]) / dx[i];
        }

        c = new vector(n - 1); // Coefficients for the quadratic term
        b = new vector(n - 1); // Coefficients for the linear term

        c[0] = 0; // Initialize the first coefficient (recursion)
        // Forward recursion to calculate coefficients c
        for (int i = 0; i < n - 2; i++)
            c[i + 1] = (p[i + 1] - p[i] - c[i] * dx[i]) / dx[i + 1]; //eq 13
        c[n - 2] /= 2; // Adjust the last coefficient

        // Backward recursion to adjust coefficients c
        for (int i = n - 3; i >= 0; i--)
            c[i] = (p[i + 1] - p[i] - c[i + 1] * dx[i + 1]) / dx[i]; //eq 14

        // Calculate coefficients b based on slopes (p) and coefficients c
        for (int i = 0; i < n - 1; i++)
            b[i] = p[i] - c[i] * dx[i];
    }

    public double evaluate(double z)
    {
        if (z < x[0] || z > x[x.size - 1])
            throw new ArgumentException("z is outside the interpolation range");

        int i = binsearch(x, z); // Find the interval index for z
        double h = z - x[i]; // Calculate the difference
        // Evaluate the quadratic spline at z using coefficients b and c
        return y[i] + h * (b[i] + h * c[i]);
    }

    public double derivative(double z)
    {
        if (z < x[0] || z > x[x.size - 1])
            throw new ArgumentException("z is outside the interpolation range");

        int i = binsearch(x, z); // Find the interval index for z
        double h = z - x[i]; // Calculate the difference
        // Evaluate the derivative of the quadratic spline at z using coefficient b and c
        return b[i] + 2 * h * c[i];
    }

    public double integral(double z)
    {
        if (z < x[0] || z > x[x.size - 1])
            throw new ArgumentException("z is outside the interpolation range");

        int i = binsearch(x, z); // Find the interval index for z
        double integral = 0;
        // Calculate the integral up to the interval containing z
        for (int j = 0; j < i; j++)
        {
            double dx = x[j + 1] - x[j];
            integral += y[j] * dx + b[j] * dx * dx / 2 + c[j] * dx * dx * dx / 3;
        }
        double h = z - x[i]; // Calculate the difference within the interval
        // Add the contribution of the quadratic spline within the interval
        integral += y[i] * h + b[i] * h * h / 2 + c[i] * h * h * h / 3;
        return integral;
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
}
