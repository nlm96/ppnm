set terminal svg background "white"
set key bottom right
set output "gamma.svg"
set xlabel "x"
set ylabel "Gamma(x)"
set tics out
set xzeroaxis
set yzeroaxis
set samples 800
set title "Gamma function"
plot [-5:5][-5:5] \
    "../data/graph-data/gamma-calc.data" with lines title "Approximation"\
    , "../data/tab-data/factorials.data" using ($1+1):($2) with points pointtype 4 title "Table data"\
