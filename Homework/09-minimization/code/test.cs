
/*

using System;
using static System.Math;

public class minimization
{





















    



    public static (vector,int) qnewton(
        Func<vector, double> f, /* objective function */
        vector start, /* starting point */
        double acc=1e-4, /* accuracy goal, on exit |gradient| should be < acc */
        int maxIter = 50000 /*Max number of iterations*/
        )
    {
            int nsteps=0;
            int n = start.size;
            matrix B = new matrix(n, n); // Initialize the Hessian approximation
            B.set_identity();

            vector dx = new vector(n);
            vector s = new vector(n);
            vector u = new vector(n);
            vector y = new vector(n);

            vector x = start.copy();
            vector gx = gradient(f, x);
            double lambda = 1.0;

            while (gx.norm() > acc && nsteps<maxIter)
            {   nsteps++;
                lambda = 1.0;
                dx = -B * gx; // Calculate the Newton's step

                while(true){
                    s = lambda*dx;

                    if (f(x + lambda * dx) < f(x))
                    { //accept the step and update B:
                    x += lambda*dx;

                    s = lambda*dx;
                    y = gradient(f, x + s) - gx;
                    u = s - B*y;
                
                    SR1Update(B,u,y);
                    
                    break;

                    }
                
                    lambda = lambda/2; // Backtracking line search
                    

                    if(lambda<1.0/Pow(2,16)){
                        //accept the step and reset B:

                        x += lambda*dx;
                        B.set_identity();
                        break;
                    }
                }
            }

            return (x,nsteps);
    }

    //private static vector gradient(Func<vector, double> f, vector x)
    {
        int n = x.size;
        vector grad = new vector(n);
        vector dx = new vector(n);

        for (int i = 0; i < n; i++)
        {
            dx[i] = Pow(2,-26) * Abs(x[i]);
            vector x_modified = x.copy();
            x_modified[i] += dx[i];
            grad[i] = (f(x_modified) - f(x)) / dx[i];
        }

        return grad;
    }

    public static matrix SR1Update(matrix B, vector u, vector y) {
        // Calculate the outer product of vectors u and u^T
        matrix uuT = matrix.outer(u, u); //u*u^T

        // Calculate the denominator u^T y
        double denominator = u.dot(y);

        // Check if the denominator is not too small to avoid division by close-to-zero values
        double epsilon = 1e-6;
        if (Math.Abs(denominator) > epsilon) {
            // Calculate the update matrix delta B
            matrix deltaB = uuT / denominator; //eq 18

            // Apply the update matrix to B
            B += deltaB;
        }

        return B;
    }
    


}

*/