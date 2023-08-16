set terminal svg
set terminal svg background "white"
set output "compare.svg"
set key top right
set xlabel "N"
set logscale x 10
set logscale y 10
set xzeroaxis
set xrange [0.8:10000000]
set tics out
set grid
set title "Error dependance of sampling points N for Monte Carlo integrations"
plot "../data/unicircle.data" using ($1):($3) with point pt 7 ps 0.6 lc rgb "web-blue" title "Pseudo-random estimated error"\
,"../data/unicircle.data" using ($1):($7)  with point pt 7 ps 0.6 lc rgb "orchid4" title "Quasi-random estimated error"\
, "../data/unicircle.data" using ($1):($4) with point pt 7 ps 0.6 lc rgb "purple" title "Real error"\
,"../data/unicircle.data" using ($1):($5) with lines lw 3 lc rgb "forest-green" title "1/Sqrt(N)"\