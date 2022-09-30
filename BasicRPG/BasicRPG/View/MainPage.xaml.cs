using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BasicRPG
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly Controller.Controller controller;
        public MainPage()
        {
            this.InitializeComponent();
        }
        private void OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            controller.MouseDown(e);
        }

        private void OnPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            controller.MouseUp(e);
        }

        private void OnKeyDown(object sender, KeyRoutedEventArgs e)
        {
            controller.KeyDown(e);
        }
    }
}
