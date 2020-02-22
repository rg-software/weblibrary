// Copyright © 2010-2015 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using CefSharp.WinForms;
using System;
using System.Windows.Forms;
using CefSharp;

namespace WebLibrary
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            CefSharpSettings.SubprocessExitIfParentProcessClosed = true;    // close subprocesses if parent process exits first
            Cef.EnableHighDPISupport();
            Cef.Initialize(new CefSettings(), performDependencyCheck: true, browserProcessHandler: null);
            Application.Run(new MainForm());
        }
    }
}
