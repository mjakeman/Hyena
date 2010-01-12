
EXTRA_DIST = \
	expansions.m4 \
	src/Hyena.Gui/Hyena.Gui.dll.config

#Warning: This is an automatically generated file, do not edit!
if ENABLE_DEBUG
 SUBDIRS =  src/Hyena src/Mono.Data.Sqlite src/Hyena.Data.Sqlite src/Hyena.Gui
endif
if ENABLE_RELEASE
 SUBDIRS =  src/Hyena src/Mono.Data.Sqlite src/Hyena.Data.Sqlite src/Hyena.Gui
endif
if ENABLE_WINDOWS
 SUBDIRS =  src/Hyena src/Mono.Data.Sqlite src/Hyena.Data.Sqlite src/Hyena.Gui
endif

if ENABLE_TESTS
TEST_ASSEMBLIES = \
	Hyena.dll \
	Hyena.Data.Sqlite.dll \
	Hyena.Gui.dll

ENV_OPTIONS = TZ=America/Chicago LC_ALL=it_IT LANG=it_IT
RUNNER = for asm in $${TEST_ASSEMBLIES}; do echo -e "\033[1mRunning tests on $${asm}...\033[0m"; $(ENV_OPTIONS) nunit-console2 -nologo -noshadow $$asm; done

test:
	@pushd bin &>/dev/null; \
	export TEST_ASSEMBLIES="$(TEST_ASSEMBLIES)"; $(RUNNER); \
	popd &>/dev/null;
else
test:
	echo "Tests not enabled.  Pass --enable-tests to configure or ./autogen.sh"
endif
