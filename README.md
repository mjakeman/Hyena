# Hyena
A utility library primarily used by Banshee.

This is an attempt at porting Hyena to .NET Core and removing mono
from the codebase.

Please note, this port is *very* incomplete and much of it
does not compile. The `Hyena.Tests` directory doesn't actually
work, and is just a dumping ground for now. I will look at rewriting
the tests for NUnit 3 in the future.

See `TODO.md` for current status.

Adds `Hyena.Addins` as stub code for `Mono.Addins`. I would like to avoid
maintaining an in-tree fork of mono-addins if possible. Perhaps we could
look at porting to another addin framework, using .NET Core's assembly
loading code directly?

The original README can be seen below:

```txt
This is a library of useful GUI and non-GUI C# code, originally used in Banshee.

NOTE
** It is not API stable, and so is not installed to the GAC.**

There are three ways to use Hyena in your app:

1) Require it as an external dep; copy its .dll files into your project

   Applications using it should make a local copy of whatever components you use.
   That is, you should consider Hyena a build-time dependency, not a run-time, since
   at run-time your app will contain a copy of Hyena.

   There are variables defined in the pkg-config files that contain assemblies 
   and files needed for a given component of Hyena (eg hyena, hyena.data.sqlite, 
   and hyena.gui).

   pkg-config --variable=Assemblies hyena
   pkg-config --variable=Files hyena
   
   You can look at PDF Mod for an example of how to use Hyena:

   http://git.gnome.org/cgit/pdfmod/tree/configure.ac
   http://git.gnome.org/cgit/pdfmod/tree/Makefile.am
   http://git.gnome.org/cgit/pdfmod/tree/src/Makefile.am

2) Include it as a submodule in your git repo

   This is advantageous if you want to closely track and maybe contribute
   back to Hyena.  It also means developers don't have to install Hyena
   themselves from packages or git.

     git submodule add git://git.gnome.org/hyena lib/Hyena
     git submodule update --init
     git add .gitmodules

   Then you'll need to add Hyena to your build system.  See Banshee's setup:

     http://git.gnome.org/cgit/banshee/tree/configure.ac
     http://git.gnome.org/cgit/banshee/tree/Makefile.am

   You can also include the appropriate .csproj in your .sln.  Set them to
   build under the 'Submodule' configuration, and the binaries will get
   outputted to ../../bin from the Hyena checkout directory.

3) Bundle the .dll files in your project

   It's an expedient, but not good form for FOSS projects.
```