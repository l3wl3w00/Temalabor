using BaseRPG.Controller.Input.InputActions;
using BaseRPG.Controller.Interfaces;
using BaseRPG.Controller.UnitControl;
using BaseRPG.View.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Input
{
    public class ProcessedInputActionMapper : IProcessedInputActionMapper
    {
        private Dictionary<string, IInputAction> inputActionMap;
        public ProcessedInputActionMapper(Dictionary<string, IInputAction> inputActionMap)
        {
            this.inputActionMap = inputActionMap;
        }

        public ProcessedInputActionMapper()
        {
        }
        public Dictionary<string, Action> InputActionMap { get; set; }

        public IInputAction ToAction(string key) {
            if (!inputActionMap.ContainsKey(key))
                return new EmptyInputAction();
            return inputActionMap[key];
        }

        public class Builder{
            private Dictionary<string, IInputAction> inputActionMap = new();
            public Builder AddMapping(string name, IInputAction action) {
                inputActionMap.Add(name, action);
                return this;
            }
            public ProcessedInputActionMapper Create() {
                return new ProcessedInputActionMapper(inputActionMap);
            }
        }
    }
}
