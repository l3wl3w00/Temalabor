using BaseRPG.Model.Tickable.FightingEntity.Hero;
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
        private DirectionVectorMapper directionVectorMapper = DirectionVectorMapper.CreateDefault();
        public PlayerControl(Hero player)
        {
            this.player = player;
        }

        public void Move(MoveDirection moveDirection) {
            Vector2D vec = directionVectorMapper.FromDirection(moveDirection);
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
