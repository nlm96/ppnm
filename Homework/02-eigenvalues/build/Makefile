CODE_DIR = ../code/
DLL_DIR = ../../../matlib/matrix/

DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)

default: main.exe Out.txt
	@echo build success!

Out.txt: main.exe
	mono main.exe -type: -rmax 30 -dr 0.1 > ../Out.txt

main.exe : $(CODE_DIR)main.cs matrix.dll jacobi.dll RadialSchrodingerSolver.dll analyticalHydrogenWavefunction.dll
	mcs -target:exe -out:$@ \
		$(CODE_DIR)main.cs \
		$(DLLS)

matrix.dll: $(DLL_DIR)/matrix.cs $(DLL_DIR)/vector.cs
	mcs -target:library -out:./$@ $^

jacobi.dll: $(CODE_DIR)jacobi.cs matrix.dll
	mcs -target:library -out:$@	\
	$(CODE_DIR)jacobi.cs \
	$(DLLS)

RadialSchrodingerSolver.dll: $(CODE_DIR)RadialSchrodingerSolver.cs jacobi.dll matrix.dll
	mcs -target:library -out:$@ \
	$(CODE_DIR)RadialSchrodingerSolver.cs \
	$(DLLS)

analyticalHydrogenWavefunction.dll: $(CODE_DIR)analyticalHydrogenWavefunction.cs matrix.dll
	mcs -target:library -out:$@ \
	$(CODE_DIR)analyticalHydrogenWavefunction.cs \
	$(DLLS)
