dnl Warning: This is an automatically generated file, do not edit!
dnl Process this file with autoconf to produce a configure script.
AC_PREREQ([2.54])
AC_INIT([hyena], [0.1])
AM_INIT_AUTOMAKE([foreign dist-bzip2 tar-ustar])
AM_MAINTAINER_MODE

dnl pkg-config
AC_PATH_PROG(PKG_CONFIG, pkg-config, no)
if test "x$PKG_CONFIG" = "xno"; then
        AC_MSG_ERROR([You need to install pkg-config])
fi

m4_include([expansions.m4])

SHAMROCK_EXPAND_LIBDIR
SHAMROCK_EXPAND_BINDIR
SHAMROCK_EXPAND_DATADIR

AC_PROG_INSTALL

AC_PATH_PROG(MCS, gmcs, no)
if test "x$MCS" = "xno"; then
        AC_MSG_ERROR([gmcs Not found])
fi


AC_ARG_ENABLE(debug,
	AC_HELP_STRING([--enable-debug],
		[Use 'DEBUG' Configuration [default=YES]]),
		enable_debug=yes, enable_debug=no)
AM_CONDITIONAL(ENABLE_DEBUG, test x$enable_debug = xyes)
if test "x$enable_debug" = "xyes" ; then
	CONFIG_REQUESTED="yes"
fi
AC_ARG_ENABLE(release,
	AC_HELP_STRING([--enable-release],
		[Use 'RELEASE' Configuration [default=NO]]),
		enable_release=yes, enable_release=no)
AM_CONDITIONAL(ENABLE_RELEASE, test x$enable_release = xyes)
if test "x$enable_release" = "xyes" ; then
	CONFIG_REQUESTED="yes"
fi
AC_ARG_ENABLE(windows,
	AC_HELP_STRING([--enable-windows],
		[Use 'WINDOWS' Configuration [default=NO]]),
		enable_windows=yes, enable_windows=no)
AM_CONDITIONAL(ENABLE_WINDOWS, test x$enable_windows = xyes)
if test "x$enable_windows" = "xyes" ; then
	CONFIG_REQUESTED="yes"
fi
if test -z "$CONFIG_REQUESTED" ; then
	AM_CONDITIONAL(ENABLE_DEBUG, true)
	enable_debug=yes
fi


dnl package checks, common for all configs
PKG_CHECK_MODULES([GTK_SHARP_20], [gtk-sharp-2.0])
PKG_CHECK_MODULES([GLIB_SHARP_20], [glib-sharp-2.0])

dnl package checks, per config


AC_CONFIG_FILES([
src/Mono.Data.Sqlite/Makefile
src/Hyena/hyena.pc
src/Hyena/Makefile
src/Hyena.Data.Sqlite/hyena.data.sqlite.pc
src/Hyena.Data.Sqlite/Makefile
src/Hyena.Gui/hyena.gui.pc
src/Hyena.Gui/Makefile
Makefile

])

AC_OUTPUT
