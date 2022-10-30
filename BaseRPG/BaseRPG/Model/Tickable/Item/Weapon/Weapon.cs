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
        private readonly AttackBuilder lightAttackFactory;
        private readonly AttackBuilder heavyAttackFactory;

        

        public Unit Owner { 
            get => owner;
            set
            {
                owner = value;
                heavyAttackFactory.Attacker(owner);
                lightAttackFactory.Attacker(owner);
                
            } 
        }
        public AttackBuilder LightAttackFactory { 
            get {
                return lightAttackFactory;
            }  }

        public Weapon(AttackBuilder heavyAttackFactory, AttackBuilder lightAttackFactory, Unit owner = null)
        {
            this.heavyAttackFactory = heavyAttackFactory;
            this.lightAttackFactory = lightAttackFactory;
            this.Owner = owner;
        }

        
    }
}
