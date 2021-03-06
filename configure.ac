dnl Warning: This is an automatically generated file, do not edit!
dnl Process this file with autoconf to produce a configure script.
AC_PREREQ([2.54])
AC_INIT([tasque], [0.2.0])
AM_INIT_AUTOMAKE([foreign tar-ustar])
AM_MAINTAINER_MODE

m4_include([m4/GAC_CHECK_MODULES.m4])

GNOME_COMMON_INIT

AC_PROG_INSTALL
AC_PROG_LIBTOOL

dnl pkg-config
PKG_PROG_PKG_CONFIG

### Begin GAC tool ###
PKG_PROG_GACUTIL
if test -z "$GACUTIL"; then
        AC_MSG_ERROR([No gacutil tool found])
fi

GACUTIL_FLAGS='/gacdir $(DESTDIR)$(prefix)'
AC_SUBST(GACUTIL_FLAGS)
### End GAC tool ###

AC_PROG_INSTALL

AC_PATH_PROG(DMCS, dmcs, no)
if test "x$DMCS" = "xno"; then
        AC_MSG_ERROR([dmcs Not found])
fi

#
# Setup xbuild
#
AC_PATH_PROG(XBUILD, xbuild, no)
if test "x$XBUILD" = "xno"; then
		AC_MSG_ERROR([xbuild not found])
fi

#
# Setup i18n tools
#
AC_PATH_PROG(INTLTOOL_MERGE, intltool-merge, no)
if test "x$INTLTOOL_MERGE" = "xno"; then
		AC_MSG_ERROR([intltool-merge not found])
fi
AC_PATH_PROG(INTLTOOL_UPDATE, intltool-update, no)
if test "x$INTLTOOL_UPDATE" = "xno"; then
		AC_MSG_ERROR([intltool-update not found])
fi

#
# Setup GETTEXT
#

GETTEXT_PACKAGE=tasque
AC_SUBST(GETTEXT_PACKAGE)
AC_DEFINE_UNQUOTED(GETTEXT_PACKAGE, "$GETTEXT_PACKAGE", [Gettext package])
AM_GLIB_GNU_GETTEXT

PKG_CHECK_MODULES([GLIB_SHARP_20], [glib-sharp-2.0])
PKG_CHECK_MODULES([GTK_SHARP_20], [gtk-sharp-2.0 >= 2.10.0])
PKG_CHECK_MODULES([GTK_SHARP_2_12], gtk-sharp-2.0 >= 2.12.0, GTK_2_12="yes", GTK_2_12="no")
AM_CONDITIONAL(GTK_2_12, test "$GTK_2_12" = "yes")
PKG_CHECK_MODULES([DBUS_SHARP_10], [dbus-sharp-1.0])
PKG_CHECK_MODULES([DBUS_SHARP_GLIB_10], [dbus-sharp-glib-1.0])

#
# Check for Mono.Addins
#
PKG_CHECK_MODULES(MONO_ADDINS, mono-addins >= 0.6)

#
# Allow the project to build without notify-sharp
#
PKG_CHECK_MODULES(NOTIFY_SHARP, notify-sharp, enable_notify_sharp="yes", enable_notify_sharp="no")
AM_CONDITIONAL(ENABLE_NOTIFY_SHARP, test "x$enable_notify_sharp" != "xno")

