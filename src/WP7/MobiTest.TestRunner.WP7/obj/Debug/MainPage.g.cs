﻿#pragma checksum "D:\@SRC\MobiTest\src\WP7\MobiTest.TestRunner.WP7\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "48722C1B90431C9D6E49E3A007D9BE31"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace MobiTest.TestRunner.WP7 {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.ListBox AllTestsListBox;
        
        internal System.Windows.Controls.ListBox FailingTestsListBox;
        
        internal System.Windows.Controls.ListBox PassingTestsListBox;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/MobiTest.TestRunner.WP7;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.AllTestsListBox = ((System.Windows.Controls.ListBox)(this.FindName("AllTestsListBox")));
            this.FailingTestsListBox = ((System.Windows.Controls.ListBox)(this.FindName("FailingTestsListBox")));
            this.PassingTestsListBox = ((System.Windows.Controls.ListBox)(this.FindName("PassingTestsListBox")));
        }
    }
}

