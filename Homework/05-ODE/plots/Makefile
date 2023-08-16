DPATH = ../data/
MPATH = plottingfiles/

default: harmonic_oscillator.svg damped_oscillator.svg lotka_volterra.svg threebody.gif
	@echo finished plotting \:\)

harmonic_oscillator.svg: $(addprefix $(MPATH), harmonic_oscillator.plot) $(addprefix $(DPATH), harmonic_oscillator.data )
	gnuplot $<

damped_oscillator.svg: $(addprefix $(MPATH), damped_oscillator.plot) $(addprefix $(DPATH), damped_oscillator.data )
	gnuplot $<

lotka_volterra.svg: $(addprefix $(MPATH), lotka_volterra.plot) $(addprefix $(DPATH), lotka_volterra.data lotka_volterra_endpoints.data)
	gnuplot $<

threebody.gif: $(addprefix $(MPATH), threebody.plot) $(addprefix $(DPATH), threebody.data )
	gnuplot $<