using System;
using System.Threading;
using static System.Console;
using static System.Math; 
using System.IO;
using System.Collections.Generic;


class main
{

    static void Main()
    {
        partA();
        partB();
        WriteLine("\n \nPart (c):");
        WriteLine("\nThe three body problem is solved an plotted in threebody.gif");
        threebodyproblem();
    }


    static void partA(){
        WriteLine("Part (A):");
       
        
        harm_osc();
        damped_osc();

    }


    static void partB(){
        WriteLine("\n\nPart (B):");

        LotkaVolterra();
    }



    static void harm_osc(){
        string folderPath = "../data/"; // Update with the actual folder path
        string filename = "harmonic_oscillator.data";
        
        // Harmonic oscillator
        // Update with the desired filename

        // Define the ODE as a function
        Func<double, vector, vector> harmonicOscillator = (x, y) =>
        {
            return new vector(y[1], -y[0]); // u'' = -u => if u'=y_2 and u=y_1 => y1' = y2, y2 = -y1
        };

        // Initial conditions
        double a_harm = 0; // Start point
        vector ya_harm = new vector(1, 0); // Initial values: u(0) = 1, u'(0) = 0
        double b_harm = 10; // End point

        // Solve the ODE using the Driver routine
        var solution_harm = odesolver.driver(harmonicOscillator, a_harm, ya_harm, b_harm);
        
        // Save the solution to a .data file
        Writefile(folderPath, filename, solution_harm.Item1, solution_harm.Item2);

        WriteLine($"The solution to the harmonic osciallator u'' = -u with initial values: u(0) = 1, u'(0) = 0 is plotted in {filename}.svg");

    }


    static void damped_osc(){
        

        Func<double, vector, vector> pend = (t, y) =>
        {
            // Damped oscillator
            double b = 0.25;
            double c = 5.0;

            //theta''(t) + b*theta'(t) + c*sin(theta(t)) = 0
            // ==> theta'(t) = omega(t)
                  //omega'(t) = -b*omega(t) - c*sin(theta(t))

            double theta = y[0];
            double omega = y[1];
            return new vector(omega, -b * omega - c * Sin(theta));
        };

        vector y0 = new vector(PI - 0.1, 0.0);

        double a_damped = 0;
        double b_integral = 10;
        double h = 0.1;

        var (xlist, ylist) = odesolver.driver(pend, a_damped, y0, b_integral, h);

        // Save the solution to a .data file
        string folderPath = "../data/"; // Change this to the desired folder path
        string fileName = "damped_oscillator.data"; // Change this to the desired filename
        Writefile(folderPath, fileName, xlist, ylist);

        WriteLine($"The damped harmonic osciallator: theta''(t) + b*theta'(t) + c*sin(theta(t)) = 0, \n with boundary conditions: theta(0) = pi - 0.1,  omega(0) = 0 \nis plotted and solved in {fileName}.svg. ");

    }

    static void LotkaVolterra()
    {
        // Define the Lotka-Volterra equations
        Func<double, vector, vector> lotkaVolterra = (t, z) =>
        {
            // Coefficients for the Lotka-Volterra equations
            double a = 1.5, b = 1, c = 3, d = 1;
            
            // Extract the variables from the state vector
            double x = z[0]; // Prey population
            double y = z[1]; // Predator population
            
            // Lotka-Volterra equations describe the dynamics of a predator-prey system
            // The equations model the interaction between prey (x) and predators (y)
            
            // The first equation represents the change in prey population:
            // dx/dt = a*x - b*x*y
            // The prey population grows due to reproduction (a*x) and decreases due to predation (b*x*y)
            
            // The second equation represents the change in predator population:
            // dy/dt = -c*y + d*x*y
            // The predator population decreases due to natural death (c*y) and increases due to successful predation (d*x*y)
            
            // The return vector holds the derivatives of the prey and predator populations with respect to time
            return new vector(a * x - b * x * y, -c * y + d * x * y);
        };


        // Initial conditions
        double a_lv = 0; // Start point
        vector ya_lv = new vector(10, 5); // Initial values

        // End point
        double b_lv = 15;

        var xlist = new genlist<double>();
        var ylist = new genlist<vector>();

        // Solve the Lotka-Volterra system using the AdaptiveDriver routine
        var (xs,ys) = odesolver.AdaptiveDriver(lotkaVolterra, a_lv, ya_lv, b_lv, xlist:xlist,ylist:ylist);

        var (x_endpoint,y_endpoint) = odesolver.AdaptiveDriver(lotkaVolterra, a_lv, ya_lv, b_lv);


        // Save the solution to a .data file
        string folderPath = "../data/"; // Update with the desired folder path
        string fileName = "lotka_volterra.data"; // Update with the desired filename
        Writefile(folderPath, fileName, xs, ys);
        Writefile(folderPath, "lotka_volterra_endpoints.data", x_endpoint, y_endpoint);

        WriteLine($"The solution to the Lotka-Volterra system is plotted and solved in {fileName}.svg.");

        
    }


