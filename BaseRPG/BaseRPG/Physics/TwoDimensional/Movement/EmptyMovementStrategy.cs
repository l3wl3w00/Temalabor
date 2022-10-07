using BaseRPG.Model.Interfaces.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional.Movement
{
    public class EmptyMovementStrategy : IMovementStrategy
    {
        public IMovementUnit CalculateNextMovement(IMovementManager mover, double speed)
        {
            return null;
        }

    }
}
