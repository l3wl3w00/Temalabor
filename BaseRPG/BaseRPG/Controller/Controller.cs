using BaseRPG.Controller.Input;
using Microsoft.UI.Input;
using Microsoft.UI.Xaml.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace BaseRPG.Controller
{
    public class Controller
    {
        private Dictionary<string, string> inputMapping;
        private static Dictionary<string, string> defaultInputMapping;
        public static Dictionary<string, string> DefaultInputMapping
        {
            get {
                if (defaultInputMapping == null) {
                    defaultInputMapping = new Dictionary<string, string>();
                    defaultInputMapping.Add("W", "move-forward");
                    defaultInputMapping.Add("A", "move-left");
                    defaultInputMapping.Add("D", "move-right");
                    defaultInputMapping.Add("S", "move-backward");
                    defaultInputMapping.Add("MouseDownLeft", "light-attack");
                    defaultInputMapping.Add("MouseDownRight", "heavy-attack-start");
                    defaultInputMapping.Add("MouseUpRight", "heavy-attack-finish");
                }
                return defaultInputMapping;
            }
        }
        private InputProcessor inputProcessor;
        public Controller(InputProcessor inputProcessor, Dictionary<string, string> inputMapping)
        {
            this.inputProcessor = inputProcessor;
            this.inputMapping = inputMapping;
        }

        public void MouseDown(PointerPoint point)
        {
            if (point.Properties.IsLeftButtonPressed)
                inputProcessor.Process(inputMapping["MouseDownLeft"]);
            if (point.Properties.IsRightButtonPressed)
                inputProcessor.Process(inputMapping["MouseDownRight"]);

        }

        public void MouseUp(PointerPoint point)
        {

        }

        public void KeyDown(KeyRoutedEventArgs e)
        {
            inputProcessor.Process(e.Key.ToString());
        }
    }
}
