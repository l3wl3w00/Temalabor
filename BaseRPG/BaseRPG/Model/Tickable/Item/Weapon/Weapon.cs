using BaseRPG.Model.Attribute;
using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Model.Tickable.FightingEntity;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Worlds;
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
                heavyAttackFactory?.Attacker(owner);
                lightAttackFactory?.Attacker(owner);
                
            } 
        }
        public AttackBuilder LightAttackBuilder { 
            get {
                return lightAttackFactory;
            }  }

        public Weapon(AttackBuilder heavyAttackFactory, AttackBuilder lightAttackFactory, World world, Unit owner = null,int basePrice = 1):base(world, basePrice)
        {
            this.heavyAttackFactory = heavyAttackFactory.World(world);
            this.lightAttackFactory = lightAttackFactory.World(world);
            this.Owner = owner;
        }
        public override void OnCollectedByHero(Hero hero)
        {
            Owner = hero;
            base.OnCollectedByHero(hero);
        }
        public override void EquippedBy(Inventory inventory)
        {
            inventory.EquippedWeapon = this;
        }

        public override object Clone(int basePrice)
        {
            return new Weapon(heavyAttackFactory, lightAttackFactory, CurrentWorld,Owner, basePrice);
        }

        public class Builder {
            private AttackBuilder lightAttackFactory = null;
            private AttackBuilder heavyAttackFactory = null;
            private Unit owner = null;

            public Builder Owner(Unit owner)
            {
                this.owner = owner;
                return this;
            }

            public Builder LightAttackFactory(AttackBuilder lightAttackFactory) {
                this.lightAttackFactory = lightAttackFactory;
                return this;
            }
            public Builder HeavyAttackFactory(AttackBuilder heavyAttackFactory)
            {
                this.heavyAttackFactory = heavyAttackFactory;
                return this;
            }
        }
    }
}
