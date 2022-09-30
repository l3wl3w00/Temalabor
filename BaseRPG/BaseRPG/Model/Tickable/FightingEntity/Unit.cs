using BaseRPG.Model.Attribute;
using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Combat;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Services;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Tickable.FightingEntity
{
    public abstract class Unit : IGameObject, IAttackable
    {
        private Health health;
        private PositionManager position;
        private Stat damage;

        public IPositionUnit Position { get { return position.Position; } }

        public AttackabilityService.Group Group { get; set; }

        public abstract void OnTick();

        public abstract void Attack();

        public void OnAttacked(IAttacking attacker) { }

        public int CalculateDamage() {
            return damage.Value;
        }
        public void Move(IMovementUnit movement) {
            position.Move(movement.Scaled(10));
        }
        public Unit(int maxHp, IPositionUnit initialPosition) {
            health = new Health(maxHp);
            this.position = new PositionManager(initialPosition);
        }

        //TODO ez baj? OCP-t szerintem nem sérti meg, minden osztály a saját tipusával felülírja
        // SRP-t egy kicsit lehet, de valahogy szét kell választanom a heterogén kollekciót a World-ben
        protected abstract string Type { get; }
        public void Separate(Dictionary<string, List<IGameObject>> dict)
        {
            string key = Type;
            if (dict.ContainsKey(key))
            {
                dict[key].Add(this);
                return;
            }
            dict.Add(key, new List<IGameObject> { this });
        }
    }
}
