set terminal svg background "white"
set key top left
set output "damped_oscillator.svg"
set xlabel "t"
set ylabel "Amplitude"
set tics out
set samples 800
set title "Damped Oscillator Solution: theta''(t) + b*theta'(t) + c*sin(theta(t)) = 0,\ntheta(0) = pi - 0.1, omega(0) = 0"
plot [0:10] \
    "../data/damped_oscillator.data" using 1:2 with lines linecolor rgb "red" linewidth 3 title "theta(t)", \
    "../data/damped_oscillator.data" using 1:3 with lines linecolor rgb "blue" linewidth 3 title "omega(t)"
