﻿#pragma checksum "..\..\..\..\Views\SearchResultView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "FA148677D2A6B40564967B29754C039F02855C492F5DFBDF4CABFB7185E78C41"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using Music.ControlAssist;
using Music.Converters;
using Music.Views;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace Music.Views {
    
    
    /// <summary>
    /// SearchResultView
    /// </summary>
    public partial class SearchResultView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\..\..\Views\SearchResultView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabControl tab;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\..\Views\SearchResultView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid songDataGrid;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\Views\SearchResultView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTemplateColumn cz;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\..\Views\SearchResultView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTemplateColumn bt;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\..\Views\SearchResultView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTemplateColumn gs;
        
        #line default
        #line hidden
        
        
        #line 112 "..\..\..\..\Views\SearchResultView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTemplateColumn zj;
        
        #line default
        #line hidden
        
        
        #line 131 "..\..\..\..\Views\SearchResultView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTemplateColumn sj;
        
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
            System.Uri resourceLocater = new System.Uri("/Music;component/views/searchresultview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\SearchResultView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            this.tab = ((System.Windows.Controls.TabControl)(target));
            return;
            case 2:
            this.songDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 33 "..\..\..\..\Views\SearchResultView.xaml"
            this.songDataGrid.PreviewMouseWheel += new System.Windows.Input.MouseWheelEventHandler(this.songDataGrid_PreviewMouseWheel);
            
            #line default
            #line hidden
            return;
            case 3:
            this.cz = ((System.Windows.Controls.DataGridTemplateColumn)(target));
            return;
            case 4:
            this.bt = ((System.Windows.Controls.DataGridTemplateColumn)(target));
            return;
            case 5:
            this.gs = ((System.Windows.Controls.DataGridTemplateColumn)(target));
            return;
            case 6:
            this.zj = ((System.Windows.Controls.DataGridTemplateColumn)(target));
            return;
            case 7:
            this.sj = ((System.Windows.Controls.DataGridTemplateColumn)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

