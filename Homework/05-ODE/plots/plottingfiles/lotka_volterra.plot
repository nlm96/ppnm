# Lotka-Volterra System Plot
set terminal svg background "white"
set output "LotkaVolterra.svg"
set key top left
set xlabel "Time"
set ylabel "Population"
set tics out

# Define plot ranges and labels
xmin = 0
xmax = 15
ymin = 0
ymax = 15

set xrange [xmin:xmax]
set yrange [ymin:ymax]

# Plot prey population (x) and predator population (y)
plot "../data/lotka_volterra.data" using 1:2 with lines linecolor rgb "blue" linewidth 2 title "x", \
     "../data/lotka_volterra.data" using 1:3 with lines linecolor rgb "red" linewidth 2 title "y", \
     "../data/lotka_volterra_endpoints.data" using 1:3 with points linecolor rgb "red" pointtype 7 linewidth 2 title "Endpoints", \
     "../data/lotka_volterra_endpoints.data" using 1:2 with points linecolor rgb "blue" pointtype 7 linewidth 2 title "Endpoints"

# Add legends and title
set key top left
set title "Lotka-Volterra System"
set xlabel "Time"
set ylabel "Population"
set grid
