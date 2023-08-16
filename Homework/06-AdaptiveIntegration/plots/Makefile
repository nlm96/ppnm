DPATH = ../data/
MPATH = plottingfiles/

default: erf.svg
	@echo finished plotting \:\)

erf.svg: $(addprefix $(MPATH), erf.plot) $(addprefix $(DPATH), erf.data erfIntegral.data)
	gnuplot $<
