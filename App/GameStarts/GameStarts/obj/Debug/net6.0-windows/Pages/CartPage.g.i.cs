﻿#pragma checksum "..\..\..\..\Pages\CartPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A54E2D62EDF21CEB4C2DA08558FC2557B707922D"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using GameStarts.Pages;
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


namespace GameStarts.Pages {
    
    
    /// <summary>
    /// CartPage
    /// </summary>
    public partial class CartPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\..\Pages\CartPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel MainCart;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\..\Pages\CartPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ClearCart;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\..\Pages\CartPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BuyButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.1.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/GameStarts;component/pages/cartpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Pages\CartPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.1.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.MainCart = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 2:
            this.ClearCart = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\..\Pages\CartPage.xaml"
            this.ClearCart.Click += new System.Windows.RoutedEventHandler(this.ClearCart_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.BuyButton = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\..\..\Pages\CartPage.xaml"
            this.BuyButton.Click += new System.Windows.RoutedEventHandler(this.BuyButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

