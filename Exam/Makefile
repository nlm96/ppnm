SPATH = ./code/
BPATH = ./build/
DPATH = ./data/
OPATH = ./plots/

default: 
	cd $(BPATH) && $(MAKE)	
	cd $(OPATH) && $(MAKE)
	@echo compiling success!


build:
	cd $(BPATH) && $(MAKE)

plot:
	$(RM) -rf $(addprefix $(OPATH), *.exe *.dll *.txt *.svg *.data)
	cd $(OPATH) && $(MAKE)

#################################################################################
clean:
	$(RM) -rf $(addprefix $(SPATH), *.exe *.dll *.txt *.svg *.data)
	$(RM) -rf $(addprefix $(BPATH), *.exe *.dll *.txt *.svg *.data)
	$(RM) -rf $(addprefix $(DPATH), *.exe *.dll *.txt *.svg *.data)
	$(RM) -rf $(addprefix $(OPATH), *.exe *.dll *.txt *.svg *.data)
	$(RM) -rf *.exe *.dll *.txt *.svg *.data
	clear
	@echo cleaned