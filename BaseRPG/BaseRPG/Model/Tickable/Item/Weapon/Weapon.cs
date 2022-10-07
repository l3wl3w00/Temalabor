using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Combat;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable.FightingEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Tickable.Item.Weapon
{
    public class Weapon:Item
    {
        private Unit owner;
        public event Action<Attack> HeavyAttackCreatedEvent;
        public event Action<Attack> LightAttackCreatedEvent;
        private IAttackFactory lightAttackFactory;
        private IAttackFactory heavyAttackFactory;

        public Unit Owner { get => owner; set => owner = value; }

        public Weapon(IAttackFactory heavyAttackFactory, IAttackFactory lightAttackFactory, Unit owner = null)
        {
            this.heavyAttackFactory = heavyAttackFactory;
            this.lightAttackFactory = lightAttackFactory;
            this.Owner = owner;
        }

        public void CreateLightAttack(IPositionUnit attackPosition) {
  
            Attack attack = lightAttackFactory.CreateAttack(Owner, attackPosition);
            LightAttackCreatedEvent?.Invoke(attack);
        }
        public void CreateHeavyAttack() {
            Attack attack = heavyAttackFactory.CreateAttack(Owner,Owner.Position);
            HeavyAttackCreatedEvent?.Invoke(attack);
        }
    }
}
