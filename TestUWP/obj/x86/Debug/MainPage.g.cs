﻿#pragma checksum "C:\Users\NolanBlew\Documents\Visual Studio 2015\Projects\TestUWP\TestUWP\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6D32A5040F33D79A4481129ACC27595D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestUWP
{
    partial class MainPage : 
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
                    this.MainGrid = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 2:
                {
                    this.ScrollViewer = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 3:
                {
                    global::Windows.UI.Xaml.Controls.Button element3 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 30 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)element3).Click += this.ButtonBase_OnClick;
                    #line default
                }
                break;
            case 4:
                {
                    this.Viewbox = (global::Windows.UI.Xaml.Controls.Viewbox)(target);
                    #line 24 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Viewbox)this.Viewbox).ManipulationDelta += this.Viewbox_OnManipulationDelta;
                    #line default
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

