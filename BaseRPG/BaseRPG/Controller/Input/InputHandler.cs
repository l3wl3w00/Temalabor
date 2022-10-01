using BaseRPG.Controller.Interfaces;
using Microsoft.UI.Input;
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

        public InputHandler(
            IRawInputProcessedInputMapper rawInputProcessedInputMapper,
            IProcessedInputActionMapper processedInputActionMapper)
        {
            this.rawInputProcessedInputMapper = rawInputProcessedInputMapper;
            this.processedInputActionMapper = processedInputActionMapper;

        }
        internal void OnTick()
        {
            if (pressedButNotReleasedInput.Count == 2) {
                var x = "";
            }

            lock (pressedButNotReleasedInput) {
                foreach (string input in pressedButNotReleasedInput)
                {
                    reactToPressedInput(input);
                }
            }
            
        }
        public void MouseDown(PointerPoint point)
        {
            string input = "";
            if (point.Properties.IsLeftButtonPressed)
                input = "MouseDownLeft";
            if (point.Properties.IsRightButtonPressed)
                input = "MouseDownRight";
            reactToInputDown(input);
        }

        

        public void MouseUp(PointerPoint point)
        {
            string input = "";
            if (point.Properties.IsLeftButtonPressed)
                input = "MouseDownLeft";
            if (point.Properties.IsRightButtonPressed)
                input = "MouseDownRight";
            reactToInputUp(input);
        }

        public void KeyDown(KeyRoutedEventArgs e)
        {
            string key = e.Key.ToString();
            reactToInputDown(key);
        }
        public void KeyUp(KeyRoutedEventArgs e)
        {
            string key = e.Key.ToString();
            reactToInputUp(key);
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
