set terminal svg enhanced font "arial,10" background rgb "white"
set output "qspline.svg"
set multiplot layout 2,2

data_path = "../data/"

# Set a smaller point size for linespoints
set style line 1 pointsize 1.5
set style line 2 pointsize 0.5
set style line 3 pointsize 0.5
set style line 4 pointsize 0.5

# First subplot: Tabulated values and Quadratic Interpolation
set title "Tabulated Values and Quadratic Interpolation"
set xrange [0.8:7]
set yrange [-0.2:8]
plot data_path."qspline_const.data" using 1:2 with points linestyle 1 title "Tabulated values for y", \
     data_path."qspline_const.data" using 3:4 with linespoints linestyle 2 title "Quadratic interpolation", \
     data_path."qspline_const.data" using 3:5 with linespoints linestyle 3 title "Integral", \
     data_path."qspline_const.data" using 3:6 with linespoints linestyle 4 title "Derivative"

# Second subplot: Tabulated values and Quadratic Interpolation
set title "Tabulated Values and Quadratic Interpolation"
set xrange [0.8:7]
set yrange [0:23]
plot data_path."qspline_lin.data" using 1:2 with points linestyle 1 title "Tabulated values for y", \
     data_path."qspline_lin.data" using 3:4 with linespoints linestyle 2 title "Quadratic interpolation", \
     data_path."qspline_lin.data" using 3:5 with linespoints linestyle 3 title "Integral", \
     data_path."qspline_lin.data" using 3:6 with linespoints linestyle 4 title "Derivative"

# Third subplot: Tabulated values and Quadratic Interpolation
set title "Tabulated Values and Quadratic Interpolation"
set xrange [0.8:7]
set yrange [0:60]
plot data_path."qspline_square.data" using 1:2 with points linestyle 1 title "Tabulated values for y", \
     data_path."qspline_square.data" using 3:4 with linespoints linestyle 2 title "Quadratic interpolation", \
     data_path."qspline_square.data" using 3:5 with linespoints linestyle 3 title "Integral", \
     data_path."qspline_square.data" using 3:6 with linespoints linestyle 4 title "Derivative"

# Fourth subplot: Tabulated values and Quadratic Interpolation
set title "Tabulated Values and Quadratic Interpolation"
set xrange [0.8:7]
set yrange [-2.5:2.5]
plot data_path."qspline_cos.data" using 1:2 with points linestyle 1 title "Tabulated values for y", \
     data_path."qspline_cos.data" using 3:4 with linespoints linestyle 2 title "Quadratic interpolation", \
     data_path."qspline_cos.data" using 3:5 with linespoints linestyle 3 title "Integral", \
     data_path."qspline_cos.data" using 3:6 with linespoints linestyle 4 title "Derivative"

unset multiplot
