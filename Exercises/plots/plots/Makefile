DPATH = ../data/
MPATH = plottingfiles/

default: erf.svg Gamma.svg LnGamma.svg
	@echo finished plotting \:\)

erf.svg: $(addprefix $(MPATH), erf.plot) $(addprefix $(DPATH), graph-data/erf-calc.data tab-data/erf-tab.data)
	gnuplot $<

Gamma.svg: $(addprefix $(MPATH), gamma.plot) $(addprefix $(DPATH), graph-data/gamma-calc.data tab-data/factorials.data)
	gnuplot $<

LnGamma.svg: $(addprefix $(MPATH), lngamma.plot) $(addprefix $(DPATH), graph-data/lngamma-calc.data)
	gnuplot $<