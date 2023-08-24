set terminal svg enhanced font 'Verdana,12' size 800,600 background rgb 'white'
set output "acc_convergence.svg"

set multiplot layout 2,1

set title "Convergence Test: Varying ACC"
set xlabel "ACC"
set ylabel "Functional Value"
plot "../data/acc.data" using 1:3 title "Functional Value" with points lt rgb "orange" pt 7 ps 1

set title "Convergence Test: Varying ACC"
set xlabel "ACC"
set ylabel "Iterations Used"
plot "../data/acc.data" using 1:2 title "Iterations Used" with points lt rgb "pink" pt 7 ps 1
unset multiplot
