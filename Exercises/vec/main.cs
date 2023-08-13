using System;
using static System.Math;
using static System.Console;
using static vec;

class Program
{
    static void Main()
    {
        // Create vec objects
        vec v1 = new vec(1, 2, 3);
        vec v2 = new vec(4, 5, 6);

        // Print the vectors
        v1.print("Vector v1: ");

        v2.print("\nVector v2: ");

        // Perform vector operations
        vec sum = v1 + v2;
        vec sub = v1 - v2;
        vec scaled = 2.5 * v1;
        double dotProduct = vec.dot(v1, v2);
        double dotProduct2 = v1.dot(v2);
        vec crossProduct = v1.cross(v2);
        vec crossProduct2 = vec.cross(v1,v2);
        double normv1 = v1.norm();


        // Print the results
        sum.print("\nSum of v1 and v2: ");

        sub.print("\nSubstraction of v1 and v2: ");
        scaled.print("\nScaled v1 (2.5 * v1):");
        WriteLine($"\nDot product of v1 and v2: {dotProduct}");
        WriteLine($"\nDot product of v1 and v2: {dotProduct2}");
        crossProduct.print("crossproduct (type1): ");
        crossProduct2.print("Crossproduct t2: ");
        WriteLine($"\nNorm of v1: {normv1}");


    }
}
