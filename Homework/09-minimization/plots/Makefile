DPATH = ../data/
MPATH = plottingfiles/

default: BreitWigner_fit.svg
	@echo finished plotting \:\)

BreitWigner_fit.svg: $(addprefix $(MPATH), BWfit.plot) $(addprefix $(DPATH), fitted_data.txt  higgsdata.txt)
	gnuplot $<
