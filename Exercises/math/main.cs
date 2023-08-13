using System;
using static System.Console;
using static System.Math;

class math{
    static void Main(){
        exercise1();
        exercise2();
        exercise3();
    }

    public static void exercise1(){
        
        WriteLine("Exercise 1");
         double sqrt2 = Sqrt(2.0);
         double powTwoOneFifth = Pow(2.0,1.0/5.0);
         double expPi = Exp(System.Math.PI);
         double piPowExp = Pow(PI,E);

        WriteLine($"sqrt(2) = {sqrt2}");
        WriteLine($"2^(1/5) = {powTwoOneFifth}");
        WriteLine($"exppi = {expPi}");
        WriteLine($"piexp = {piPowExp}");
        WriteLine($"sqrt2*sqrt2 = {sqrt2*sqrt2} (should be equal 2)");
        WriteLine($"(2^(1/5))^5 = {Pow(powTwoOneFifth,5)} (should be equal 2)");
        WriteLine($"ln(expPi) = {Log(expPi)} (should be equal {PI})");
        WriteLine($"log10(piPowExp) = {Log(piPowExp)/Log(PI)} (should be equal {E})");
    }

    public static void exercise2(){
        WriteLine($"\nExercise 2");
        WriteLine($"gamma(1)={sfuncs.gamma(1)}, should be 1");
        WriteLine($"gamma(2)={sfuncs.gamma(2)}, should be 1");
        WriteLine($"gamma(3)={sfuncs.gamma(3)}, should be 2");
        WriteLine($"gamma(10)={sfuncs.gamma(10)}, should be 362880");

    }

     static void exercise3(){
        WriteLine($"\nExercise 3");
        WriteLine($"lngamma(1) = {sfuncs.lngamma(1)}");
        WriteLine($"lngamma(2) = {sfuncs.lngamma(2)}");
        WriteLine($"lngamma(3) = {sfuncs.lngamma(3)}");
        WriteLine($"lngamma(10) = {sfuncs.lngamma(10)}");
        WriteLine($"Then we can get");
        WriteLine($"Exp(lngamma(1)) = {Exp(sfuncs.lngamma(1))}");
        WriteLine($"Exp(lngamma(2)) = {Exp(sfuncs.lngamma(2))}");
        WriteLine($"Exp(lngamma(3)) = {Exp(sfuncs.lngamma(3))}");
        WriteLine($"Exp(lngamma(10)) = {Exp(sfuncs.lngamma(10))}");

    }
}