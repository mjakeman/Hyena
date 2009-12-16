ASSEMBLY = Hyena.Gui
ASSEMBLY_BUILD_FLAGS = -unsafe
TARGET = library
LINK = -r:ICSharpCode.SharpZipLib -r:Mono.Posix -r:System -r:System.Core -r:Mono.Cairo \
	   $(GTKSHARP_LIBS) $(GLIBSHARP_LIBS) \
	   -r:$(DIR_BIN)/Hyena.dll
SOURCES =  \
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
	Hyena.Data.Gui/DataViewLayoutStyle.cs \
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
	Hyena.Gui/BaseWidgetAccessible.cs \
	Hyena.Widgets/EntryPopup.cs \
	Hyena.Widgets/SimpleTable.cs \
	Hyena.Gui/PangoCairoHelper.cs \
	Hyena.Gui/PangoExtensions.cs \
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
	Hyena.Data.Gui/Accessibility/ColumnCellAccessible.cs \
	Hyena.Data.Gui/Accessibility/ColumnCellTextAccessible.cs \
	Hyena.Data.Gui/Accessibility/ColumnHeaderCellTextAccessible.cs \
	Hyena.Data.Gui/Accessibility/ICellAccessibleParent.cs \
	Hyena.Data.Gui/Accessibility/ListViewAccessible.cs \
	Hyena.Data.Gui/Accessibility/ListViewAccessible_Selection.cs \
	Hyena.Data.Gui/Accessibility/ListViewAccessible_Table.cs \
	Hyena.Data.Gui/ListView/ListView_Accessible.cs \
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

include $(top_srcdir)/build/build.mk
EXTRA_DIST += hyena.gui.pc.in Hyena.Gui.dll.config
module_SCRIPTS += Hyena.Gui.dll.config
