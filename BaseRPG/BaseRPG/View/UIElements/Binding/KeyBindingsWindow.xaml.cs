using BaseRPG.Controller.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BaseRPG.View.UIElements
{
    public sealed partial class KeyBindingsWindow : CustomWindow
    {
        private readonly int numerOfColumns;
        public static string WindowName = "key-bindings";
        private BindingHandler bindingHandler;

        public KeyBindingsWindow(BindingHandler bindingHandler, int numerOfColumns)
        {
            this.InitializeComponent();
            this.bindingHandler = bindingHandler;
            this.numerOfColumns = numerOfColumns;
            AfterInit();
            
        }

        private void AfterInit() {
            CreateColumnDefinitions();
            FillPossibleActions();
        }
        private void CreateColumnDefinitions() {
            for (int i = 0; i < numerOfColumns; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }
        private void FillPossibleActions() {
            grid.RowDefinitions.Clear();
            grid.Children.Clear();
            int index = 0;
            foreach (var binding in bindingHandler.Bindings)
            {
                KeyBinding keyBinding = new KeyBinding(binding, bindingHandler);
                grid.Children.Add(keyBinding);
                if (index % numerOfColumns == 0) grid.RowDefinitions.Add(new RowDefinition());

                Grid.SetColumn(keyBinding, index % numerOfColumns);
                Grid.SetRow(keyBinding, index / numerOfColumns);
                index++;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            bindingHandler.Save();
        }
    }
}
