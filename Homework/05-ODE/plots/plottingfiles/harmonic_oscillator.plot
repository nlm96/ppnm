set terminal svg background "white"
set key top left
set output "harmonic_oscillator.svg"
set xlabel "t"
set ylabel "Amplitude"
set tics out
set samples 800
set title "Harmonic Oscillator Solution: u'' = -u, u(0) = 1, u'(0) = 0"
set yrange [-1.2:1.2]
plot [0:10] \
    "../data/harmonic_oscillator.data" using 1:2 with lines linecolor rgb "red" linewidth 3 title "u(t)", \
    "../data/harmonic_oscillator.data" using 1:3 with lines linecolor rgb "blue" linewidth 3 title "u'(t)"
