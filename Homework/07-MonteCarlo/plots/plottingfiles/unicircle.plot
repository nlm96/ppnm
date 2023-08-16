set terminal svg
set terminal svg background "white"
set output "unicircle.svg"
set key top right
set xlabel "N"
set logscale x 10
set xzeroaxis
set xrange [-0.1:10000000]
set ylabel "Area"
set tics out
set grid
set title "Monte Carlo approximaton of area of unit circle"
plot "../data/unicircle.data" using ($1):($2) with point pt 7 ps 0.6 lc rgb "web-blue" title "Pseudo-random"\
, "../data/unicircle.data" using ($1):($2) with lines lc rgb "web-blue" notitle\
, "../data/unicircle.data" using ($1):($6) with point pt 7 ps 0.6 lc rgb "web-green" title "Quasi-random "\
, "../data/unicircle.data" using ($1):($6) with lines lc rgb "web-green" notitle\