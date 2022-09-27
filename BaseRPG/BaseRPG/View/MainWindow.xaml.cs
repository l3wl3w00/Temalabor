using Microsoft.UI.Xaml;
using Microsoft.Graphics.Canvas.UI.Xaml;
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

namespace BaseRPG
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private readonly Controller.Controller controller;
        public MainWindow(Controller.Controller controller)
        {
            this.controller = controller;
            this.InitializeComponent();
            //canvasAnimatedControl.add_Draw( canvas_Draw);
        }
        void canvas_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        { 
        
        }

        private void PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            controller.MouseDown(e.GetCurrentPoint((CanvasControl)sender));
        }

        private void PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            controller.MouseUp(e.GetCurrentPoint((CanvasControl)sender));
        }

        private void KeyDown(object sender, KeyRoutedEventArgs e)
        {
            controller.KeyDown(e);
        }
    }
}
