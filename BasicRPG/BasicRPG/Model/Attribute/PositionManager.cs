using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicRPG.Model.Attribute
{
    public class PositionManager
    {
        private System.Numerics.Vector2 postion;
        public void Move(System.Numerics.Vector2 moveDirection)
        {
            postion += moveDirection;
        }
    }
}
