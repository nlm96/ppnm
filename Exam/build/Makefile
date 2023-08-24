CODE_DIR = ../code/
DLL_DIR = ../../matlib/matrix/
QRGS_DIR = ../../01-linearEquations/

DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)

default: main.exe Out.txt
	@echo build success!

Out.txt: main.exe
	mono main.exe -type: > ../Out.txt

main.exe : $(CODE_DIR)main.cs matrix.dll BBPSO.dll
	mcs -target:exe -out:$@ \
		$(CODE_DIR)main.cs \
		$(DLLS)

matrix.dll: $(DLL_DIR)/matrix.cs $(DLL_DIR)/vector.cs
	mcs -target:library -out:./$@ $^

BBPSO.dll: $(CODE_DIR)BBPSO.cs matrix.dll
	mcs -target:library -out:$@	\
	$(CODE_DIR)BBPSO.cs \
	$(DLLS)

