using static System.Math;
using System;

namespace special.functions {
    public static partial class sfuns
    {
        public static double erf(double x)
        {
            /// single precision error function (Abramowitz and Stegun, from Wikipedia)
            if(x<0) return -erf(-x);
            double[] a={0.254829592,-0.284496736,1.421413741,-1.453152027,1.061405429};
            double t=1/(1+0.3275911*x);
            double sum=t*(a[0]+t*(a[1]+t*(a[2]+t*(a[3]+t*a[4]))));/* the right thing */
            return 1-sum*Exp(-x*x);
        } 

        public static double erfIntegral(double z, double theta = 0.0001)
        { //Integral representation of erf.
            if (z < theta)
                return -erf(-z);
            else if (z <= 1)
            {
                Func<double, double> integrand = x => Exp(-x * x);
                return 2 / Sqrt(PI) * integration.integrate(integrand, theta, z);
            }
            else
            {
                Func<double, double> integrand = t => Exp(-Pow(z + (1 - t) / t, 2)) / (t * t);
                return 1 - 2 / Sqrt(PI) * integration.integrate(integrand, theta, 1);
            }
        }
    }
}

//Error function: returning y-value for a given x-value.