    static void threebodyproblem()
    {
        
        Func<double, vector, vector> newtonianThreeBody = (double t, vector z) =>
        {
            double m1=1, m2=1, m3=1;
            
            /* Use structure:   z[0] = x1       z[1] = y1
                                z[2] = vx1      z[3] = vy1
                                z[4] = x2       z[5] = y2
                                z[6] = vx2      z[7] = vy2
                                z[8] = x3       z[9] = y3
                                z[10] = vx3     z[11] = vy3
                                */

        vector r1 = new vector(z[0], z[1]); // r = (x,y)
        vector dr1dt = new vector(z[2], z[3]); // dr/dt= v = (vx,vy)

        vector r2 = new vector(z[4], z[5]);
        vector dr2dt = new vector(z[6], z[7]);

        vector r3 = new vector(z[8], z[9]);
        vector dr3dt = new vector(z[10], z[11]);


        vector dv1dt = gravitation(r1, r2, r3, m1, m2, m3);
        vector dv2dt = gravitation(r2, r1, r3, m2, m1, m3);
        vector dv3dt = gravitation(r3, r1, r2, m3, m1, m2);

        double[] state_derivatives = new[]
        {
            /* structure is:
            dr₁/dt = v₁
            dv₁/dt = F₁/m₁

            array= 
            Array[0] = dr1dt_x;     // x-component of velocity of body 1
            Array[1] = dr1dt_y;    // y-component of velocity of body 1
            Array[2] = dv1dt_x;    // x-component of acceleration of body 1
            Array[3] = dv1dt_y;    // y-component of acceleration of body 1
            
            .
            .
            .

            */


            dr1dt[0], dr1dt[1], dv1dt[0], dv1dt[1], dr2dt[0], dr2dt[1], dv2dt[0], dv2dt[1], dr3dt[0],
            dr3dt[1], dv3dt[0], dv3dt[1]
        };

        return new vector(state_derivatives);

        };

        //Initial conditions
        /*  arrray structure for initial conditions:  x1(t=0), y1(t=0), v1_x(t=0), v1_y(t=0), x2(t=0), ...  where 1,2,3 represents each body
        */
        double[] y0Array = new[] { 0,0,-0.93240737, -0.86473146,
            -0.97000436, 0.24308753, 0.4662036850, 0.4323657300, 
            0.97000436, -0.24308753, 0.4662036850, 0.4323657300}; //initial conditions from wikipedia: Here the gravitational constant G has been set to 1, and the initial conditions are r1(0) = -r3(0) = (-0.97000436, 0.24308753); r2(0) = (0,0); v1(0) = v3(0) = (0.4662036850, 0.4323657300); v2(0) = (-0.93240737, -0.86473146).
        var y0 = new vector(y0Array);
        double a_int = 0;
        double b_int = 6.3259;
        double h = 0.01;


        var xlist = new genlist<double>();
        var ylist = new genlist<vector>();

        var (time, y_tbp) = odesolver.AdaptiveDriver(newtonianThreeBody, a_int, y0, b_int, h,xlist:xlist, ylist:ylist);

        /* y_tbp has structure:
        Column 0: x-coordinate of body 1
        Column 1: y-coordinate of body 1
        Column 2: x-velocity of body 1
        Column 3: y-velocity of body 1
        Column 4: x-coordinate of body 2
        Column 5: y-coordinate of body 2
        Column 6: x-velocity of body 2
        Column 7: y-velocity of body 2
        Column 8: x-coordinate of body 3
        Column 9: y-coordinate of body 3
        Column 10: x-velocity of body 3
        Column 11: y-velocity of body 3
        */

        string path = "../data/threebody.data";

        using (StreamWriter writer = new StreamWriter(path))
        {
            for (int i = 0; i < time.size; i++)
            {
                writer.Write($"{time[i]} ");
                for (int j = 0; j < y_tbp[i].size; j++)
                {
                    writer.Write($"{y_tbp[i][j]} ");
                }
                writer.Write("\n");
            }
        }

    }


    static vector gravitation(vector r, vector r2, vector r3, double m, double m2, double m3){
        const double G = 1;
        // Function calculates the newtonian gravitational forces acting on the body (r,m) from body 2 and 3.
        vector Gforce = -G*m*m2*(r-r2)/Pow((r-r2).norm(), 3) - G*m*m3*(r-r3)/Pow((r-r3).norm(), 3);
        return Gforce;
    }






    // Method to save solution to a .data file
    static void Writefile(string folderPath, string filename, genlist<double> xlist, genlist<vector> ylist)
    {
        using (StreamWriter writer = new StreamWriter(Path.Combine(folderPath, filename)))
        {
            for (int i = 0; i < xlist.size; i++)
            {
                double x = xlist[i];
                vector y = ylist[i];
                writer.WriteLine($"{x} {y[0]} {y[1]}"); // Save x, u, u' values in a tab-separated format
            }
        }
    }
}
