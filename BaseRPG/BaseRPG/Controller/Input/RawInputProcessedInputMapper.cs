using BaseRPG.Controller.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Input
{
    public class RawInputProcessedInputMapper: IRawInputProcessedInputMapper
    {
        private Dictionary<string, string> mapping = new Dictionary<string, string>();

        public RawInputProcessedInputMapper(Dictionary<string, string> mapping)
        {
            this.mapping = mapping;
        }

        public RawInputProcessedInputMapper()
        {
        }

        public string toProcessedInput(string input) {
            if (!mapping.ContainsKey(input)) { return ""; }
            return mapping[input];
        }

        public static RawInputProcessedInputMapper CreateDefault() {
            RawInputProcessedInputMapper rawInputProcessedInputMapper = new RawInputProcessedInputMapper();
            Dictionary<string, string> mapping = rawInputProcessedInputMapper.mapping;
            mapping.Add("W", "move-forward");
            mapping.Add("A", "move-left");
            mapping.Add("D", "move-right");
            mapping.Add("S", "move-backward");
            mapping.Add("Left", "move-left");
            mapping.Add("Right", "move-right");
            mapping.Add("Up", "move-forward");
            mapping.Add("Down", "move-backward");
            mapping.Add("MouseDownLeft", "light-attack");
            mapping.Add("MouseDownRight", "heavy-attack-start");
            mapping.Add("MouseUpRight", "heavy-attack-finish");
            return rawInputProcessedInputMapper;
        } 
    }
}
