using static matrix;
using static vector;


public class QRGS{
    public matrix Q,R;
    public int m,n;

    //---- Decomposes matrix A to Q,R (A=QR) with Q orthogonal and R upper triangular m x m matrix
    public QRGS(matrix A){
   
        n=A.size1;
        m=A.size2;
        Q=A.copy();
        R=new matrix(m,m);

        /* orthogonalize Q and fill-in R */
        //QR Decomposition by Gram-Schmidt - algorithm used from LINEQ PDF
        for ( int i =0;i<m; i++){
            R[i,i]=Q[i].norm(); /* Q[ i ] points to the i−th columb */
            Q[i]/=R[i,i];
            
            for (int j=i +1;j<m; j++){
                R[i,j]=Q[i].dot(Q[j]);
                Q[j]-=Q[i]*R[i,j] ; 
            } 
        }
    }

    //Solves x for a linear equation system Ax=b.
    public vector solve(vector b){  
    
    vector x = Q.T * b;

        //Back substitution algorithm from PDF (eq 4, U=R and c=Q^T*b)
        for ( int i=x.size-1; i >=0; i--){
            double sum=0;
        
            for (int k=i +1; k<x.size; k++) {
                sum+=R[i,k] * x[k] ;
            }
            x[i]=(x[i]-sum)/R[i,i] ;  
        }

    return x;
    
    }


    public double det(){ 

    // cf. note eq. 44; Determinant of A can be calculated as product of diagonal elements of R-matrix.

        double determinant = 1.0;

        for (int i = 0; i < R.size1; i++)
        {
            determinant *= R[i, i]; // Multiply the diagonal element
        }

        return determinant;

    }


    public matrix inverse(matrix A){

        matrix inv = new matrix(A.size1, A.size2);

        for (int i = 0; i < A.size2; i++){
            vector e = new vector(A.size1);
            e[i] = 1;
            vector x = solve(e);
            
            for (int j = 0; j < A.size1; j++){
                inv[j, i] = x[j];
            }
        }

    return inv;
    }


}