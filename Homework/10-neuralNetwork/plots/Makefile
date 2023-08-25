DPATH = ../data/
MPATH = plottingfiles/

default: interpolation.svg plot.svg
	@echo finished plotting \:\)

interpolation.svg: $(addprefix $(MPATH), interpolation.plot) $(addprefix $(DPATH), InterpolationTable.data)
	gnuplot $<

plot.svg: $(addprefix $(MPATH), plot.plot) $(addprefix $(DPATH), AntiD.data  FirstD.data  InterpolationTable.data  SecondD.data)
	gnuplot $<
