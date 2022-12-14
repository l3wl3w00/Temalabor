using BaseRPG.Controller.Interfaces;
using BaseRPG.Controller.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Input
{
    public class RawInputProcessedInputMapper: IRawInputProcessedInputMapper
    {
        private BindingHandler binding;

        public RawInputProcessedInputMapper(BindingHandler binding)
        {
            this.binding = binding;
        }


        public IEnumerable<string> toProcessedInput(string input) {
            //if (!binding.HasInput(input)) { return ""; }
            return binding.BindingOf(input);
        }

        public static RawInputProcessedInputMapper CreateDefault(string filePath) {
            RawInputProcessedInputMapper rawInputProcessedInputMapper = new RawInputProcessedInputMapper(new BindingHandler(filePath));
            //var mapping = rawInputProcessedInputMapper.binding;
            //mapping.Add("W", "move-forward");
            //mapping.Add("A", "move-left");
            //mapping.Add("D", "move-right");
            //mapping.Add("S", "move-backward");
            //mapping.Add("E", "skill-1");
            //mapping.Add("Q", "skill-2");
            //mapping.Add("Left", "move-left");
            //mapping.Add("Right", "move-right");
            //mapping.Add("Up", "move-forward");
            //mapping.Add("Down", "move-backward");
            //mapping.Add("MouseLeft", "light-attack");
            //mapping.Add("MouseLeft", "initiate-interaction");
            //mapping.Add("MouseRight", "heavy-attack");
            //mapping.Save();
            return rawInputProcessedInputMapper;
        }
        public static RawInputProcessedInputMapper FromBinding(BindingHandler bindingHandler)
        {
            RawInputProcessedInputMapper rawInputProcessedInputMapper = new RawInputProcessedInputMapper(bindingHandler);
            return rawInputProcessedInputMapper;
        }
    }
}
