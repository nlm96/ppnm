DPATH = ../data/
MPATH = plottingfiles/

default: convergence.svg solution.svg
	@echo finished plotting \:\)

convergence.svg: $(addprefix $(MPATH), convergence.plot) $(addprefix $(DPATH), acc_data.data  eps_data.data  rmax_data.data  rmin_data.data)
	gnuplot $<

solution.svg: $(addprefix $(MPATH), solution.plot) $(addprefix $(DPATH), f_E_data.data)
	gnuplot $<