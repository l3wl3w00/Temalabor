using BaseRPG.Model.Interfaces.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Attribute
{
    public class MovementManager
    {
        private IPositionUnit position;
        private IMovementUnit lastMovement;

        public MovementManager(IPositionUnit position)
        {
            this.position = position;
        }

        public IPositionUnit Position { get { return position; } }
        public void Move(IMovementUnit movement)
        {
            lastMovement = movement;
            position.MoveBy(lastMovement);
        }
        public IMovementUnit LastMovement => lastMovement;
    }
}
