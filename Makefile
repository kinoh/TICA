LINK		= link
CPPFLAGS	= /nologo /EHsc /wd4820 /wd4350 /wd4514 /wd4710 /wd4711 -Wall -Ox -I d:/src/Eigen
LDFLAGS		= /NOLOGO

BIN			= main.exe
OBJS		= main.obj BitmapPicker.obj BitmapWriter.obj ICA.obj


$(BIN):				$(OBJS)
					$(LINK) $(LDFLAGS) $(OBJS) /OUT:$@

main.obj:			$$(@R).cxx BitmapPicker.h BitmapWriter.h ICA.h

ICA.obj:			$$(@R).cxx $$(@R).h

BitmapPicker.obj:	$$(@R).cxx $$(@R).h

BitmapWriter.obj:	$$(@R).cxx $$(@R).h

.cxx.obj:
					$(CPP) $(CPPFLAGS) -c $<

clean:
					del *.obj *.exe
