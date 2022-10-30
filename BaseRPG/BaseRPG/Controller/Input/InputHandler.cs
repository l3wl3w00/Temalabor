using BaseRPG.Controller.Interfaces;
using BaseRPG.View.Animation;
using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
using Microsoft.UI.Input;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Input
{
    public class InputHandler
    {
        private IProcessedInputActionMapper processedInputActionMapper;
        private IRawInputProcessedInputMapper rawInputProcessedInputMapper;
        private List<string> pressedButNotReleasedInput = new List<string>();
        private PositionTracker mousePositionTracker = new PositionTracker();
        public Vector2D MousePosition {
            get { return mousePositionTracker.Position; }
            set {
                mousePositionTracker.Position = new(value.X,value.Y);
            } 
        }
        public PositionTracker MousePositionTracker => mousePositionTracker;
        internal void OnTick()
        {

            lock (pressedButNotReleasedInput) {
                foreach (string input in pressedButNotReleasedInput)
                {
                    reactToPressedInput(input);
                }
            }
            
        }
        public void MouseDown(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint point = e.GetCurrentPoint((Canvas)sender);
            string input = "";
            if (point.Properties.IsLeftButtonPressed )
                input = "MouseDownLeft";
            if (point.Properties.IsRightButtonPressed)
                input = "MouseDownRight";
            reactToPressedInput(input);
            //reactToInputDown(input);
        }

        

        public void MouseUp(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint point = e.GetCurrentPoint((Canvas)sender);
            string input = "";
            if (point.Properties.IsLeftButtonPressed)
                input = "MouseDownLeft";
            if (point.Properties.IsRightButtonPressed)
                input = "MouseDownRight";
            //reactToInputUp(input);
        }

        public void KeyDown(object sender,KeyRoutedEventArgs e)
        {
            string key = e.Key.ToString();
            reactToInputDown(key);
        }

        internal void Initialize(
            IRawInputProcessedInputMapper rawInputProcessedInputMapper,
            IProcessedInputActionMapper processedInputActionMapper)
        {
            this.rawInputProcessedInputMapper = rawInputProcessedInputMapper;
            this.processedInputActionMapper = processedInputActionMapper;
        }

        public void KeyUp(object sender, KeyRoutedEventArgs e)
        {
            string key = e.Key.ToString();
            reactToInputUp(key);
        }
        public void MouseMoved(object sender, PointerRoutedEventArgs args)
        {
            Windows.Foundation.Point position = args.GetCurrentPoint((Canvas)sender).Position;
            MousePosition = new(position.X,position.Y);
        }

        private void reactToInputDown(string rawInput)
        {
            lock (pressedButNotReleasedInput) {
                if (pressedButNotReleasedInput.Contains(rawInput)) return;
                pressedButNotReleasedInput.Add(rawInput);
            }
            
        }

        

        private void reactToInputUp(string rawInput)
        {
            lock (pressedButNotReleasedInput)
            {
                pressedButNotReleasedInput.RemoveAll((s) => s == rawInput);
            }
            
        }
        private void reactToPressedInput(string rawInput) {
            Action action = processedInputActionMapper.ToAction(rawInputProcessedInputMapper.toProcessedInput(rawInput));
            action.Invoke();
        }
        
    }
}
