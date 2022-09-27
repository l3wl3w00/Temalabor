using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Input
{
    public class InputActionMapper
    {
        private Dictionary<string, Action> inputActionMap;
        public Dictionary<string, Action> InputActionMap { get; set; }
        public Dictionary<string, Action> CreateDefaultInputActionMapping(PlayerControl playerControl) {
            Dictionary<string, Action>  defaultInputActionMap = new Dictionary<string, Action>();
            defaultInputActionMap.Add("move-forward", () => { playerControl.Move(MoveDirection.Forward); });
            defaultInputActionMap.Add("move-left", () => { playerControl.Move(MoveDirection.Left); });
            defaultInputActionMap.Add("move-right", () => { playerControl.Move(MoveDirection.Right); });
            defaultInputActionMap.Add("move-backward", () => { playerControl.Move(MoveDirection.Backward); });
            defaultInputActionMap.Add("light-attack", () => { playerControl.LightAttack(); });
            defaultInputActionMap.Add("heavy-attack-start", () => { playerControl.HeavyAttack(); });
            defaultInputActionMap.Add("heavy-attack-finish", () => { });
            return defaultInputActionMap;
        }
        
    }
}