#
# AppIndicator
# AppIndicator is enabled by default because it is an absolute requirement for the systray to
# work on Ubuntu/Unity. If AppIndicator is not available on a given system it must be switched off.
#
appindicator=yes
AC_ARG_ENABLE(appindicator,
	AC_HELP_STRING([--enable-appindicator],
		[Enables the use of Ubuntu/Unity's Application Indicator as systray icon [default=yes]]),
		appindicator=$enableval, )
if test "x$appindicator" != "xno" ; then
	PKG_CHECK_MODULES([APPINDICATOR_SHARP], [appindicator-sharp-0.1], appindicator="yes", appindicator="no")
	if test "$appindicator" == "no" ; then
		AC_MSG_WARN([Please install appindicator-sharp, if you want systray support on Ubuntu/Unity. \
If appindicator-sharp is not available on your system, you probably won't need it. To disable \
appindicator pass --enable-appindicator=no to configure."])
	fi
fi
AM_CONDITIONAL(ENABLE_APPINDICATOR, test "x$appindicator" != "xno")


AC_ARG_ENABLE(debug, AC_HELP_STRING([--enable-debug], [Use 'Debug' Configuration [default=no]]),
	enable_debug=$enableval, enable_debug=no)
AM_CONDITIONAL(ENABLE_DEBUG, test "x$enable_debug" != "xno")


#
# DEFINES uses for #if statements
#
AC_ARG_WITH(dbus_service_dir, [  --with-dbus-service-dir=DIR            Where to install Tasque's DBus service file.])
AM_CONDITIONAL(WITH_DBUS_SERVICE_DIR, test "x$with_dbus_service_dir" != "x")
if test "x$with_dbus_service_dir" != "x"; then
	DBUS_SERVICE_DIR=$with_dbus_service_dir
else
	DBUS_SERVICE_DIR=${datadir}/dbus-1/services
fi
AC_SUBST(DBUS_SERVICE_DIR)


#CSC_DEFINES=""
#if pkg-config --atleast-version=2.12 gtk-sharp-2.0; then
#	CSC_DEFINES="-d:GTK_2_12"
#fi
#AC_SUBST(CSC_DEFINES)

# Set default values for final_*
# This doesn't affect anything, as final_* values
# are always overridden. Just helps when printing the
# values at the end
final_backend_dummy=no
final_backend_eds=no
final_backend_icecore=no
final_backend_rtm=no
final_backend_sqlite=no
final_backend_hiveminder=no
final_experimental_backends=no

#
# All Backends = standard + experimental.
#
AC_ARG_ENABLE(all_backends,
	AC_HELP_STRING([--enable-all-backends],
		[Enable all backends, including standard and experimental [default=no]]),
		final_all_backends=$enableval, final_all_backends=no)
if test "x$final_all_backends" != "xno" ; then
   final_standard_backends=yes
   final_experimental_backends=yes
fi

#
# Standard Backends
#
AC_ARG_ENABLE(standard_backends,
	AC_HELP_STRING([--enable-standard-backends],
		[Enable standard (RTM, SQLite) backends. [default=yes]]),
		final_standard_backends=$enableval, final_standard_backends=yes)
if test "x$final_standard_backends" != "xno" ; then
   final_backend_rtm=yes
   final_backend_sqlite=yes
fi

#
# Experimental Backends
#
AC_ARG_ENABLE(experimental_backends,
	AC_HELP_STRING([--enable-experimental-backends],
		[Enable experimental (Hiveminder, ICECore) backends. [default=no]]),
		final_experimental_backends=$enableval, )
if test "x$final_experimental_backends" != "xno" ; then
   final_backend_hiveminder=yes
   final_backend_icecore=yes
fi

#
# Dummy Backend (for debugging)
#
AC_ARG_ENABLE(backend_dummy,
	AC_HELP_STRING([--enable-backend-dummy],
		[Enable the Dummy (Debug) Backend [default=if --enable-debug: yes; else: no]]),
		final_backend_dummy=$enableval, final_backend_dummy=$enable_debug)
AM_CONDITIONAL(ENABLE_BACKEND_DUMMY, test "x$final_backend_dummy" != "xno")

#
# Remember the Milk Backend
#
AC_ARG_ENABLE(backend_rtm,
	AC_HELP_STRING([--enable-backend-rtm],
		[Enable the RTM Backend [default=yes]]),
		final_backend_rtm=$enableval, )

#
# SQLite Backend
#
AC_ARG_ENABLE(backend_sqlite,
	AC_HELP_STRING([--enable-backend-sqlite],
		[Enable the SQLite Backend [default=no]]),
		final_backend_sqlite=$enableval, )
if test "x$final_backend_sqlite" != "xno"; then
	GAC_CHECK_MODULES([MONO_DATA_SQLITE], [Mono.Data.Sqlite])
fi
AM_CONDITIONAL(ENABLE_BACKEND_SQLITE, test "x$final_backend_sqlite" != "xno")

#
# ICEcore for IceBackend Support
#
AC_ARG_ENABLE(backend_icecore,
	AC_HELP_STRING([--enable-backend-icecore],
		[Enable the ICEcore Backend [default=no]]),
		final_backend_icecore=$enableval, )
AM_CONDITIONAL(ENABLE_BACKEND_ICECORE, test "x$final_backend_icecore" != "xno")
if test "x$final_backend_icecore" != "xno" ; then
       # FIXME : Is this the right way to do this ?
       PKG_CHECK_MODULES(ICE_DESKTOP, Novell.IceDesktop)
fi

#
# Evolution-Sharp for EDSBackend Support
#

AC_ARG_ENABLE(backend_eds,
       AC_HELP_STRING([--enable-backend-eds],
               [Enable the EDS Backend [default=no]]),
               final_backend_eds=$enableval, )
AM_CONDITIONAL(ENABLE_BACKEND_EDS, test "x$final_backend_eds" != "xno")
if test "x$final_backend_eds" != "xno" ; then
       # FIXME : Is this the right way to do this ?
       PKG_CHECK_MODULES(EVOLUTION_SHARP, evolution-sharp >= 0.18.1)
fi

#
# Hiveminder Backend
#
AC_ARG_ENABLE(backend_hiveminder,
	AC_HELP_STRING([--enable-backend-hiveminder],
		[Enable the Hiveminder Backend [default=no]]),
		final_backend_hiveminder=$enableval, )
AM_CONDITIONAL(ENABLE_BACKEND_HIVEMINDER, test "x$final_backend_hiveminder" != "xno")

#
# If no backends were enabled,
# enable the RTM Backend by default.
#
if test "x$final_backend_dummy" = "xno" -a "x$final_backend_rtm" = "xno" \
	-a "x$final_backend_sqlite" = "xno" -a "x$final_backend_icecore" = "xno" \
	-a "x$final_backend_eds" = "xno" -a "x$final_backend_hiveminder" = "xno"; then
	# No other backend was enabled, so enable
	# the RTM Backend by default
	final_backend_rtm=yes
fi
# Define ENABLE_BACKEND_RTM here so it only gets defined once
AM_CONDITIONAL(ENABLE_BACKEND_RTM, test "x$final_backend_rtm" != "xno")


AC_CONFIG_FILES([
Makefile
build/build.csproj:build/build.csproj
])

AC_OUTPUT

$XBUILD /target:bootstrap /property:TopSrcDir=$srcdir /property:TopBuildDir=. build/build.csproj


echo "
Configuration:

	Prefix:            ${prefix}
	Debug build:       ${enable_debug}
	Notification:      ${enable_notify_sharp}
	Dummy Backend:     ${final_backend_dummy}
	Evolution Backend: ${final_backend_eds}
	ICECore Backend:   ${final_backend_icecore}
	RTM Backend:       ${final_backend_rtm}
	SQLite Backend:    ${final_backend_sqlite}
	Hiveminder Backend: ${final_backend_hiveminder}
	AppIndicator:      ${appindicator}
"
