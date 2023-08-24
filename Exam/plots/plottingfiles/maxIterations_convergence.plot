set terminal svg enhanced font 'Verdana,12' size 800,600 background rgb 'white'
set output "maxIterations_convergence.svg"

set multiplot layout 2,1

set title "Convergence Test: Varying Max Iterations"
set xlabel "Max Iterations"
set ylabel "Functional Value"
set yrange [0:500]
plot "../data/maxIterations.data" using 1:3 title "Functional Value" with points lt rgb "red" pt 7 ps 1
set title "Convergence Test: Varying Max Iterations"
set xlabel "Max Iterations"
set ylabel "Iterations Used"
set yrange [0:10000]
plot "../data/maxIterations.data" using 1:2 title "Iterations Used" with points lt rgb "purple" pt 7 ps 1

unset multiplot
