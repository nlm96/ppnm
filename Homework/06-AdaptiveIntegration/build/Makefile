CODE_DIR = ../code/
DLL_DIR = ../../../matlib/matrix/

DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)

default: main.exe Out.txt
	@echo build success!

Out.txt: main.exe
	mono main.exe -type: > ../Out.txt

main.exe : $(CODE_DIR)main.cs integrator.dll matrix.dll sfuns.dll
	mcs -target:exe -out:$@ \
		$(CODE_DIR)main.cs \
		$(DLLS)

matrix.dll: $(DLL_DIR)/matrix.cs $(DLL_DIR)/vector.cs
	mcs -target:library -out:./$@ $^

integrator.dll: $(CODE_DIR)integrator.cs matrix.dll
	mcs -target:library -out:$@ \
	$(CODE_DIR)integrator.cs  \
	$(DLLS)

sfuns.dll: $(CODE_DIR)erf.cs matrix.dll integrator.dll
	mcs -target:library -out:$@ \
	$(CODE_DIR)erf.cs  \
	$(DLLS)