using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Input
{
    public class InputProcessor
    {
        public Dictionary<string, Action> InputActionMapping{ get; }
        public void Process(string input)
        {
            if (!InputActionMapping.ContainsKey(input)) return;
            InputActionMapping[input].Invoke();
        }
        public InputProcessor(Dictionary<string, Action> inputActionMapping)
        {
            this.InputActionMapping = inputActionMapping;
        }
        public InputProcessor(){
            InputActionMapping = new Dictionary<string, Action>();
        }
    }
}
