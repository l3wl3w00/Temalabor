using BasicRPG.Controller.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml.Input;

namespace BasicRPG.Controller
{
    public class Controller
    {
        private readonly KeyInputProcessor keyInputProcessor;

        public Controller(KeyInputProcessor keyInputProcessor)
        {
            this.keyInputProcessor = keyInputProcessor;
        }

        public void MouseDown(PointerRoutedEventArgs e)
        {
        }

        public void MouseUp(PointerRoutedEventArgs e)
        {
        }

        public void KeyDown(KeyRoutedEventArgs e)
        {
            keyInputProcessor.Process(e.Key.ToString());
        }
    }
}
