
EXTRA_DIST =  

# Warning: This is an automatically generated file, do not edit!

if ENABLE_DEBUG
ASSEMBLY_COMPILER_COMMAND = $(MCS)
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize+ -debug -define:DEBUG "-define:NET_2_0"
ASSEMBLY = ../../bin/Hyena.Data.Sqlite.dll
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = library
PROJECT_REFERENCES =  \
	../../bin/Mono.Data.Sqlite.dll \
	../../bin/Hyena.dll
BUILD_DIR = ../../bin

MONO_DATA_SQLITE_DLL_SOURCE=../../bin/Mono.Data.Sqlite.dll
MONO_DATA_SQLITE_DLL_MDB_SOURCE=../../bin/Mono.Data.Sqlite.dll.mdb
HYENA_DLL_SOURCE=../../bin/Hyena.dll
HYENA_DLL_MDB_SOURCE=../../bin/Hyena.dll.mdb
HYENA_DATA_SQLITE_DLL_MDB_SOURCE=../../bin/Hyena.Data.Sqlite.dll.mdb

endif

if ENABLE_WINDOWS
ASSEMBLY_COMPILER_COMMAND = $(MCS)
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize- -debug -define:DEBUG
ASSEMBLY = ../../bin/Hyena.Data.Sqlite.dll
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = library
PROJECT_REFERENCES =  \
	../../bin/Mono.Data.Sqlite.dll \
	../../bin/Hyena.dll
BUILD_DIR = ../../bin

MONO_DATA_SQLITE_DLL_SOURCE=../../bin/Mono.Data.Sqlite.dll
MONO_DATA_SQLITE_DLL_MDB_SOURCE=../../bin/Mono.Data.Sqlite.dll.mdb
HYENA_DLL_SOURCE=../../bin/Hyena.dll
HYENA_DLL_MDB_SOURCE=../../bin/Hyena.dll.mdb
HYENA_DATA_SQLITE_DLL_MDB_SOURCE=../../bin/Hyena.Data.Sqlite.dll.mdb

endif

AL=al2
SATELLITE_ASSEMBLY_NAME=$(notdir $(basename $(ASSEMBLY))).resources.dll

PROGRAMFILES = \
	$(HYENA_DATA_SQLITE_DLL_MDB)  

LINUX_PKGCONFIG = \
	$(HYENA_DATA_SQLITE_PC)  


RESGEN=resgen2
	
all: $(ASSEMBLY) $(PROGRAMFILES) $(LINUX_PKGCONFIG) 

FILES = \
	Hyena.Data.Sqlite/DatabaseColumn.cs \
	Hyena.Data.Sqlite/DatabaseColumnAttribute.cs \
	Hyena.Data.Sqlite/SqliteModelProvider.cs \
	Hyena.Data.Sqlite/HyenaSqliteCommand.cs \
	Hyena.Data.Sqlite/HyenaSqliteConnection.cs \
	Hyena.Data.Sqlite/ICacheableDatabaseModel.cs \
	Hyena.Data.Sqlite/SqliteModelCache.cs \
	Hyena.Data.Sqlite/SqliteUtils.cs \
	Hyena.Data.Sqlite/Tests/DbBoundType.cs \
	Hyena.Data.Sqlite/Tests/SqliteCommandTests.cs \
	Hyena.Data.Sqlite/Tests/SqliteModelProviderTests.cs \
	Hyena.Data.Sqlite/HyenaSqliteArrayDataReader.cs \
	Hyena.Data.Sqlite/Tests/SqliteUtilTests.cs

DATA_FILES = 

RESOURCES = 

EXTRAS = \
	hyena.data.sqlite.pc.in 

REFERENCES =  \
	Mono.Posix \
	System \
	System.Core \
	System.Data \
	System.Xml

DLL_REFERENCES = 

CLEANFILES = $(PROGRAMFILES) $(LINUX_PKGCONFIG) 

include $(top_srcdir)/Makefile.include

MONO_DATA_SQLITE_DLL = $(BUILD_DIR)/Mono.Data.Sqlite.dll
MONO_DATA_SQLITE_DLL_MDB = $(BUILD_DIR)/Mono.Data.Sqlite.dll.mdb
HYENA_DLL = $(BUILD_DIR)/Hyena.dll
HYENA_DLL_MDB = $(BUILD_DIR)/Hyena.dll.mdb
HYENA_DATA_SQLITE_DLL_MDB = $(BUILD_DIR)/Hyena.Data.Sqlite.dll.mdb
HYENA_DATA_SQLITE_PC = $(BUILD_DIR)/hyena.data.sqlite.pc

$(eval $(call emit-deploy-wrapper,HYENA_DATA_SQLITE_PC,hyena.data.sqlite.pc))


$(eval $(call emit_resgen_targets))
$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'

$(ASSEMBLY_MDB): $(ASSEMBLY)

$(ASSEMBLY): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list) $(build_satellite_assembly_list)
	mkdir -p $(shell dirname $(ASSEMBLY))
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)
