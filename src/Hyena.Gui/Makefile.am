
EXTRA_DIST =  

# Warning: This is an automatically generated file, do not edit!

if ENABLE_DEBUG
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -unsafe -warn:4 -optimize+ -debug -define:DEBUG "-define:NET_2_0"
ASSEMBLY = ../../bin/Hyena.Gui.dll
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = library
PROJECT_REFERENCES =  \
	../../bin/Hyena.dll
BUILD_DIR = ../../bin

HYENA_DLL_SOURCE=../../bin/Hyena.dll
MONO_DATA_SQLITE_DLL_SOURCE=../../bin/Mono.Data.Sqlite.dll
MONO_DATA_SQLITE_DLL_MDB_SOURCE=../../bin/Mono.Data.Sqlite.dll.mdb
HYENA_DLL_MDB_SOURCE=../../bin/Hyena.dll.mdb
HYENA_GUI_DLL_MDB_SOURCE=../../bin/Hyena.Gui.dll.mdb

endif

if ENABLE_WINDOWS
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -unsafe -warn:4 -optimize- -debug -define:DEBUG
ASSEMBLY = ../../bin/Hyena.Gui.dll
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = library
PROJECT_REFERENCES =  \
	../../bin/Hyena.dll
BUILD_DIR = ../../bin

HYENA_DLL_SOURCE=../../bin/Hyena.dll
MONO_DATA_SQLITE_DLL_SOURCE=../../bin/Mono.Data.Sqlite.dll
MONO_DATA_SQLITE_DLL_MDB_SOURCE=../../bin/Mono.Data.Sqlite.dll.mdb
HYENA_DLL_MDB_SOURCE=../../bin/Hyena.dll.mdb
HYENA_GUI_DLL_MDB_SOURCE=../../bin/Hyena.Gui.dll.mdb

endif

AL=al2
SATELLITE_ASSEMBLY_NAME=$(notdir $(basename $(ASSEMBLY))).resources.dll

PROGRAMFILES = \
	$(HYENA_DLL) \
	$(MONO_DATA_SQLITE_DLL) \
	$(MONO_DATA_SQLITE_DLL_MDB) \
	$(HYENA_DLL_MDB) \
	$(HYENA_GUI_DLL_MDB)  

LINUX_PKGCONFIG = \
	$(HYENA_GUI_PC)  


RESGEN=resgen2
	
all: $(ASSEMBLY) $(PROGRAMFILES) $(LINUX_PKGCONFIG) 

