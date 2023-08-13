BPATH = ../../build/

default: erf-calc.data gamma-calc.data lngamma-calc.data
	@echo data success!
	

#################################################################################
erf-calc.data: $(addprefix $(BPATH), erf-calc.exe)
	mono $< > $> $@ 

gamma-calc.data: $(addprefix $(BPATH), gamma-calc.exe)
	mono $< > $> $@ 

lngamma-calc.data: $(addprefix $(BPATH), lngamma-calc.exe)
	mono $< > $> $@ 
	