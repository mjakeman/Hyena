
EXTRA_DIST =  

# Warning: This is an automatically generated file, do not edit!

if ENABLE_DEBUG
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize+ -debug -define:DEBUG "-define:NET_2_0"
ASSEMBLY = ../../bin/Hyena.dll
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = library
PROJECT_REFERENCES =  \
	../../bin/Mono.Data.Sqlite.dll
BUILD_DIR = ../../bin

MONO_DATA_SQLITE_DLL_SOURCE=../../bin/Mono.Data.Sqlite.dll
MONO_DATA_SQLITE_DLL_MDB_SOURCE=../../bin/Mono.Data.Sqlite.dll.mdb
HYENA_DLL_MDB_SOURCE=../../bin/Hyena.dll.mdb

endif

if ENABLE_WINDOWS
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize- -debug -define:DEBUG
ASSEMBLY = ../../bin/Hyena.dll
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = library
PROJECT_REFERENCES =  \
	../../bin/Mono.Data.Sqlite.dll
BUILD_DIR = ../../bin

MONO_DATA_SQLITE_DLL_SOURCE=../../bin/Mono.Data.Sqlite.dll
MONO_DATA_SQLITE_DLL_MDB_SOURCE=../../bin/Mono.Data.Sqlite.dll.mdb
HYENA_DLL_MDB_SOURCE=../../bin/Hyena.dll.mdb

endif

AL=al2
SATELLITE_ASSEMBLY_NAME=$(notdir $(basename $(ASSEMBLY))).resources.dll

PROGRAMFILES = \
	$(MONO_DATA_SQLITE_DLL) \
	$(MONO_DATA_SQLITE_DLL_MDB) \
	$(HYENA_DLL_MDB)  

LINUX_PKGCONFIG = \
	$(HYENA_PC)  


RESGEN=resgen2
	
all: $(ASSEMBLY) $(PROGRAMFILES) $(LINUX_PKGCONFIG) 

