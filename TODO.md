# Hyena for .NET Core
This is a TODO list of outstanding issues that should be resolved
at some point.

## Hyena Core
 - Builds
 - Localisation is now handled through NGettext. Unfortunately, the
   library has been designed around Mono.Unix.Catalog so we need a
   static shim wrapper. **Edit 30/10/20:** This might actually be incorrect,
   as Banshee implements a Catalog that fits this criteria. Look at porting
   Banshee's Catalog to Hyena and using for both.

## Hyena Sqlite
 - Builds (apparently successfully?)

## Hyena Gui
 - Ported ExtensionDialog.cs only
 - Contains some glue for handling stylesheets
 - Uses gir.core/Gtk3
 - Incrementally port over library as gir.core improves

## Tests
 - Do not build
 - Probably broken