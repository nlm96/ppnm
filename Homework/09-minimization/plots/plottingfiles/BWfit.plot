set terminal svg background "white"
set output "BreitWigner_fit.svg"
set key top right
set xlabel "E [GeV]"
set ylabel "Signal"
set grid
set yrange [-5:10]

set title "Breit-Wigner fit to Higgs data"
set tics out
plot "../data/fitted_data.txt" using ($1):($2) with lines lt rgb "sea-green" lw 2 title "Breit-Wigner fit"\
, "../data/higgsdata.txt" using 1:2:3 with yerrorbars lc "black" pt 7 ps 0.8 title "Higgs data"\