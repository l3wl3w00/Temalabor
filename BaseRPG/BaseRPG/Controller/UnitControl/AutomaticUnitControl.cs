

using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable.FightingEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.UnitControl
{

    public class AutomaticUnitControl:UnitControlBase
    {

        public AutomaticUnitControl(Unit unit):base(unit)
        {
        }
        
        public void Attack()
        {

        }
        public override IMovementUnit NextMovement(double delta) {
            if (ControlledUnit.NextMovement == null) {
                return null;
            }
            IMovementUnit movement = ControlledUnit.NextMovement.Scaled(delta);
            return movement;
        }

    }
}
