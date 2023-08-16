set term svg size 800,600 background rgb "white"
set output "erf.svg"
set title "Error Function and Error Function Integral"
set xlabel "x"
set ylabel "y"
set grid
set key top left spacing 1.5
set border linewidth 1.5
set tics scale 0.75
set yrange [-1.5:1.5]

plot "../data/erf.data" using 1:2 with lines linewidth 2 linecolor rgb "red" title "erf(x)", \
     "../data/erfIntegral.data" using 1:2 with lines linestyle 2 dashtype 2 linewidth 2 linecolor rgb "black" title "erfIntegral(x)"
