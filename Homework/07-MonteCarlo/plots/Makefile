DPATH = ../data/
MPATH = plottingfiles/

default: unicircle.svg uni_error.svg compare.svg
	@echo finished plotting \:\)

unicircle.svg: $(addprefix $(MPATH), unicircle.plot) $(addprefix $(DPATH), unicircle.data)
	gnuplot $<

uni_error.svg: $(addprefix $(MPATH), uni_error.plot) $(addprefix $(DPATH), unicircle.data)
	gnuplot $<

compare.svg: $(addprefix $(MPATH), compare.plot) $(addprefix $(DPATH), unicircle.data)
	gnuplot $<