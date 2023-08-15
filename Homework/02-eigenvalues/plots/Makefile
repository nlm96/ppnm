DPATH = ../data/
MPATH = plottingfiles/

default: convergence-dr.svg convergence-rmax.svg eigenfuncs.svg
	@echo finished plotting \:\)

convergence-dr.svg: $(addprefix $(MPATH), convergence-dr.plot) $(addprefix $(DPATH), convergence-dr.data)
	gnuplot $<

convergence-rmax.svg: $(addprefix $(MPATH), convergence-rmax.plot) $(addprefix $(DPATH), convergence-rmax.data)
	gnuplot $<


eigenfuncs.svg: $(addprefix $(MPATH), radial-wavefunc.plot) $(addprefix $(DPATH), /eigenfunctions.data analytical_eigenfunctions.data)
	gnuplot $<
