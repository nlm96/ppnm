using System;
using System.IO;
using static System.Math;
using static System.Console;
using static matrix;
using static vector;


class main{
    static void Main(){

        //Part A: solving LinEq using QR-decomposition and gram-schmidt 

        WriteLine("Part A:\n \n");

        int n = 7; // Choose the size of the matrix
        int m = 5;
        int minValue =0;
        int maxValue = 20;
        
        // Generate a random tall matrix A
        matrix A = RandomMatrix(n, m, minValue, maxValue);
        //Generate random square matrix
        matrix A3 = RandomMatrix(m, m, minValue, maxValue);

        //Generate vector b
        vector b = RandomVector(m, minValue, maxValue);

        //-------- Printing and generating all matrices ----------------
        WriteLine("Printing matrices:");
        WriteLine("Matrix A:");
        A.print();   

        // Create an instance of QRGS using the constructor
        QRGS qrgs = new QRGS(A);
        
        matrix R = qrgs.R;
        WriteLine("Matrix R:");
        R.print();


        matrix Q = qrgs.Q;
        matrix identity = matrix.id(Q.size2);
        WriteLine("Identity matrix:");
        identity.print();


        WriteLine("Q matrix:");
        Q.print();
        matrix QT=Q.T;
        WriteLine("Transpose of Q, Q^T:");
        QT.print();


        matrix QTQ = QT*Q;
        WriteLine("Q^T*Q=");
        QTQ.print();

        // ------------- Testing decomposition ---------------
        WriteLine($"\n \n Tests:");

        WriteLine($"Is Q^TQ=I? {QTQ.approx(identity)}");

        matrix A2 = Q*R;
        WriteLine($"Is Q*R=A?: {A2.approx(A)}");

        bool isUpperTriangular = IsUpperTriangular(R);
        WriteLine($"Is R upper triangular? {isUpperTriangular}");


        // ----------- Testing solve method -------------
        WriteLine("\n \nSolving for x in linear system of equations of form Ax=b");
        WriteLine("Matrix A3:");
        A3.print();
        WriteLine("Vector b:");
        b.print();

        QRGS qrgs2 = new QRGS(A3);
        

        vector x = qrgs2.solve(b);
        WriteLine("x is given by:");
        x.print();


        vector Ax= A3*x;
        WriteLine("A*x =");
        Ax.print();
        WriteLine($"Is A*x=b?: {Ax.approx(b)}");




        // Part B) : Matrix inverse by Gram-Schmidt QR factorization
        WriteLine("\n \n \nPart B:\n \n");
        WriteLine("I will use the above square matrix A3:");
        A3.print();

        matrix R3 = qrgs2.R;
        WriteLine("R3=");
        R3.print();

        matrix Q3 = qrgs2.Q;
        WriteLine("Q3=");
        Q3.print();

        matrix Ainv = qrgs2.inverse(A3);
        WriteLine("inv(A3)=");
        Ainv.print();

        matrix I = Ainv*A3;
        WriteLine("inv(A3)*A3=");
        I.print();

        WriteLine($"Is inv(A3)*A3=I?: {I.approx(matrix.id(A3.size2))}");


    }

     static bool IsUpperTriangular(matrix mat){
            for (int i = 1; i < mat.size1; i++)
            {
                for (int j = 0; j < i; j++){
                if (Abs(mat[i, j]) > 1e-10) // tolerance
                    return false;
                }
            }
        return true;
        }

    private static Random rnd = new Random();

    public static matrix RandomMatrix(int rows, int cols, double minValue, double maxValue)
    {
        matrix mat = new matrix(rows, cols);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                double value = minValue + (maxValue - minValue) * rnd.NextDouble();
                mat[i, j] = value;
            }
        }
        return mat;
    }

    public static vector RandomVector(int size, double minValue, double maxValue)
    {
        vector vec = new vector(size);
        for (int i = 0; i < size; i++)
        {
            double value = minValue + (maxValue - minValue) * rnd.NextDouble();
            vec[i] = value;
        }
        return vec;
    }
}