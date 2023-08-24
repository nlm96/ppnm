DPATH = ../data/
MPATH = plottingfiles/

default: swarmSize_convergence.svg maxIterations_convergence.svg acc_convergence.svg particle_movement.gif
	@echo finished plotting \:\)

swarmSize_convergence.svg: $(addprefix $(MPATH), swarmSize_convergence.plot) $(addprefix $(DPATH), swarmSize.data)
	gnuplot $<

maxIterations_convergence.svg: $(addprefix $(MPATH), maxIterations_convergence.plot) $(addprefix $(DPATH), maxIterations.data)
	gnuplot $<

acc_convergence.svg: $(addprefix $(MPATH), acc_convergence.plot) $(addprefix $(DPATH), acc.data)
	gnuplot $<

particle_movement.gif: $(addprefix $(MPATH), particle_movement.plot) $(addprefix $(DPATH), rosenbrockTracking.data)
	gnuplot $<
