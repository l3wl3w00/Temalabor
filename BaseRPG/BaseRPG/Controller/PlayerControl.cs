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
        private List<IMovementUnit> movements = new List<IMovementUnit>();
        public PlayerControl(Hero player, IPhysicsFactory physicsFactory)
        {
            directionVectorMapper = DirectionMovementUnitMapper.CreateDefault(physicsFactory);
            this.player = player;
        }

        public void OnMove(MoveDirection moveDirection) {
            IMovementUnit movement = directionVectorMapper.FromDirection(moveDirection);
            movements.Add(movement);
            
        }
        public void LightAttack() {
        
        }
        public void HeavyAttack() {
        
        }
        public void UseSpell() {
        
        }
        public void OnTick(double delta) {
            lock (this)
            {
                if (movements.Count == 0) return;
                IMovementUnit movementUnit = IMovementUnit.Unite(movements);
                player.Move(movementUnit.Scaled(delta*200));
                movements.Clear();
            }
            
            
        }
    }
}
