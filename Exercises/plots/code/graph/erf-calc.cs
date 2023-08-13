using static System.Math;
using static System.Console;
using System;
using special.functions;
class main{
    static void Main(){
        double dx = 1.0/64, shift = dx/2;
        for (double x = 0 + shift; x<=5; x+=dx){
            WriteLine($"{x} {sfuns.erf(x)}");
        }
    }
}