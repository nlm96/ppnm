CODE_DIR = ../code/
DLL_DIR = ../../../matlib/matrix/

DLLS = $(addprefix -reference:,$(filter %.dll,$^))
CODE = $(filter %.cs,$^)

default: main.exe Out.txt
	@echo build success!

Out.txt: main.exe
	mono main.exe -type: > ../Out.txt

main.exe : $(CODE_DIR)main.cs matrix.dll genlist.dll ODE.dll
	mcs -target:exe -out:$@ \
		$(CODE_DIR)main.cs \
		$(DLLS)

matrix.dll: $(DLL_DIR)/matrix.cs $(DLL_DIR)/vector.cs
	mcs -target:library -out:./$@ $^

ODE.dll: $(CODE_DIR)ODE.cs matrix.dll genlist.dll
	mcs -target:library -out:$@ \
	$(CODE_DIR)ODE.cs  \
	$(DLLS)

genlist.dll: $(CODE_DIR)genlist.cs matrix.dll
	mcs -target:library -out:$@ \
	$(CODE_DIR)genlist.cs  \
	$(DLLS)