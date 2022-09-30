using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Combat;
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
        public event Action<Attack> AttackCreatedEvent;
        private IAttackFactory lightAttackFactory;
        private IAttackFactory heavyAttackFactory;

        public Weapon(IAttackFactory heavyAttackFactory, IAttackFactory lightAttackFactory)
        {
            this.heavyAttackFactory = heavyAttackFactory;
            this.lightAttackFactory = lightAttackFactory;
        }

        public void OnLightAttack(IAttackable attackable) {
            Attack attack = lightAttackFactory.CreateAttack(owner.Position);
            AttackCreatedEvent(attack);
        }
        public void OnHeavyAttack(IAttackable attackable) {
            Attack attack = heavyAttackFactory.CreateAttack(owner.Position);
            AttackCreatedEvent(attack);
        }
    }
}
