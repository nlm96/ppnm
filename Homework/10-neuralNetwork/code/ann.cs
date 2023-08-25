using System;
using static System.Math;
using static System.Console;

public class ann{
	public int n;
	public Func<double,double> f;
	public vector p;
	public int N;
	public ann(int n, Func<double,double> f){
	this.n=n;
	this.f=f;
	this.p=new vector(3*n);
	}//ann


	public double response(double x){
	double sum=0;

	for(int i=0; i<n;i++){
	double a=p[3*i+0];
	double b=p[3*i+1];
	double w=p[3*i+2];

	sum +=f((x-a)/b)*w;
	}
	return sum;
	}

	public void train(vector x, vector y){
		Func<vector,double> cost=C=>{
		p=C;
		double sum =0;
			for(int i=0; i<x.size;i++){
				sum+= Pow(response(x[i])-y[i],2);
			}
			return sum;
		};
		vector xstart=p;

		var (res,steps) = minimization.qnewton(cost,xstart,1e-5);
		N=steps;
		p=res;
	}



	public double d_response(double x)
    {
        double sum = 0;
        for (int i = 0; i < n; i++)
        {
            double a = p[3 * i + 0];
            double b = p[3 * i + 1];
            double w = p[3 * i + 2];
            sum += d_activationFunction((x - a) / b) * w / b;
        }
        return sum;
    }

    public double dd_response(double x)
	{
		double sum = 0;
		for (int i = 0; i < n; i++)
		{
			double a = p[3 * i + 0];
			double b = p[3 * i + 1];
			double w = p[3 * i + 2];
			
			// Compute the second derivative of the activation function over the scaled interval
			double secondDerivative = dd_activationFunction((x - a) / b);
			
			// Update the sum with the contribution from the current neuron, scaled by w / (b * b)
			sum += secondDerivative * w / (b * b);
		}
		return sum;
	}


    public double antiderivative_response(double x)
	{
		double sum = 0;
		for (int i = 0; i < n; i++)
		{
			double a = p[3 * i + 0];
			double b = p[3 * i + 1];
			double w = p[3 * i + 2];
			
			// Use the antiderivative function and scale by w * b
			sum += antiderivative_activationFunction((x - a) / b) * w * b;
		}
		return sum;
	}





	public double d_activationFunction(double x)
    {
        return (1 - 2 * x * x) * Exp(-x * x);
    }

    public double dd_activationFunction(double x)
    {
        return Exp(-x*x)*(4*x*x*x-6*x);
    }

    public double antiderivative_activationFunction(double x)
    {
        return -0.5 * Exp(-x * x);
    }




}
