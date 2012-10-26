// Permission is hereby granted, free of charge, to any person obtaining 
// a copy of this software and associated documentation files (the 
// "Software"), to deal in the Software without restriction, including 
// without limitation the rights to use, copy, modify, merge, publish, 
// distribute, sublicense, and/or sell copies of the Software, and to 
// permit persons to whom the Software is furnished to do so, subject to 
// the following conditions: 
//  
// The above copyright notice and this permission notice shall be 
// included in all copies or substantial portions of the Software. 
//  
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, 
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND 
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE 
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION 
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION 
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
// 
// Copyright (c) 2008 Novell, Inc. (http://www.novell.com) 
// 
// Authors: 
//      Sandy Armstrong <sanfordarmstrong@gmail.com>
// 


using System;
using System.IO;

namespace Tasque
{
	public class GtkApplication : INativeApplication
	{
		#region INativeApplication implementation 

		private string confDir;

		public virtual event EventHandler ExitingEvent;

		public GtkApplication ()
		{
			confDir = Path.Combine (
				Environment.GetFolderPath (
				Environment.SpecialFolder.ApplicationData),
				"tasque");
			if (!Directory.Exists (confDir))
				Directory.CreateDirectory (confDir);
		}
		
		public virtual void Initialize (string locale_dir, string display_name, string process_name, string[] args)
		{
			Mono.Unix.Catalog.Init ("tasque", Defines.LocaleDir);

			Gtk.Application.Init ();

			// add package icon path to default icon theme search paths
			Gtk.IconTheme.Default.PrependSearchPath (Defines.IconsDir);
#if WIN && DEBUG
			Gtk.IconTheme.Default.PrependSearchPath (Defines.DataDir + "\\icons");
#endif
		}

		public virtual void InitializeIdle ()
		{
		}
		
		public virtual void Exit (int exitcode)
		{
			if (ExitingEvent != null)
				ExitingEvent (null, new EventArgs ());
			System.Environment.Exit (exitcode);
		}
		
		public virtual void StartMainLoop ()
		{
			Gtk.Application.Run ();
		}
		
		public virtual void QuitMainLoop ()
		{
			Gtk.Application.Quit ();
		}

		public virtual string ConfDir
		{
			get
			{
				return confDir;
			}
		}

		public virtual void OpenUrl (string url)
		{
			try {
				System.Diagnostics.Process.Start (url);
			} catch (Exception e) {
				Logger.Error ("Error opening url [{0}]:\n{1}", url, e.ToString ());
			}
		}

		#endregion
	}
}
