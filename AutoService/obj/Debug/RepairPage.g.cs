#pragma checksum "..\..\RepairPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "75561A8E2E025A31149A9A3FEE53E0FBEE2935BAD934D01F17E7F4EFCA69AB6A"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using AutoService;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace AutoService {
    
    
    /// <summary>
    /// RepairPage
    /// </summary>
    public partial class RepairPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\RepairPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grdRepair;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\RepairPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgContracts;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\RepairPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgDiagnostics;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\RepairPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbStatusName;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\RepairPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbTimeStatus;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\RepairPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddStatus;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\RepairPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDelOrder;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/AutoService;component/repairpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\RepairPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.grdRepair = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.dgContracts = ((System.Windows.Controls.DataGrid)(target));
            
            #line 23 "..\..\RepairPage.xaml"
            this.dgContracts.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.dgContracts_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.dgDiagnostics = ((System.Windows.Controls.DataGrid)(target));
            
            #line 34 "..\..\RepairPage.xaml"
            this.dgDiagnostics.RowEditEnding += new System.EventHandler<System.Windows.Controls.DataGridRowEditEndingEventArgs>(this.dgDiagnostics_RowEditEnding);
            
            #line default
            #line hidden
            
            #line 34 "..\..\RepairPage.xaml"
            this.dgDiagnostics.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.dgDiagnostics_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.tbStatusName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.lbTimeStatus = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.btnAddStatus = ((System.Windows.Controls.Button)(target));
            
            #line 85 "..\..\RepairPage.xaml"
            this.btnAddStatus.Click += new System.Windows.RoutedEventHandler(this.btnAddStatus_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnDelOrder = ((System.Windows.Controls.Button)(target));
            
            #line 86 "..\..\RepairPage.xaml"
            this.btnDelOrder.Click += new System.Windows.RoutedEventHandler(this.btnDelOrder_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

