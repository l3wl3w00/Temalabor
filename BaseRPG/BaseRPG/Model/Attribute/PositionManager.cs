using BaseRPG.Model.Interfaces.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Attribute
{
    public class PositionManager
    {
        private IPositionUnit position;

        public PositionManager(IPositionUnit position)
        {
            this.position = position;
        }

        public IPositionUnit Position { get { return position; } }
        public void Move(IMovementUnit moveDirection)
        {
            position.MoveBy(moveDirection);
        }
    }
}
