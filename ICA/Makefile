EIGEN		= d:/src/Eigen

LINK		= link
CPPFLAGS	= /nologo /EHsc /LD /Ox -I $(EIGEN)
LDFLAGS		= /NOLOGO /dll /LIBPATH:$(EIGEN)

BIN			= ICA.dll
OBJS		= ICA.obj

.SUFFIXES:			.cxx .obj

$(BIN):				$(OBJS)
					$(LINK) $(LDFLAGS) $(OBJS) /OUT:$@

ICA.obj:			$$(@R).cxx

.cxx.obj:
					$(CPP) $(CPPFLAGS) -c $<

clean:
					del *.obj *.exp *.lib *.dll
