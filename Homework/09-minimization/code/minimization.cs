using System;
using static System.Math;
using System.Collections.Generic;

public class minimization
{
    public static vector FitBreitWigner(
    Func<vector, double> fittedFunction,
    vector initialGuesses,
    List<double> xValues,
    List<double> yValues,
    List<double> yErrors,
    double accuracy
){
    Func<vector, double> deviationFunction = parameters => {
        double deviationSum = 0;
        vector parametersVector = new vector(initialGuesses.size + 1);
        for(int i = 0; i < xValues.Count; i++){
            parametersVector[0] = xValues[i];
            for(int j = 0; j < initialGuesses.size; j++){
                parametersVector[j+1] = parameters[j];
            }
            double deviation = (fittedFunction(parametersVector) - yValues[i]) / yErrors[i];
            deviationSum += Pow(deviation, 2);
        }
        return deviationSum;
    };
    
    // D(m,Γ,A) = Σi[(F(Ei|m,Γ,A) - σi) / Δσi]^2
    vector fittedParameters = qnewton(deviationFunction, initialGuesses, accuracy).Item1;
    return fittedParameters;
}




   public static (vector,int) qnewton(Func<vector,double> f, vector start, double acc = 1e-4){
        int n = start.size;
        vector x = start.copy();
        vector grad = new vector(n);
        vector deltax = new vector(n);
        vector s = new vector(n);
        vector u = new vector(n);
        vector y = new vector(n);
        matrix deltaB = new matrix(n,n);
        matrix B = new matrix(n,n);
        B.set_identity();

        double lambda = 1.0;
        grad = gradient(f, x);
        int nsteps=0;

        while(grad.norm() > acc){
            nsteps++;
            deltax = -B*grad;
            lambda = 1.0;
            while(true){
                s = lambda*deltax;
                if (f(x+s) < f(x)){ // accept step and update B
                    x = x + s;
                    vector oldGrad = grad;
                    grad = gradient(f, x);
                    // Update B
                    y = grad - oldGrad;
                    u = s - B*y;
                    // deltaB * y = u  =>  deltaB * y * u = u * u 
                    deltaB = matrix.outer(u,u)/(u.dot(y));
                    B += deltaB;
                    break;
                }
                lambda = lambda/2;
                if (lambda < 1.0/Pow(2,16)){ // accept step and reset B
                    x = x + s;
                    grad = gradient(f, x);
                    B.set_identity();
                    break;
                }
            }
        }
        return (x,nsteps);
    }

    
    static vector gradient(Func<vector,double> f, vector x){
        int dim = x.size;
        vector grad = new vector(dim);
        vector newx = x.copy();
        for(int i = 0; i<dim; i++){
            double dx = Abs(x[i])*Pow(2,-26);
            newx[i] = x[i] + dx;
            grad[i] = (f(newx) - f(x))/dx;
        }
        return grad;
    }


}

