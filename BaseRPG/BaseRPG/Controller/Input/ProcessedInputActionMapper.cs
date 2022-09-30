using BaseRPG.Controller.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Input
{
    public class ProcessedInputActionMapper: IProcessedInputActionMapper
    {
        private Dictionary<string, Action> inputActionMap;
        public static ProcessedInputActionMapper CreateDefault(PlayerControl playerControl)
        {
            Dictionary<string, Action>  inputActionMap = new Dictionary<string, Action>();
            inputActionMap.Add("move-forward", () => { playerControl.Move(MoveDirection.Forward); });
            inputActionMap.Add("move-left", () => { playerControl.Move(MoveDirection.Left); });
            inputActionMap.Add("move-right", () => { playerControl.Move(MoveDirection.Right); });
            inputActionMap.Add("move-backward", () => { playerControl.Move(MoveDirection.Backward); });
            inputActionMap.Add("light-attack", () => { playerControl.LightAttack(); });
            inputActionMap.Add("heavy-attack-start", () => { playerControl.HeavyAttack(); });
            inputActionMap.Add("heavy-attack-finish", () => { });
            inputActionMap.Add("", () => { });
            return new ProcessedInputActionMapper(inputActionMap);
        }
        public ProcessedInputActionMapper(Dictionary<string, Action> inputActionMap)
        {
            this.inputActionMap = inputActionMap;
        }

        public ProcessedInputActionMapper()
        {
        }
        public Dictionary<string, Action> InputActionMap { get; set; }
        
        public Action ToAction(string key) {
            if (!inputActionMap.ContainsKey(key)) 
                return () => { };
            return inputActionMap[key];
        }
    }
}
