set terminal svg background "white"
set key bottom right
set output "convergence-dr.svg"
set xlabel "dr"
set ylabel "\epsilon_0"
set tics out

set xzeroaxis
set yzeroaxis
set samples 800
set title "Convergence dr"
set xrange [0:3]
set yrange [-0.6:-0.1]
plot \
    "../data/convergence-dr.data" with points title "r_{max}=20", \
