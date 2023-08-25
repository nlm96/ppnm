set terminal svg background "white" size 1000,800
set output "Plot.svg"

set multiplot layout 2,2

set title "Cos(5x-1)Exp(-xx)" font ",18"
set xlabel "x" font ",16"
set ylabel "g(x)" font ",16"
set key font ",13"
set key bottom right
plot "../data/InterpolationTable.data" index 0 using 1:2 with points pt 42 lc rgb "blue" title "Points", \
     "../data/InterpolationTable.data" index 1 using 1:2 with lines lc rgb "red" title "Interpolation from neural-network"

set title "First derivative" font ",18"
set xlabel "x" font ",16"
set ylabel "g'(x)" font ",16"
set key font ",13"
set key bottom left
plot "../data/FirstD.data" using 1:3 with lines lc "web-green" title "Analytical solution", \
     "../data/FirstD.data" using 1:2 with points pt 7 ps 0.25 lc "black" title "Interpolated values"

set title "Second derivative" font ",18"
set xlabel "x" font ",16"
set ylabel "g''(x)" font ",16"
set key font ",13"
set key bottom left
plot "../data/SecondD.data" using 1:3 with lines lc "web-green" title "Analytical solution", \
     "../data/SecondD.data" using 1:2 with points pt 7 ps 0.25 lc "black" title "Interpolated values"

set title "Anti-derivative" font ",18"
set xlabel "x" font ",16"
set ylabel "G(x)" font ",16"
set key font ",13"
set key bottom right
plot "../data/AntiD.data" using 1:2 with points pt 7 ps 0.25 lc "black" title "Interpolated values", \
     "../data/AntiD.data" using 1:3 with lines lc "web-green" title "Analytical solution"
