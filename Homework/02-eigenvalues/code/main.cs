using System;
using System.IO;
using static System.Math;
using static System.Console;
using static matrix;
using static vector;
class main{

    public static void Main(string[] args){

        // Parse command line arguments
        
        double rmax = 30; // Default values
        double dr = 0.1;
        
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "-rmax" && i + 1 < args.Length){
                rmax = double.Parse(args[i + 1]);
                }
            
            if (args[i] == "-dr" && i + 1 < args.Length){
                dr = double.Parse(args[i + 1]);
            }
        }
        
        partA();

        //Part B
        WriteLine("\n\nPart B:");
        convergence_dr();
        convergence_rmax();
        partB(rmax,dr);
    }

    static void partA(){

        int n = 8;
        matrix A = RandomSymmetricMatrix(n,0,20);

        matrix D = A.copy(); //I use this matrix as replacement for A in cyclic method, since it updates D to a diagonal matrix with eigenvalues.
        matrix V = new matrix(n,n);

        jacobi.cyclic(D,V);

        WriteLine("Random symmetric matrix A=");
        A.print();
        
        WriteLine("\nMatrix with eigenvectors V=");
        V.print();

        WriteLine("\nDiagonal matrix with eigenvalues D=");
        D.print();
        
        matrix VTAV= V.T * A * V;
        WriteLine("\nV^T*A*T is=");
        VTAV.print();

        WriteLine($"\nIs V^T*A*T=D ? {VTAV.approx(D)}");

        matrix VDVT = V*D*V.T;
        WriteLine("\nV*D*V^T is=");
        VDVT.print();

        WriteLine($"\nIs V*D*V^T=A? {VDVT.approx(A)}");

        matrix VTV = V.T * V;
        WriteLine("\nV^T*V is=");
        VTV.print();

        WriteLine($"\n Is V orthogonal? is V^T*V=I? {VTV.approx(matrix.id(n))}");


    }

    static void partB(double rmax, double dr){
        

        int n_max=3; //Max principal quantum number of reduced radial S-wavefuncs I want to calculate.
        
        //Creating vector r containing each point r_i.
        int npoints = (int)(rmax/dr)-1;
        vector r = new vector(npoints);
        for(int i=0;i<npoints;i++){
            r[i]=dr*(i+1);
        }


        // Build Hamiltonian matrix
        matrix H = RadialSchrodingerSolver.BuildHamiltonian(rmax, dr);

        // Diagonalize Hamiltonian matrix
        var (eigenvals, eigenvecs) = RadialSchrodingerSolver.DiagonalizeHamiltonian(H);

        // Initialize arrays to store eigenfunctions and squared eigenfunctions
        vector[] eigenfuncs = new vector[n_max];
        vector[] squaredEigenfuncs = new vector[n_max];
        vector[] AnalyticalEigenfuncs = new vector[n_max ];
        vector[] squaredAnalyticalEigenfuncs = new vector[n_max ];

        // Storing calculated eigenfunctions with increasing principal quantum numbers
        for (int n = 1; n <= n_max; n++)
        {
            eigenfuncs[n-1] = RadialSchrodingerSolver.Eigenfunction(eigenvecs, n-1, dr);
            
            // Square the vector elements manually
            squaredEigenfuncs[n-1] = new vector(eigenfuncs[n-1].size);
            for (int i = 0; i < eigenfuncs[n-1].size; i++)
            {
                squaredEigenfuncs[n-1][i] = eigenfuncs[n-1][i] * eigenfuncs[n-1][i];
            }
        }

        // Storing analytical eigenfuncs
        for (int n = 1; n <= n_max; n++)
        {
            AnalyticalEigenfuncs[n-1] = AnalyticalHydrogenWavefunction.AnalyticalWavefunction(n, dr, rmax);
            
            // Square the vector elements manually for analytical eigenfuncs
            squaredAnalyticalEigenfuncs[n-1] = new vector(AnalyticalEigenfuncs[n-1].size);
            for (int i = 0; i < AnalyticalEigenfuncs[n-1].size; i++)
            {
                squaredAnalyticalEigenfuncs[n-1][i] = AnalyticalEigenfuncs[n-1][i] * AnalyticalEigenfuncs[n-1][i];
            }
        }

        WriteEigenfunctionsToFile(r, squaredEigenfuncs, "eigenfunctions.data");
        WriteEigenfunctionsToFile(r, squaredAnalyticalEigenfuncs, "analytical_eigenfunctions.data");
        WriteLine("The eigenfunctions are plotted in eigenfunctions.svg in plots folder");
    
        
    }


    static void convergence_dr()
    {
        WriteLine("Convergence plot for dr (with fixed Rmax) is plotted in convergence-dr.svg in plots folder");
        var outfile = new StreamWriter("../data/convergence-dr.data");

        float rmax = 20;

        for (double dr = 3 + 1/32; dr > 0.0; dr -= 1.0/16)
        {
            matrix H = RadialSchrodingerSolver.BuildHamiltonian(rmax, dr);
            var (eigenvals, eigenvecs) = RadialSchrodingerSolver.DiagonalizeHamiltonian(H);

            outfile.WriteLine($"{dr} {eigenvals[0]}");
            outfile.Flush();
        }

        // Manually add the data point for dr = 0.04
        double special_dr = 0.03;
        matrix special_H = RadialSchrodingerSolver.BuildHamiltonian(rmax, special_dr);
        var (special_eigenvals, special_eigenvecs) = RadialSchrodingerSolver.DiagonalizeHamiltonian(special_H);
        outfile.WriteLine($"{special_dr} {special_eigenvals[0]}");
        outfile.Flush();

        outfile.Close();
    }


    static void convergence_rmax(){
        WriteLine("Convergence plot for rmax (with fixed dr) is plotted in convergence-rmax.svg in plots folder");
        var outfile = new StreamWriter("../data/convergence-rmax.data");
        for (double rmax = 2+1/32; rmax < 20.0; rmax +=1.0/16){
            float dr = 0.2F;
            // Build Hamiltonian matrix
            matrix H = RadialSchrodingerSolver.BuildHamiltonian(rmax, dr); 
            var (eigenvals, eigenvecs) = RadialSchrodingerSolver.DiagonalizeHamiltonian(H);

            //Console.WriteLine($"dr: {dr}, rmax: {rmax}, eigenvals[0]: {eigenvals[0]}");

            outfile.WriteLine($"{rmax} {eigenvals[0]}");
        }
        outfile.Close();
    }


    static void WriteEigenfunctionsToFile(double[] r, vector[] eigenfuncs, string filename)
    {
        string folderPath = "../data/"; // Replace with your desired folder path
        string filePath = Path.Combine(folderPath, filename);

        using (var outfile = new StreamWriter(filePath))
        {
            // Write the column headers
            outfile.Write("r");
            for (int n = 0; n < eigenfuncs.Length; n++)
            {
                outfile.Write($" R_{n}");
            }
            outfile.WriteLine();

            // Write the data rows
            for (int i = 0; i < r.Length; i++)
            {
                outfile.Write($"{r[i]}");
                for (int n = 0; n < eigenfuncs.Length; n++)
                {
                    outfile.Write($" {eigenfuncs[n][i]}");
                }
                outfile.WriteLine();
            }
        }
    }




    public static matrix RandomSymmetricMatrix(int size, int minValue, int maxValue)
    {
        matrix mat = new matrix(size, size);
        Random rnd = new Random();

        for (int i = 0; i < size; i++)
        {
            for (int j = i; j < size; j++) // Only fill upper triangle
            {
                int value = rnd.Next(minValue, maxValue + 1);
                mat[i, j] = value;
                mat[j, i] = value; // Set the symmetric element
            }
        }
        return mat;
    }

}