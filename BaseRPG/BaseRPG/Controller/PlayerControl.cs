using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Physics.TwoDimensional;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller
{
    public enum MoveDirection { 
        Forward, Backward, Left, Right
    }


    public class PlayerControl
    {

        private Hero player;
        private DirectionMovementUnitMapper directionVectorMapper;

        public PlayerControl(Hero player, IPhysicsFactory physicsFactory)
        {
            directionVectorMapper = DirectionMovementUnitMapper.CreateDefault(physicsFactory);
            this.player = player;
        }

        public void Move(MoveDirection moveDirection) {
            IMovementUnit vec = directionVectorMapper.FromDirection(moveDirection);
            player.Move(vec);
        }
        public void LightAttack() {
        
        }
        public void HeavyAttack() {
        
        }
        public void UseSpell() {
        
        }
    }
}
