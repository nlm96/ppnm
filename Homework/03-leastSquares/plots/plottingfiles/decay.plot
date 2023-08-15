set terminal svg background "white"
set key left
set output "Decay.svg"
set xlabel "Time in days"
set ylabel "Activity"
set tics out
set xzeroaxis
set yzeroaxis
set samples 800
set title "Decay of element ThX"

plot "../data/Expfit_data.data" using 1:2:3 with yerrorbars title "Data with Errorbars", \
     "../data/Expfit_plot.data" using 1:2 with lines title "Fit", \
     "../data/Expfit_plot.data" using 1:3 with lines title "Upper Bound", \
     "../data/Expfit_plot.data" using 1:4 with lines title "Lower Bound"