FILES = \
	Hyena.Data/BaseListModel.cs \
	Hyena.Data/ICareAboutView.cs \
	Hyena.Data/IFilterable.cs \
	Hyena.Data/IListModel.cs \
	Hyena.Data/ISortable.cs \
	Hyena.Data/ISortableColumn.cs \
	Hyena.Data/ModelSelection.cs \
	Hyena.Data/SortType.cs \
	Hyena.Query/QueryListNode.cs \
	Hyena.Query/QueryNode.cs \
	Hyena.Query/QueryParser.cs \
	Hyena.Query/QueryTermNode.cs \
	Hyena.Query/QueryToken.cs \
	Hyena.Data/PropertyStore.cs \
	Hyena.CommandLine/Layout.cs \
	Hyena.CommandLine/LayoutOption.cs \
	Hyena.CommandLine/LayoutGroup.cs \
	Hyena.CommandLine/CommandLineParser.cs \
	Hyena.Data/IPropertyStoreExpose.cs \
	Hyena/IUndoAction.cs \
	Hyena/UndoManager.cs \
	Hyena.Data/ColumnDescription.cs \
	Hyena.Data/ICacheableModel.cs \
	Hyena.Collections/RangeCollection.cs \
	Hyena.Collections/IStackProvider.cs \
	Hyena.Collections/IntervalHeap.cs \
	Hyena.Collections/Selection.cs \
	Hyena.Collections/SelectionProxy.cs \
	Hyena.Query/QueryField.cs \
	Hyena.Query/UserQueryParser.cs \
	Hyena.Query/XmlQueryParser.cs \
	Hyena.Query/QueryFieldSet.cs \
	Hyena.Data.Sqlite/DatabaseColumn.cs \
	Hyena.Data.Sqlite/DatabaseColumnAttribute.cs \
	Hyena.Data.Sqlite/SqliteModelProvider.cs \
	Hyena.Data.Sqlite/HyenaSqliteCommand.cs \
	Hyena.Data.Sqlite/HyenaSqliteConnection.cs \
	Hyena.Data.Sqlite/ICacheableDatabaseModel.cs \
	Hyena.Data/ModelCache.cs \
	Hyena.Data.Sqlite/SqliteModelCache.cs \
	Hyena.Query/DateQueryValue.cs \
	Hyena.Query/FileSizeQueryValue.cs \
	Hyena.Query/IntegerQueryValue.cs \
	Hyena.Query/QueryOperator.cs \
	Hyena.Query/QueryValue.cs \
	Hyena.Query/StringQueryValue.cs \
	Hyena/Timer.cs \
	Hyena/DateTimeUtil.cs \
	Hyena.SExpEngine/ArithmeticFunctionSet.cs \
	Hyena.SExpEngine/CastFunctionSet.cs \
	Hyena.SExpEngine/CompareFunctionSet.cs \
	Hyena.SExpEngine/ControlFunctionSet.cs \
	Hyena.SExpEngine/Evaluator.cs \
	Hyena.SExpEngine/EvaluatorBase.cs \
	Hyena.SExpEngine/FunctionFunctionSet.cs \
	Hyena.SExpEngine/FunctionNode.cs \
	Hyena.SExpEngine/FunctionSet.cs \
	Hyena.SExpEngine/ListFunctionSet.cs \
	Hyena.SExpEngine/Literals.cs \
	Hyena.SExpEngine/LogicFunctionSet.cs \
	Hyena.SExpEngine/Parser.cs \
	Hyena.SExpEngine/StringFunctionSet.cs \
	Hyena.SExpEngine/TreeNode.cs \
	Hyena.SExpEngine/UtilityFunctionSet.cs \
	Hyena.Data/ArrayModelCache.cs \
	Hyena.Data/DictionaryModelCache.cs \
	Hyena.Query/AliasedObjectSet.cs \
	Hyena/StringUtil.cs \
	Hyena.Query/QueryLimit.cs \
	Hyena.Query/QueryOrder.cs \
	Hyena.Data.Sqlite/SqliteUtils.cs \
	Hyena/Log.cs \
	Hyena/CryptoUtil.cs \
	Hyena/PlatformUtil.cs \
	Hyena.Query/IntegerKeyedObjectQueryValue.cs \
	Hyena/ConsoleCrayon.cs \
	Hyena.Data/ISelectable.cs \
	Hyena.Query/NullQueryValue.cs \
	Hyena.Query/RelativeTimeSpanQueryValue.cs \
	Hyena.Query/TimeSpanQueryValue.cs \
	Hyena.Collections/Tests/IntervalHeapTests.cs \
	Hyena.Collections/Tests/RangeCollectionTests.cs \
	Hyena.Data.Sqlite/Tests/DbBoundType.cs \
	Hyena.Data.Sqlite/Tests/SqliteCommandTests.cs \
	Hyena.Data.Sqlite/Tests/SqliteModelProviderTests.cs \
	Hyena.Query/Tests/QueryTests.cs \
	Hyena/Tests/CryptoUtilTests.cs \
	Hyena/Tests/StringUtilTests.cs \
	Hyena/Tests/TestBase.cs \
	Hyena.Data/ICacheableItem.cs \
	Hyena.Collections/CollectionExtensions.cs \
	Hyena.Collections/QueuePipeline.cs \
	Hyena.Collections/Tests/QueuePipelineTests.cs \
	Hyena.Collections/QueuePipelineElement.cs \
	Hyena.Collections/WriteLineElement.cs \
	Hyena.Json/Tokenizer.cs \
	Hyena.Json/Token.cs \
	Hyena.Json/JsonObject.cs \
	Hyena.Json/JsonArray.cs \
	Hyena.Json/Tests/TokenizerTests.cs \
	Hyena.Json/TokenType.cs \
	Hyena.Json/Deserializer.cs \
	Hyena.Json/IJsonCollection.cs \
	Hyena.Json/Tests/DeserializerTests.cs \
	Hyena/Delegates.cs \
	Hyena.Collections/LruCache.cs \
	Hyena.Data.Sqlite/HyenaSqliteArrayDataReader.cs \
	Hyena.Data.Sqlite/Tests/SqliteUtilTests.cs \
	Hyena.Query/ExactStringQueryValue.cs \
	Hyena.Jobs/Job.cs \
	Hyena.Jobs/JobExtensions.cs \
	Hyena.Jobs/PriorityHints.cs \
	Hyena.Jobs/Resource.cs \
	Hyena.Jobs/Scheduler.cs \
	Hyena.Jobs/SimpleAsyncJob.cs \
	Hyena.Jobs/Tests/SchedulerTests.cs \
	Hyena/ApplicationContext.cs \
	Hyena/ThreadAssist.cs \
	Hyena/XdgBaseDirectorySpec.cs 

DATA_FILES = 

RESOURCES = 

EXTRAS = \
	hyena.pc.in 

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
HYENA_DLL_MDB = $(BUILD_DIR)/Hyena.dll.mdb
HYENA_PC = $(BUILD_DIR)/hyena.pc

$(eval $(call emit-deploy-wrapper,HYENA_PC,hyena.pc))


$(eval $(call emit_resgen_targets))
$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'

$(ASSEMBLY) $(ASSEMBLY_MDB): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list) $(build_satellite_assembly_list)
	mkdir -p $(shell dirname $(ASSEMBLY))
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)
