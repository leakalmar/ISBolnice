﻿#pragma checksum "..\..\..\..\View\AppointmentPatient.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0CC459CBEA70DB7411C88D575C3513E3745997F8"
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
    /// AppointmentPatient
    /// </summary>
    public partial class AppointmentPatient : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 64 "..\..\..\..\View\AppointmentPatient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Logout;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\..\View\AppointmentPatient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Home;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\..\View\AppointmentPatient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ReserveApp;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\..\View\AppointmentPatient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AllApp;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\..\View\AppointmentPatient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Doc;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\..\View\AppointmentPatient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox Doctors;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\..\..\View\AppointmentPatient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker Calendar;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\..\..\View\AppointmentPatient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button showApp;
        
        #line default
        #line hidden
        
        
        #line 106 "..\..\..\..\View\AppointmentPatient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid listOfAppointments;
        
        #line default
        #line hidden
        
        
        #line 115 "..\..\..\..\View\AppointmentPatient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button reserve;
        
        #line default
        #line hidden
        
        
        #line 124 "..\..\..\..\View\AppointmentPatient.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button change;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.3.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Hospital-IS;component/view/appointmentpatient.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\AppointmentPatient.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.3.0")]
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
            
            #line 66 "..\..\..\..\View\AppointmentPatient.xaml"
            this.Home.Click += new System.Windows.RoutedEventHandler(this.home);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ReserveApp = ((System.Windows.Controls.Button)(target));
            return;
            case 4:
            this.AllApp = ((System.Windows.Controls.Button)(target));
            
            #line 76 "..\..\..\..\View\AppointmentPatient.xaml"
            this.AllApp.Click += new System.Windows.RoutedEventHandler(this.allApp);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Doc = ((System.Windows.Controls.Button)(target));
            
            #line 77 "..\..\..\..\View\AppointmentPatient.xaml"
            this.Doc.Click += new System.Windows.RoutedEventHandler(this.showDoc);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Doctors = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.Calendar = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 8:
            this.showApp = ((System.Windows.Controls.Button)(target));
            
            #line 95 "..\..\..\..\View\AppointmentPatient.xaml"
            this.showApp.Click += new System.Windows.RoutedEventHandler(this.showAvailableApp);
            
            #line default
            #line hidden
            return;
            case 9:
            this.listOfAppointments = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 10:
            this.reserve = ((System.Windows.Controls.Button)(target));
            
            #line 115 "..\..\..\..\View\AppointmentPatient.xaml"
            this.reserve.Click += new System.Windows.RoutedEventHandler(this.reserveAppointment);
            
            #line default
            #line hidden
            return;
            case 11:
            this.change = ((System.Windows.Controls.Button)(target));
            
            #line 124 "..\..\..\..\View\AppointmentPatient.xaml"
            this.change.Click += new System.Windows.RoutedEventHandler(this.changeAppointmentButton);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

