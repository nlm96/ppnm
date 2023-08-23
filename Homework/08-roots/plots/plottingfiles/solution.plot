set term svg background rgb "white"
set output "solution.svg"
set tics in
set key top right

set xlabel "r" font ",20"
set ylabel "f(r)" font ",20"
set title "Bound State Hydrogen Atom" font ",25"

plot "../data/f_E_data.data" every ::1 u 1:2 with points pointtype 7 linecolor "black" pointsize 1 title "Minimised solution", \
    x*exp(-x) with lines linetype 1 linecolor "red" linewidth 2.5 title "Analytical solution"
    
 