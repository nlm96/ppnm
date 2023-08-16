set terminal svg
set terminal svg background "white"
set output "uni_error.svg"
set key top right
set xlabel "N"
set logscale x 10
set xzeroaxis
set xrange [0.8:10000000]
set tics out
set grid
set title "Error dependance of sampling points N for plain Monte Carlo integration"
plot "../data/unicircle.data" using ($1):($3) with point pt 7 ps 0.6 lc rgb "web-blue" title "Estimated error"\
, "../data/unicircle.data" using ($1):($4) with point pt 7 ps 0.6 lc rgb "purple" title "Real error"\
,"../data/unicircle.data" using ($1):($5) with lines lw 3 lc rgb "forest-green" title "1/Sqrt(N)"\