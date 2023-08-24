using System;
using static System.Math;

public class minimization
{
    public static vector ComputeGradient(Func<vector, double> func, vector x0)
    {
        vector x = x0.copy();
        int dimension = x.size;
        vector gradient = new vector(dimension);
        double delta = Pow(2, -26);
        double fx = func(x);

        for (int i = 0; i < dimension; i++)
        {
            double dx = Abs(x[i]) * delta;
            if (Abs(x[i]) < Sqrt(delta)) { dx = delta; }

            x[i] += dx;
            gradient[i] = (func(x) - fx) / dx;
            x[i] -= dx;
        }

        return gradient;
    }

    public static (vector, int) qnewton(Func<vector, double> func, vector xStart, double accuracy)
    {
        double fx = func(xStart);
        vector gradientX = ComputeGradient(func, xStart);
        vector x = xStart.copy();
        matrix B = matrix.id(xStart.size);
        double delta = Pow(2, -26);
        int iterationCount = 0;

        while (iterationCount < 99999)
        {
            iterationCount++;
            vector deltaX = -B * gradientX;

            if (deltaX.norm() < delta * deltaX.norm()) { break; }
            if (gradientX.norm() < accuracy) { break; }

            vector z;
            double fz, lambda = 1;

            while (true)
            {
                z = x + deltaX * lambda;
                fz = func(z);

                if (fz < fx) { break; }
                if (lambda < delta)
                {
                    B.setid();
                    break;
                }
                lambda /= 2;
            }

            vector displacement = z - x;
            vector gradientZ = ComputeGradient(func, z);
            vector gradientChange = gradientZ - gradientX;
            vector displacementChange = displacement - B * gradientChange;
            double displacementChangeDotGradientChange = displacementChange.dot(gradientChange);

            if (Abs(displacementChangeDotGradientChange) > 1e-6)
            {
                B.update(displacementChange, displacementChange, 1 / displacementChangeDotGradientChange);
            }

            x = z;
            gradientX = gradientZ;
            fx = fz;
        }

        return (x, iterationCount);
    }
}
