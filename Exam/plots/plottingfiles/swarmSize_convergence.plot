set terminal svg enhanced font 'Verdana,12' size 800,600 background rgb 'white'
set output "swarmSize_convergence.svg"

set multiplot layout 2,1

set title "Convergence Test: Varying Swarm Size"
set xlabel "Swarm Size"
set ylabel "Functional Value"
plot "../data/swarmSize.data" using 1:3 title "Functional Value" with points lt rgb "blue" pt 7 ps 1

set title "Convergence Test: Varying Swarm Size"
set xlabel "Swarm Size"
set ylabel "Iterations Used"
plot "../data/swarmSize.data" using 1:2 title "Iterations Used" with points lt rgb "green" pt 7 ps 1

unset multiplot
