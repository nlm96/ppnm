SPATH1 = ../code/graph/
SPATH2 = ../code/functions/

default: 	erf-calc.exe gamma-calc.exe lngamma-calc.exe
	@echo build success!


#---------------- erf ----------------------------------
erf-calc.exe: $(addprefix $(SPATH1), erf-calc.cs) sfuns.dll
	mcs -target:exe -out:$@ \
		$(filter %.cs, $^)	\
		$(addprefix -reference:,$(filter %.dll,$^))


#----------------- gamma ------------------
gamma-calc.exe: $(addprefix $(SPATH1), gamma-calc.cs) sfuns.dll
	mcs -target:exe -out:$@ \
		$(filter %.cs, $^)	\
		$(addprefix -reference:,$(filter %.dll,$^))

#--------------- lngamma ------------------------
lngamma-calc.exe: $(addprefix $(SPATH1), lngamma-calc.cs) sfuns.dll
	mcs -target:exe -out:$@ \
		$(filter %.cs, $^)	\
		$(addprefix -reference:,$(filter %.dll,$^))
		

#################################################################################
sfuns.dll: $(addprefix $(SPATH2), erf.cs gamma.cs lngamma.cs)
	mcs -target:library -out:$@ $^