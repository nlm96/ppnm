set terminal svg background "white"
set key bottom right
set output "Linspline.svg"
set xlabel "x"
set ylabel "y"
set tics out
set xzeroaxis
set yzeroaxis
set samples 800
set title "Linear spline"
set yrange [-2:1.2]  # Adjust the y-axis range

data_path = "../data/"

plot [0:10] \
    data_path."data.data" using 1:2 with points title "Original data (cos)" pointsize 1 lc rgb "blue", \
    data_path."linspline.data" using 1:2 with points title "Splined datapoints" pointsize 1 lc rgb "green", \
    data_path."int_linspline.data" using 1:2 with points title "Integration of spline" pointsize 1 lc rgb "red", \
    data_path."sin.data" using 1:2 with lines title "sin(x)" linewidth 2 lc rgb "#999999" dashtype 3, \
    data_path."cos.data" using 1:2 with lines title "cos(x)" linewidth 2 lc rgb "#444444" dashtype 3