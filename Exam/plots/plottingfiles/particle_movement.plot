set terminal gif animate delay 20   # Delay between frames in 0.1 seconds
set output "particle_movement.gif"

filedata = "../data/rosenbrockTracking.data"  # Path to your data file

n = system(sprintf('cat %s | wc -l', filedata))

set title "Particle Movement Animation"  # Set the title

# Adjust the ranges for the x and y axes if needed
set xrange [-1000:1000]
set yrange [-1000:1000]

do for [i=2:n-1] {
    plot for [j=1:30] filedata using (column(2+(j-1)*2)):(column(2+(j-1)*2 + 1)) every ::i-1::i with points pt 7 ps 1 lc j notitle, \
         for [j=1:30] filedata using (column(2+(j-1)*2)):(column(2+(j-1)*2 + 1)) every ::(i-2)::i with lines lc j lw 1 dt 2 notitle
}
