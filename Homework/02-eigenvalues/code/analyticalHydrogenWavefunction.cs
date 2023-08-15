using System;
using static System.Math;

//Calculates the analytical reduced radial S-wave function
public class AnalyticalHydrogenWavefunction
{
    public static vector AnalyticalWavefunction(int n, double dr, double rmax)
    {
        int npoints = (int)(rmax / dr);
        vector r = new vector(npoints);
        vector f = new vector(npoints);
        
        for (int i = 0; i < npoints; i++)
        {
            r[i] = dr * (i + 1);
            f[i] = CalculateAnalyticalWavefunction(n, r[i]);
        }
        
        return f;
    }

    private static double CalculateAnalyticalWavefunction(int n, double r)
    {
        
        if (n == 1)
        {
            return  (r * 2 * Exp(-r));
        }
        else if (n == 2)
        {
            return r*(1.0 / Sqrt(2)) * (1 - r /2) * Exp(-r /2 );
        }
        else if (n == 3)
        {
            return r*(2.0 / (81 * Sqrt(3))) * (27 - 18*r + 2*r*r) * Exp(-r / (3));
        }
        else
        {
            throw new ArgumentException("Invalid value of n. Only n = 1, 2, 3 supported.");
        }
    }
}
