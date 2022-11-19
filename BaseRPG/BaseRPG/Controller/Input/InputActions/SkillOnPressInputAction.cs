using BaseRPG.Controller.Interfaces;
using BaseRPG.Controller.UnitControl;
using BaseRPG.Model.Skills;
using BaseRPG.Model.Tickable.FightingEntity;
using BaseRPG.Model.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Input.InputActions
{
    public class SkillOnPressInputAction:IInputAction
    {
        private readonly Unit unit;
        private readonly int skillIndex;

        public SkillOnPressInputAction(Unit unit, int skillIndex)
        {
            this.unit = unit;
            this.skillIndex = skillIndex;
        }
        public void OnPressed()
        {
            unit.CastSkill(skillIndex, new object());
        }

    }
}
