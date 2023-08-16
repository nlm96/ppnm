using System;
using System.Collections.Generic;
using static System.Math;

public class odesolver
{
    // Implements the embedded Runge-Kutta step with error estimation
    public static (vector yh, vector er) Rkstep12(
	Func<double,vector,vector> f, /* the f from dy/dx=f(x,y) */
	double x,                    /* the current value of the variable */
	vector y,                    /* the current value y(x) of the sought function */
	double h                     /* the step to be taken */
	)
    {
        // Calculate k0 and k1 using the embedded Runge-Kutta method
        vector k0 = f(x, y); /* embedded lower order formula (Euler) */
        vector k1 = f(x + h / 2, y + k0 * (h / 2)); /* higher order formula (midpoint) */
        
        // Estimate the new solution yh and the error er
        vector yh = y + k1 * h; /* y(x+h) estimate */
        vector er = (k1 - k0) * h; /* error estimate */
        return (yh, er);
    }


    // Implements the driver routine with path recording
    public static (genlist<double>,genlist<vector>) driver(
        Func<double,vector,vector> f, /* the f from dy/dx=f(x,y) */
        double a,                    /* the start-point a */
        vector ya,                   /* y(a) */
        double b,                    /* the end-point of the integration */
        double h=0.01,               /* initial step-size */
        double acc=0.01,             /* absolute accuracy goal */
        double eps=0.01              /* relative accuracy goal */
    )
    {
        // Check if a is greater than b
        if (a > b)
        {
            throw new SystemException("driver: a > b");
        }

        double x = a;
        vector y = ya.copy();
        
        // Initialize lists for recording path
        var xlist = new genlist<double>();
        xlist.add(x);
        var ylist = new genlist<vector>();
        ylist.add(y);

        do
        {
            // Check if the integration is complete
            if (x >= b)
            {
                return (xlist, ylist); /* job done */
            }

            // Adjust step size for the last step
            if (x + h > b)
            {
                h = b - x; /* last step should end at b */
            }
            
            // Compute the y estimate and error estimate using Rkstep12
            var (yh, erv) = Rkstep12(f, x, y, h);
            
            // Calculate the tolerance based on absolute and relative accuracy goals
            double tol = Max(acc, yh.norm() * eps) * Sqrt(h / (b - a));
            double err = erv.norm();
            
            // Accept the step if error is within tolerance
            if (err <= tol)
            {
                x += h;
                y = yh;
                xlist.add(x);
                ylist.add(y);
            }
            // Adjust step size based on the error estimate and tolerance
            h *= Min(Pow(tol / err, 0.25) * 0.95, 2); // readjust stepsize
        } while (true);
    }

    // Improved driver routine with optional path recording
    public static (genlist<double> xlist, genlist<vector> ylist) AdaptiveDriver(Func<double, vector, vector> f, double a,
        vector ya, double b, double h = 0.01, double acc = 0.01, double eps = 0.01, genlist<double> xlist = null, genlist<vector> ylist = null)
    {
        double x = a;
        vector y = ya.copy();
        
        do
        {
            double[] tol = new double[y.size];
            double[] err = new double[y.size];
            
            // Check if the integration is complete
            if (x >= b)
            {
                // Return path or solution at end-point based on provided lists
                if (xlist == null && ylist == null)
                {
                    xlist = new genlist<double>();
                    xlist.add(x);
                    ylist = new genlist<vector>();
                    ylist.add(y);
                    return (xlist, ylist);
                }
                else
                {
                    return (xlist, ylist);
                }
            }
            // Adjust step size for the last step
            if (x + h > b)
            {
                h = b - x;
            }
            
            // Compute the y estimate and error estimate using Rkstep12
            var (yh, erv) = Rkstep12(f, x, y, h);
            
            // Calculate tolerances for each component of y
            for (int i = 0; i < y.size; i++)
            {
                tol[i] = Max(acc, yh.norm() * eps) * Sqrt(h / (b - a));
            }
            bool ok = true;
            for (int i = 0; i < y.size; i++)
            {
                // Check if each component's error is within its tolerance
                if (!(err[i] < tol[i]))
                {
                    ok = false;
                }
            }
            if (ok)
            {
                x += h;
                y = yh;
                // Add to path lists if provided
                if (xlist != null & ylist != null)
                {
                    xlist.add(x);
                    ylist.add(y);
                }
                // Adjust step size based on the error estimates and tolerances
                double factor = tol[0] / Abs(erv[0]);
                for (int i = 1; i < y.size; i++)
                {
                    factor = Min(factor, tol[i] / Abs(erv[i]));
                }
                h *= Min(Pow(factor, 0.25) * 0.95, 2);
            }
        } while (true);
    }
}

/* Usage Guide:
     * This class provides methods for numerically solving systems of first-order
     * ordinary differential equations (ODEs) using the embedded Runge-Kutta step
     * with error estimation. Follow these steps to use the methods effectively.
     *
     * 1. Define Your ODE System:
     *    - Define a function 'f' representing the system of ODEs dy/dx = f(x, y).
     *    - For each equation, use an entry in the 'y' vector for the corresponding variable.
     *    - For example, u'' = -u can be defined as:
     *      Func<double, vector, vector> harmonicOscillator = (x, y) => new vector(y[1], -y[0]);
     *
     * 2. Choose a Method:
     *    - Decide between the 'Driver' or 'AdaptiveDriver' method based on your needs.
     *    - improved driver:
                Quite often one is only interested in the solution-vector at the end-point of the integration interval. 
                In this case recording the path is a waste of time. 
                To address this case, modify the interface to your driver such that the driver records the path 
                only if the user provides initialized generic lists for both "x" and "y". 
                Otherwise, that is, if the lists are "null", the driver only returns the "y" at the end-point.
     *
     * 3. Call the Method:
     *    - Call the selected method with the function 'f', initial values 'ya', and integration bounds.
     *    - For example, to solve u'' = -u from x = 0 to x = 10 with u(0) = 1 and u'(0) = 0:
     *      (genlist<double> xlist, genlist<vector> ylist) = Driver(harmonicOscillator, 0, new vector(1, 0), 10);
     *
     * 4. Extract Results:
     *    - The returned lists 'xlist' and 'ylist' contain the values of 'x' and 'y' at each integration step.
     *    - Use these lists to plot or analyze the solution of the ODE system.
     *
     * That's it! Follow these steps to effectively use the provided methods for ODE solving.
     */