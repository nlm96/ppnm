//This code is made by Kevin Graversen
using System;
using static System.Console;
using static System.Math;

class epsilon{
    static void Main(){
        exercise1();
        exercise2();
        exercise3();
        exercise4();
    }

    static void exercise1(){
        WriteLine("Exercise 1");
        int i = 0;
        while(i+1>i) i++;
        WriteLine($"My max int is {i}");
        WriteLine($"The max int fron int.MaxValue is {int.MaxValue}\n");
        
        int ni = 0;
        while(ni-1<ni) ni--;
        WriteLine($"My min int is {ni}");
        WriteLine($"The min int from int.MinValue is {int.MinValue}");
    }

    static void exercise2(){
        WriteLine("\nExercise 2");
        double x = 1;
        while(x+1 != 1) x/=2;
        x*=2;

        float y = 1F;
        while ((float)(1F + y) != 1F ) y /= 2F;
        y *= 2F;

        WriteLine($"x is {x} and should be {System.Math.Pow(2,-52)}");
        WriteLine($"x/2+1 results in {x/2+1}");
        WriteLine($"y is {y} and should be {System.Math.Pow(2,-23)}");
        WriteLine($"y/2+1 results in {y/2F+1F}");
    }

    static void exercise3(){
        WriteLine("\nExercise 3");
        
        int n=(int)1e6;
        double epsilon=Pow(2,-52);
        double tiny=epsilon/2;
        double sumA=0,sumB=0;

        sumA+=1; 
        for(int i=0;i<n;i++){sumA+=tiny;}
        WriteLine($"sumA-1 = {sumA-1:e} should be {n*tiny:e}");

        for(int i=0;i<n;i++){sumB+=tiny;} 
        sumB+=1;
        WriteLine($"sumB-1 = {sumB-1:e} should be {n*tiny:e}");
    
        WriteLine("The difference is from the binary representation of both the numbers.");
        WriteLine("The value tiny is smaller than the possible representable number in double-precision, so sumA and sumB are subject to rounding errors. ");
        WriteLine("When tiny is added to 1, the resulting sum is rounded to 1 due to the limited precision of the floating-point system.");
    }

    static void exercise4(){       
        WriteLine($"\nExercise 4");
        WriteLine($"approx(a=1e-10, b=1.1e-10) = {approx(1e-10, 1.1e-10)}");
        WriteLine($"approx(a=1e-9, b=1.1e-9) = {approx(1e-9, 1.1e-9)}");
    }
    static bool approx(double a, double b, double tau = 1e-9, double epsilon = 1e-9){
        // tau = absolute accuracy, epsilon = relative accuracy
            if(Abs(a-b) < tau | Abs(a-b)/(Abs(a)+Abs(b)) < epsilon) return true;
            return false;
        }
}

