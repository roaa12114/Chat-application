﻿#pragma checksum "..\..\..\ClientsInteraction.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "42C6BD87089E998EEFC75915F7A5C9C4F5383538"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using AllChatClient;
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


namespace AllChatClient {
    
    
    /// <summary>
    /// ClientsInteraction
    /// </summary>
    public partial class ClientsInteraction : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 31 "..\..\..\ClientsInteraction.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock usernameTextBlock;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\ClientsInteraction.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock listTextBlock;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\ClientsInteraction.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox messageTextBox;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\ClientsInteraction.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox onlineUsersListBox;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\ClientsInteraction.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock writeYourMessageHere;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\..\ClientsInteraction.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox writeMessageTextBox;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\..\ClientsInteraction.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button sendButton;
        
        #line default
        #line hidden
        
        
        #line 125 "..\..\..\ClientsInteraction.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addFileButton;
        
        #line default
        #line hidden
        
        
        #line 155 "..\..\..\ClientsInteraction.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button logoutButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.5.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/AllChatClient;component/clientsinteraction.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ClientsInteraction.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.usernameTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.listTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.messageTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.onlineUsersListBox = ((System.Windows.Controls.ListBox)(target));
            return;
            case 5:
            this.writeYourMessageHere = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.writeMessageTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 86 "..\..\..\ClientsInteraction.xaml"
            this.writeMessageTextBox.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.writeMessageClicked);
            
            #line default
            #line hidden
            return;
            case 7:
            this.sendButton = ((System.Windows.Controls.Button)(target));
            
            #line 98 "..\..\..\ClientsInteraction.xaml"
            this.sendButton.Click += new System.Windows.RoutedEventHandler(this.sendButtonClicked);
            
            #line default
            #line hidden
            return;
            case 8:
            this.addFileButton = ((System.Windows.Controls.Button)(target));
            
            #line 131 "..\..\..\ClientsInteraction.xaml"
            this.addFileButton.Click += new System.Windows.RoutedEventHandler(this.addFileButtonClicked);
            
            #line default
            #line hidden
            return;
            case 9:
            this.logoutButton = ((System.Windows.Controls.Button)(target));
            
            #line 162 "..\..\..\ClientsInteraction.xaml"
            this.logoutButton.Click += new System.Windows.RoutedEventHandler(this.logoutButtonClicked);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

