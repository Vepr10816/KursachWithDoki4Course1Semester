#pragma checksum "..\..\ClientOrderCreationPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "DC4E84C33C109D5B51A72D2D3BC4E158D0CA74CAD4DF870ECB5C32F440AE06CE"
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
    /// ClientOrderCreationPage
    /// </summary>
    public partial class ClientOrderCreationPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 12 "..\..\ClientOrderCreationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grdOrderCreate;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\ClientOrderCreationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgService;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\ClientOrderCreationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tblDescription;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\ClientOrderCreationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dpOrder;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\ClientOrderCreationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbCars;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\ClientOrderCreationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RichTextBox lbDescription;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\ClientOrderCreationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbPriceEnding;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\ClientOrderCreationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddOrder;
        
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
            System.Uri resourceLocater = new System.Uri("/AutoService;component/clientordercreationpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ClientOrderCreationPage.xaml"
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
            this.grdOrderCreate = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.dgService = ((System.Windows.Controls.DataGrid)(target));
            
            #line 18 "..\..\ClientOrderCreationPage.xaml"
            this.dgService.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.dgService_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.tblDescription = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.dpOrder = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 6:
            this.cbCars = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.lbDescription = ((System.Windows.Controls.RichTextBox)(target));
            return;
            case 8:
            this.lbPriceEnding = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.btnAddOrder = ((System.Windows.Controls.Button)(target));
            
            #line 68 "..\..\ClientOrderCreationPage.xaml"
            this.btnAddOrder.Click += new System.Windows.RoutedEventHandler(this.btnAddOrder_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 3:
            
            #line 38 "..\..\ClientOrderCreationPage.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Checked += new System.Windows.RoutedEventHandler(this.chDG_Checked);
            
            #line default
            #line hidden
            
            #line 38 "..\..\ClientOrderCreationPage.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Unchecked += new System.Windows.RoutedEventHandler(this.chDG_Unchecked);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

