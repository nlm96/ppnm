set terminal svg background "white"
set key top right
set output "lngamma.svg"
set xlabel "x"
set ylabel "Ln(gamma((x)))"
set tics out
set xzeroaxis
set yzeroaxis
set samples 800
set title "LnGamma function"
plot [0:5][-0.3:5] \
    "../data/graph-data/lngamma-calc.data" with lines title "Approximation"\
