CODE_DIR = ../code/
DLL_DIR = ../../../matlib/matrix/
QRGS_DIR = ../../01-linearEquations/

DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)

default: main.exe Out.txt
	@echo build success!

Out.txt: main.exe ../data/higgsdata.txt
	mono main.exe < ../data/higgsdata.txt -type: > ../Out.txt

main.exe : $(CODE_DIR)main.cs matrix.dll minimization.dll genlist.dll
	mcs -target:exe -out:$@ \
		$(CODE_DIR)main.cs \
		$(DLLS)

matrix.dll: $(DLL_DIR)/matrix.cs $(DLL_DIR)/vector.cs
	mcs -target:library -out:./$@ $^

minimization.dll: $(CODE_DIR)minimization.cs matrix.dll
	mcs -target:library -out:$@	\
	$(CODE_DIR)minimization.cs \
	$(DLLS)

genlist.dll: $(CODE_DIR)genlist.cs matrix.dll
	mcs -target:library -out:$@ \
	$(CODE_DIR)genlist.cs  \
	$(DLLS)
