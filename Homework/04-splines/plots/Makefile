DPATH = ../data/
MPATH = plottingfiles/

default: linspline.svg qspline.svg
	@echo finished plotting \:\)

linspline.svg: $(addprefix $(MPATH), linspline.plot) $(addprefix $(DPATH), cos.data  data.data  int_linspline.data  linspline.data  sin.data)
	gnuplot $<

qspline.svg: $(addprefix $(MPATH), qspline.plot) $(addprefix $(DPATH), qspline_const.data qspline_lin.data qspline_cos.data qspline_square.data)
	gnuplot $<