set terminal svg background "white"
set key top left
set output "eigenfunctions.svg"
set xlabel "r"
set ylabel "|u_{n0}(r)|^2"
set tics out

set xzeroaxis
set yzeroaxis
set samples 800
set title "Eigenfunctions"
set xrange [0:20]
set yrange [0:0.7]

# Define custom line types
set style line 1 lt 1 lc rgb "blue" lw 3
set style line 2 lt 2 lc rgb "red" lw 3
set style line 3 lt 3 lc rgb "green" lw 3
set style line 4 lt 1 lc rgb "#CCCCCC" lw 3 dt 2

plot \
    for [n=0:2] "../data/eigenfunctions.data" using 1:(column(n+2)) every ::1 with lines linestyle n+1 title sprintf("Calculated Eigenfunc n=%d", n+1), \
    for [n=0:2] "../data/analytical_eigenfunctions.data" using 1:(column(n+2)) every ::1 with lines linestyle 4 title sprintf("Analytical Eigenfunc n=%d", n+1)
