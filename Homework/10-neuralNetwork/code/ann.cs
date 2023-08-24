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

}
