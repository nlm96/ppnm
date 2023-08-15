DPATH = ../data/
MPATH = plottingfiles/

default: Decay.svg
	@echo finished plotting \:\)

Decay.svg: $(addprefix $(MPATH), decay.plot) $(addprefix $(DPATH), Expfit_data.data Expfit_plot.data)
	gnuplot $<