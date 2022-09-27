using BaseRPG.Model.Tickable.FightingEntity.Hero;
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

        public PlayerControl(Hero player)
        {
            this.player = player;
        }

        public void Move(MoveDirection moveDirection) {
            
        }
        public void LightAttack() {
        
        }
        public void HeavyAttack() {
        
        }
        public void UseSpell() {
        
        }
    }
}
