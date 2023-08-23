set term svg background rgb "white"
set output "Convergence.svg"

set multiplot layout 2,2

set tics in
set xtics font ",5"
set ytics font ",5"
unset key
set xlabel "accuracy" font ",20"
set ylabel "E0" font ",20"
set title "Accuracy test" font ",25"
plot "../data/acc_data.data" using 1:2 with points pt 7 ps 0.5 lc rgb "black"

set tics in
unset key
set xlabel "epsilon" font ",20"
set ylabel "E0" font ",20"
set title "Epsilon test" font ",25"
plot "../data/eps_data.data" using 1:2 with points pt 7 ps 0.5 lc rgb "black"

set tics in
unset key
set xlabel "rmin" font ",20"
set ylabel "E0" font ",20"
set title "rmin test" font ",25"
plot "../data/rmin_data.data" using 1:2 with points pt 7 ps 0.5 lc rgb "black"

set tics in
unset key
set xlabel "rmax" font ",20"
set ylabel "E0" font ",20"
set title "rmax test" font ",25"
plot "../data/rmax_data.data" using 1:2 with points pt 7 ps 0.5 lc rgb "black"