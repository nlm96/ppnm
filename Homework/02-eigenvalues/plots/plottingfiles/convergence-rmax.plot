set terminal svg background "white"
set key bottom right
set output "convergence-rmax.svg"
set xlabel "r_{max}"
set ylabel "\epsilon_0"
set tics out
set xzeroaxis
set yzeroaxis
set samples 800
set title "Convergence for r_{max}"
plot [2:20][-0.6:0.1] \
    "../data/convergence-rmax.data" with points title "dr=0.2", \
