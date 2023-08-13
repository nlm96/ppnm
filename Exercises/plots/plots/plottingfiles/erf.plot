set terminal svg background "white"
set key bottom right
set output "erf.svg"
set xlabel "x"
set ylabel "y"
set tics out
set xzeroaxis
set yzeroaxis
set samples 800
set key left 
set title "Error function"
plot [0:7][0:1.1] \
    "../data/graph-data/erf-calc.data" with lines title "Approximation"\
    , "../data/tab-data/erf-tab.data" using ($1):($2) with points pointtype 4 title "Table data"\
