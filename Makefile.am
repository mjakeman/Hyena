
SUBDIRS =  \
    build \
    Hyena \
	Mono.Data.Sqlite \
	Hyena.Data.Sqlite \
	Hyena.Gui

clean-local:
	rm -rf $(top_builddir)/bin

if ENABLE_TESTS
TEST_ASSEMBLIES = \
	Hyena.dll \
	Hyena.Data.Sqlite.dll \
	Hyena.Gui.dll

ENV_OPTIONS = TZ=America/Chicago LC_ALL=it_IT LANG=it_IT
NUNIT_CONSOLE = $$(echo $$(which nunit-console2 || which nunit-console))
RUNNER = for asm in $${TEST_ASSEMBLIES}; do echo -e "\033[1mRunning tests on $${asm}...\033[0m"; $(ENV_OPTIONS) $(NUNIT_CONSOLE) -nologo -noshadow $$asm; done

test:
	@pushd bin &>/dev/null; \
	export TEST_ASSEMBLIES="$(TEST_ASSEMBLIES)"; $(RUNNER); \
	popd &>/dev/null;
else
test:
	echo "Tests not enabled.  Pass --enable-tests to configure or ./autogen.sh"
endif

# pkg-config
pkgconfigdir = $(libdir)/pkgconfig
pkgconfig_in_files = \
	Hyena/hyena.pc.in \
	Hyena.Data.Sqlite/hyena.data.sqlite.pc.in \
	Hyena.Gui/hyena.gui.pc.in
pkgconfig_DATA = $(pkgconfig_in_files:.pc.in=.pc)
EXTRA_DIST = $(pkgconfig_in_files)
CLEANFILES = $(pkgconfig_DATA)
%.pc: $(pkgconfig_in_files)
	sed "s,\@VERSION\@,$(VERSION),g; s,\@prefix\@,$(prefix),g; s,\@libdir\@,$(libdir),g; s,\@PACKAGE\@,$(PACKAGE),g" < $< > $@

MAINTAINERCLEANFILES = \
	compile \
	INSTALL \
	config.h.in \
	aclocal.m4 \
	ltmain.sh \
	Makefile.in \
	depcomp \
	missing \
	install-sh \
	configure \
	config.sub \
	config.guess \
	intltool-extract.in \
	intltool-merge.in   \
	intltool-update.in  \
	mkinstalldirs
