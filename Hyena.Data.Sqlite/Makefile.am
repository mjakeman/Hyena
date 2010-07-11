ASSEMBLY = Hyena.Data.Sqlite
TARGET = library
LINK = -r:Mono.Posix -r:System  -r:System.Core  -r:System.Data \
	   -r:$(DIR_BIN)/Mono.Data.Sqlite.dll \
	   -r:$(DIR_BIN)/Hyena.dll
SOURCES =  \
	Hyena.Data.Sqlite/DatabaseColumn.cs \
	Hyena.Data.Sqlite/DatabaseColumnAttribute.cs \
	Hyena.Data.Sqlite/HyenaSqliteArrayDataReader.cs \
	Hyena.Data.Sqlite/HyenaSqliteCommand.cs \
	Hyena.Data.Sqlite/HyenaSqliteConnection.cs \
	Hyena.Data.Sqlite/ICacheableDatabaseModel.cs \
	Hyena.Data.Sqlite/SqliteModelCache.cs \
	Hyena.Data.Sqlite/SqliteModelProvider.cs \
	Hyena.Data.Sqlite/SqliteUtils.cs \
	Hyena.Data.Sqlite/Tests/DbBoundType.cs \
	Hyena.Data.Sqlite/Tests/SqliteCommandTests.cs \
	Hyena.Data.Sqlite/Tests/SqliteModelProviderTests.cs \
	Hyena.Data.Sqlite/Tests/SqliteUtilTests.cs \
	Hyena.Metrics/DbSampleStore.cs \
	Hyena.Metrics/HttpPoster.cs \
	Hyena.Metrics/ISampleStore.cs \
	Hyena.Metrics/MemorySampleStore.cs \
	Hyena.Metrics/Metric.cs \
	Hyena.Metrics/MetricsCollection.cs \
	Hyena.Metrics/Sample.cs \
	Hyena.Metrics/Tests/MetricsTests.cs

include $(top_srcdir)/build/build.mk
