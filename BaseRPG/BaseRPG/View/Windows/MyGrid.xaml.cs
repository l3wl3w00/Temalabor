﻿using Microsoft.UI.Xaml;
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

namespace BaseRPG.View.Windows
{
    public sealed partial class MyGrid : UserControl
    {
        private Grid grid;
        private int rows;
        public int Rows {
            get { return rows; } 
            set 
            { 
                rows = value;
                var currentRows = grid.RowDefinitions.Count;
            }
        }
        public MyGrid()
        {
            this.InitializeComponent();
        }
    }
}
