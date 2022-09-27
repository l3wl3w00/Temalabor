using BaseRPG.Model.Attribute;
using BaseRPG.Model.Interfaces;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Tickable.FightingEntity
{
    public abstract class Unit : ITickable, IAttackable
    {
        private Health health;
        private PositionManager position;
        private Stat damage;

        public Vector2D Position { get { return position.Position; } }

        public AttackabilityService.Group Group { get; set; }

        public abstract void OnTick();

        public abstract void Attack();

        public void OnAttacked(IAttacking attacker) { }

        public int CalculateDamage() {
            return damage.Value;
        }
        public void Move() {
            
        }

    }
}
