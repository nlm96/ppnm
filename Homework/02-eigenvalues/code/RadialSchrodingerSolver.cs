using static System.Math;
using static System.Console;

public class RadialSchrodingerSolver
{
    public static matrix BuildHamiltonian(double rmax, double dr)
    {
        int npoints = (int)(rmax/dr)-1;
        vector r = new vector(npoints);
        for(int i=0;i<npoints;i++){
            r[i]=dr*(i+1);
        }
        matrix H = new matrix(npoints,npoints);
        for(int i=0;i<npoints-1;i++){
            H[i,i]  =-2;
            H[i,i+1]= 1;
            H[i+1,i]= 1;
        }
        H[npoints-1,npoints-1]=-2;
        matrix.scale(H,-0.5/dr/dr);
        for(int i=0;i<npoints;i++){
            H[i,i]+=-1/r[i];
        }

        return H;
        
    }

    public static (vector, matrix) DiagonalizeHamiltonian(matrix H)
    {
        int n = H.size1;
        matrix eigenvecs = new matrix(n, n);
        matrix H_copy = H.copy();

        jacobi.cyclic(H_copy, eigenvecs);

        // Get the eigenvalues and eigenvectors from H_copy and V
        vector eigenvals = new vector(n);

        for (int i = 0; i < n; i++)
        {
            eigenvals[i] = H_copy[i, i];
        }
        return (eigenvals, eigenvecs);
    }   


    public static vector Eigenfunction(matrix eigenvecs, int k, double dr)
    {
        // Returns the k'th eigenfunc f_k(r).

        int n = eigenvecs.size1;
        vector eigenfunc = new vector(n);

        // Extract the k'th column vector from eigenvecs
        vector eigenvec_k = new vector(n);
        for (int i = 0; i < n; i++)
        {
            eigenvec_k[i] = eigenvecs[i, k];
        }

        // Calculate the square sum of the elements in eigenvec_k
        double sumSquares = 0.0;
        for (int i = 0; i < n; i++)
        {
            sumSquares += eigenvec_k[i] * eigenvec_k[i];
        }

        // Calculate the normalization constant
        double normalizationConst = 1.0 / Sqrt(dr * sumSquares);

        // Normalize the eigenfunction and apply the normalization constant
        
        eigenfunc = normalizationConst * eigenvec_k;

        return eigenfunc;
    }
}
