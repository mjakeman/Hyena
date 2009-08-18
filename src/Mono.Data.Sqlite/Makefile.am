
EXTRA_DIST =  

# Warning: This is an automatically generated file, do not edit!

if ENABLE_DEBUG
ASSEMBLY_COMPILER_COMMAND = $(MCS)
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -unsafe -warn:4 -optimize- -debug -define:DEBUG "-define:NET_2_0;MONO_BACKWARD_COMPAT"
ASSEMBLY = ../../bin/Mono.Data.Sqlite.dll
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = library
PROJECT_REFERENCES = 
BUILD_DIR = ../../bin

MONO_DATA_SQLITE_DLL_MDB_SOURCE=../../bin/Mono.Data.Sqlite.dll.mdb

endif

if ENABLE_WINDOWS
ASSEMBLY_COMPILER_COMMAND = $(MCS)
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -unsafe -warn:4 -optimize- -debug -define:DEBUG
ASSEMBLY = ../../bin/Mono.Data.Sqlite.dll
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = library
PROJECT_REFERENCES = 
BUILD_DIR = ../../bin

MONO_DATA_SQLITE_DLL_MDB_SOURCE=../../bin/Mono.Data.Sqlite.dll.mdb

endif

AL=al2
SATELLITE_ASSEMBLY_NAME=$(notdir $(basename $(ASSEMBLY))).resources.dll

PROGRAMFILES = \
	$(MONO_DATA_SQLITE_DLL_MDB)  

LINUX_PKGCONFIG = 


RESGEN=resgen2
	
all: $(ASSEMBLY) $(PROGRAMFILES) $(LINUX_PKGCONFIG) 

FILES = \
	Mono.Data.Sqlite/SQLite3.cs \
	Mono.Data.Sqlite/SQLite3_UTF16.cs \
	Mono.Data.Sqlite/SQLiteBase.cs \
	Mono.Data.Sqlite/SQLiteCommand.cs \
	Mono.Data.Sqlite/SQLiteCommandBuilder.cs \
	Mono.Data.Sqlite/SQLiteConnection.cs \
	Mono.Data.Sqlite/SQLiteConnectionStringBuilder.cs \
	Mono.Data.Sqlite/SQLiteConvert.cs \
	Mono.Data.Sqlite/SQLiteDataAdapter.cs \
	Mono.Data.Sqlite/SQLiteDataReader.cs \
	Mono.Data.Sqlite/SQLiteEnlistment.cs \
	Mono.Data.Sqlite/SQLiteException.cs \
	Mono.Data.Sqlite/SQLiteFactory.cs \
	Mono.Data.Sqlite/SQLiteFunction.cs \
	Mono.Data.Sqlite/SQLiteFunctionAttribute.cs \
	Mono.Data.Sqlite/SQLiteMetaDataCollectionNames.cs \
	Mono.Data.Sqlite/SQLiteParameter.cs \
	Mono.Data.Sqlite/SQLiteParameterCollection.cs \
	Mono.Data.Sqlite/SQLiteStatement.cs \
	Mono.Data.Sqlite/SQLiteTransaction.cs \
	Mono.Data.Sqlite/SR.Designer.cs \
	Mono.Data.Sqlite/SqliteDataSourceEnumerator.cs \
	Mono.Data.Sqlite/UnsafeNativeMethods.cs 

DATA_FILES = 

RESOURCES = 

EXTRAS =

REFERENCES =  \
	System \
	System.Data \
	System.Transactions \
	System.Xml

DLL_REFERENCES = 

CLEANFILES = $(PROGRAMFILES) $(LINUX_PKGCONFIG) 

include $(top_srcdir)/Makefile.include

MONO_DATA_SQLITE_DLL_MDB = $(BUILD_DIR)/Mono.Data.Sqlite.dll.mdb


$(eval $(call emit_resgen_targets))
$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'

$(ASSEMBLY) $(ASSEMBLY_MDB): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list) $(build_satellite_assembly_list)
	mkdir -p $(shell dirname $(ASSEMBLY))
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)