FILES = \
	Hyena.Data.Gui/Column.cs \
	Hyena.Data.Gui/ColumnCell.cs \
	Hyena.Data.Gui/ColumnCellText.cs \
	Hyena.Data.Gui/ColumnController.cs \
	Hyena.Data.Gui/ColumnHeaderCellText.cs \
	Hyena.Data.Gui/SortableColumn.cs \
	Hyena.Gui/CleanRoomStartup.cs \
	Hyena.Gui.Dialogs/ExceptionDialog.cs \
	Hyena.Gui.Dialogs/VersionInformationDialog.cs \
	Hyena.Gui/CairoExtensions.cs \
	Hyena.Data.Gui/ObjectListView.cs \
	Hyena.Gui/EditableEraseAction.cs \
	Hyena.Gui/EditableInsertAction.cs \
	Hyena.Gui/EditableUndoAdapter.cs \
	Hyena.Gui/GtkUtilities.cs \
	Hyena.Data.Gui/CellContext.cs \
	Hyena.Data.Gui/IHeaderCell.cs \
	Hyena.Widgets/ScrolledWindow.cs \
	Hyena.Widgets/RoundedFrame.cs \
	Hyena.Gui.Theatrics/Actor.cs \
	Hyena.Gui.Theatrics/Stage.cs \
	Hyena.Gui.Theatrics/SingleActorStage.cs \
	Hyena.Query.Gui/DateQueryValueEntry.cs \
	Hyena.Query.Gui/IntegerQueryValueEntry.cs \
	Hyena.Query.Gui/QueryBox.cs \
	Hyena.Query.Gui/QueryTermBox.cs \
	Hyena.Query.Gui/QueryValueEntry.cs \
	Hyena.Query.Gui/StringQueryValueEntry.cs \
	Hyena.Data.Gui/ListView/ListView.cs \
	Hyena.Data.Gui/ListView/ListView_Windowing.cs \
	Hyena.Data.Gui/ListView/ListView_Rendering.cs \
	Hyena.Data.Gui/ListView/ListView_Header.cs \
	Hyena.Data.Gui/ListView/ListView_Model.cs \
	Hyena.Data.Gui/ListView/ListView_Interaction.cs \
	Hyena.Data.Gui/RowActivatedHandler.cs \
	Hyena.Query.Gui/FileSizeQueryValueEntry.cs \
	Hyena.Query.Gui/QueryLimitBox.cs \
	Hyena.Query.Gui/QueryTermsBox.cs \
	Hyena.Widgets/MessageBar.cs \
	Hyena.Widgets/AnimatedImage.cs \
	Hyena.Gui.Theming/Theme.cs \
	Hyena.Gui.Theming/GtkColors.cs \
	Hyena.Gui.Theming/ThemeContext.cs \
	Hyena.Gui.Theming/ThemeEngine.cs \
	Hyena.Gui.Theming/GtkTheme.cs \
	Hyena.Data.Gui/ListView/ListView_DragAndDrop.cs \
	Hyena.Gui/PangoCairoHelper.cs \
	Hyena.Widgets/AnimatedBox.cs \
	Hyena.Widgets/AnimatedHBox.cs \
	Hyena.Widgets/AnimatedVBox.cs \
	Hyena.Widgets/AnimatedWidget.cs \
	Hyena.Gui.Theatrics/Choreographer.cs \
	Hyena.Gui/ShadingTestWindow.cs \
	Hyena.Query.Gui/NullQueryValueEntry.cs \
	Hyena.Query.Gui/RelativeTimeSpanQueryValueEntry.cs \
	Hyena.Query.Gui/TimeSpanQueryValueEntry.cs \
	Hyena.Data.Gui/IListView.cs \
	Hyena.Widgets/SmoothScrolledWindow.cs \
	Hyena.Widgets/MenuButton.cs \
	Hyena.Gui/CompositeUtils.cs \
	Hyena.Gui/TooltipSetter.cs \
	Hyena.Widgets/WrapLabel.cs \
	Hyena.Query.Gui/QueryDebugger.cs \
	Hyena.Gui/TestModuleAttribute.cs \
	Hyena.Gui/TestModuleRunner.cs \
	Hyena.Widgets/SegmentedBar.cs \
	Hyena.Data.Gui/ListViewTestModule.cs \
	Hyena.Data.Gui/IInteractiveCell.cs \
	Hyena.Data.Gui/ColumnCellCheckBox.cs \
	Hyena.Data.Gui/ISizeRequestCell.cs \
	Hyena.Data.Gui/ITooltipCell.cs \
	Hyena.Data.Gui/ColumnCellRating.cs \
	Hyena.Widgets/RatingEntry.cs \
	Hyena.Widgets/RatingMenuItem.cs \
	Hyena.Widgets/ComplexMenuItem.cs \
	Hyena.Gui/RatingRenderer.cs \
	Hyena.Data.Gui/ListView/ListViewBase.cs \
	Hyena.Gui/GtkWorkarounds.cs \
	Hyena.Gui/Contrast.cs \
	Hyena.Widgets/ImageButton.cs \
	Hyena.Data.Gui/ITextCell.cs \
	Hyena.Gui.Theatrics/Tests/ChoreographerTests.cs \
	Hyena.Widgets/PulsingButton.cs \
	Hyena.Gui.Theatrics/Pulsator.cs \
	Hyena.Gui/PixbufImageSurface.cs \
	Hyena.Widgets/TextViewEditable.cs \
	Hyena.Gui/DragDropList.cs \
	Hyena.Gui/ActionManager.cs \
	Hyena.Gui/HyenaActionGroup.cs \
	Hyena.Widgets/GenericToolItem.cs \
	Hyena.Widgets/HigMessageDialog.cs 

DATA_FILES = 

RESOURCES = 

EXTRAS = \
	hyena.gui.pc.in 

REFERENCES =  \
	ICSharpCode.SharpZipLib \
	Mono.Posix \
	System \
	System.Core \
	$(GTK_SHARP_20_LIBS) \
	Mono.Cairo \
	$(GLIB_SHARP_20_LIBS) \
	System.Xml

DLL_REFERENCES = 

CLEANFILES = $(PROGRAMFILES) $(LINUX_PKGCONFIG) 

include $(top_srcdir)/Makefile.include

HYENA_DLL = $(BUILD_DIR)/Hyena.dll
MONO_DATA_SQLITE_DLL = $(BUILD_DIR)/Mono.Data.Sqlite.dll
MONO_DATA_SQLITE_DLL_MDB = $(BUILD_DIR)/Mono.Data.Sqlite.dll.mdb
HYENA_DLL_MDB = $(BUILD_DIR)/Hyena.dll.mdb
HYENA_GUI_DLL_MDB = $(BUILD_DIR)/Hyena.Gui.dll.mdb
HYENA_GUI_PC = $(BUILD_DIR)/hyena.gui.pc

$(eval $(call emit-deploy-wrapper,HYENA_GUI_PC,hyena.gui.pc))


$(eval $(call emit_resgen_targets))
$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'

$(ASSEMBLY) $(ASSEMBLY_MDB): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list) $(build_satellite_assembly_list)
	mkdir -p $(shell dirname $(ASSEMBLY))
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)
