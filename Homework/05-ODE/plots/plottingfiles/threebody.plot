set terminal gif animate delay 2    #2 ms
set output "threebody.gif"

#Get the number of frames from the data file
filedata = '../data/threebody.data'
n = system(sprintf('cat %s | wc -l', filedata))

# Set the range for the x and y axes
set xrange [-1.5:1.5]
set yrange [-1:1]


#Set up the initial plot
plot filedata using 2:3 every ::0::0 with points pt 7 lc rgb "black" notitle, \
     filedata using 6:7 every ::0::0 with points pt 7 lc rgb "red" notitle, \
     filedata using 10:11 every ::0::0 with points pt 7 lc rgb "blue" notitle, \
     filedata using 2:3 every ::0::0 with lines lc rgb "black" notitle, \
     filedata using 6:7 every ::0::0 with lines lc rgb "red" notitle, \
     filedata using 10:11 every ::0::0 with lines lc rgb "blue" notitle

#loop through the data and update the plot
do for [i=1:n-1]{
    plot filedata using 2:3 every ::i::i with points pt 7 ps 2 lc rgb "black" notitle, \
         filedata using 6:7 every ::i::i with points pt 7 ps 2 lc rgb "red" notitle, \
         filedata using 10:11 every ::i::i with points pt 7 ps 2 lc rgb "blue" notitle, \
         filedata using 2:3 every ::0::i with lines lw 2 lc rgb "black" notitle, \
         filedata using 6:7 every ::0::i with lines lw 2 lc rgb "red" notitle, \
         filedata using 10:11 every ::0::i with lines lw 2 lc rgb "blue" notitle
}