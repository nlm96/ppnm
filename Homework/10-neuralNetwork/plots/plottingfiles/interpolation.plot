set term svg background rgb "white"
set output "interpolation.svg"
set xlabel "x"
set ylabel "y"
set key below
set title "g(x)=cos(5*x-1)*exp(-x*x)"
set grid
plot\
"../data/InterpolationTable.data" index 0 using 1:2 with points pt 42 lc rgb "blue" title "Points",\
"../data/InterpolationTable.data" index 1 using 1:2 with lines lc rgb "red" title "Interpolation from neural-network"
