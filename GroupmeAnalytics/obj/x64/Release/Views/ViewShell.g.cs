﻿#pragma checksum "C:\Users\Chris\documents\visual studio 2015\Projects\GroupmeAnalytics\GroupmeAnalytics\Views\ViewShell.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1572880416A28918729BFBC0235FE0AA"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GroupmeAnalytics.Views
{
    partial class ViewShell : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.mainSplitView = (global::Windows.UI.Xaml.Controls.SplitView)(target);
                }
                break;
            case 2:
                {
                    this.hamburgerButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 73 "..\..\..\Views\ViewShell.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.hamburgerButton).Click += this.GroupMenu_Expand;
                    #line default
                }
                break;
            case 3:
                {
                    this.GroupMenuPane = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 4:
                {
                    global::Windows.UI.Xaml.Controls.ListView element4 = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    #line 28 "..\..\..\Views\ViewShell.xaml"
                    ((global::Windows.UI.Xaml.Controls.ListView)element4).SelectionChanged += this.Menu_SelectionChanged;
                    #line default
                }
                break;
            case 5:
                {
                    this.SplitViewFrame = (global::Windows.UI.Xaml.Controls.Frame)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

