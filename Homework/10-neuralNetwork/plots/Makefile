DPATH = ../data/
MPATH = plottingfiles/

default: interpolation.svg 
	@echo finished plotting \:\)

interpolation.svg: $(addprefix $(MPATH), interpolation.plot) $(addprefix $(DPATH), InterpolationTable.data)
	gnuplot $<
