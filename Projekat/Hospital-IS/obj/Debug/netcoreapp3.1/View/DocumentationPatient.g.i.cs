﻿#pragma checksum "..\..\..\..\View\DocumentationPatient.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "049F2E9BC14DA814112F1C62B2C275651A196169"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Hospital_IS.View;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Hospital_IS.View {
    
    
    /// <summary>
    /// DocumentationPatient
    /// </summary>
    public partial class DocumentationPatient : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 64 "..\..\..\..\View\DocumentationPatient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Logout;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\..\View\DocumentationPatient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Home;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\..\View\DocumentationPatient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ReserveApp;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\..\View\DocumentationPatient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AllApp;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\..\View\DocumentationPatient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Doc;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.4.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Hospital-IS;component/view/documentationpatient.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\DocumentationPatient.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.4.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Logout = ((System.Windows.Controls.Button)(target));
            return;
            case 2:
            this.Home = ((System.Windows.Controls.Button)(target));
            
            #line 66 "..\..\..\..\View\DocumentationPatient.xaml"
            this.Home.Click += new System.Windows.RoutedEventHandler(this.home);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ReserveApp = ((System.Windows.Controls.Button)(target));
            
            #line 67 "..\..\..\..\View\DocumentationPatient.xaml"
            this.ReserveApp.Click += new System.Windows.RoutedEventHandler(this.reserveApp);
            
            #line default
            #line hidden
            return;
            case 4:
            this.AllApp = ((System.Windows.Controls.Button)(target));
            
            #line 68 "..\..\..\..\View\DocumentationPatient.xaml"
            this.AllApp.Click += new System.Windows.RoutedEventHandler(this.showAll);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Doc = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

