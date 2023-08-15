using System;
using static System.Console;
using static System.Math;
using static cmath;

class main
{
    public static void Main(){
        //Creating objects (complex numbers)
        complex minusone = new complex(-1,0);
        complex I = new complex(0,1);

        //variables
        string Good = "The found value is close enough to the manually calculated value.";
        string Bad = "The found value is not close enough to the manually calculated value.";

        //Using complex library to do complex math operations on objects
        complex sqrtMinusone = sqrt(minusone);
        complex sqrtI = sqrt(I);
        complex expI = exp(I);
        complex expPiI = exp(PI*I);
        complex ItoI = I.pow(I);
        complex logI = log(I);
        complex sinIpi = sin(I*PI);

        // Printing results
        WriteLine($"sqrt(-1) is found to be= {sqrtMinusone} \nIt should be equal to: +1 and -1");
         if (sqrtMinusone.approx(I)) WriteLine(Good+"\n");
		else WriteLine(Bad+"\n");

        WriteLine($"sqrt(i) = {sqrtI} \nIt should be equal to: (1+i)/sqrt(2)=0.707(1+i)");
        if (sqrtI.approx(new complex(1/sqrt(2),1/sqrt(2)))) WriteLine(Good+"\n");
		else WriteLine(Bad+"\n");

        WriteLine($"e^i\nshould be cos(1) + i sin(1), which is approximately 0.54+0.84i\nis found to be {expI}");
        if (expI.approx(new complex(Cos(1),Sin(1)))) WriteLine(Good+"\n");
		else WriteLine(Bad+"\n");

        WriteLine($"e^(pi i)\nshould be -1\nis found to be {expPiI}");
        if (expPiI.approx(-1)) WriteLine(Good+"\n");
		else WriteLine(Bad+"\n");

        WriteLine($"i^i\nshould be e^(-pi/2), which is approximately 0.208\nis found to be {ItoI}");
        if (ItoI.approx(exp(-PI/2))) WriteLine(Good+"\n");
		else WriteLine(Bad+"\n");

        WriteLine($"ln(i)\nshould be i pi / 2, which is approximately 1.57i\nis found to be {logI}");
        if (logI.approx(new complex(0,PI/2))) WriteLine(Good+"\n");
		else WriteLine(Bad+"\n");

        WriteLine($"sin(i pi)\nshould be i*sinh(pi), which is approximately 11.54i\nis found to be {sinIpi}");
        if (sinIpi.approx(new complex(0,Sinh(PI)))) WriteLine(Good+"\n");
		else WriteLine(Bad+"\n");

    }
}
