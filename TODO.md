# Hyena for .NET Core
This is a TODO list of outstanding issues that should be resolved
at some point.

## Hyena Core
 - Builds
 - SafeUri doesn't work, as the filename conversions have been
   replaced with stub code (to remove GLib dependency)
 - Localisation is now handled through NGettext. Unfortunately, the
   library has been designed around Mono.Unix.Catalog so we need a
   static shim wrapper. Look at resolving this later.

## Hyena Sqlite
 - Builds (apparently successfully?)

## Hyena Gui
 - Ported ExtensionDialog.cs only
 - Uses gir.core/Gtk3
 - Incrementally port over library as gir.core improves

## Tests
 - Do not build
 - Probably broken