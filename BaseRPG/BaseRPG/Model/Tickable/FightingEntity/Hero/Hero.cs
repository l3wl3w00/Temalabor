using BaseRPG.Model.Attribute;
using BaseRPG.Model.Interfaces.Collecting;
using BaseRPG.Model.Interfaces.Combat;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Services;
using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.Physics.TwoDimensional.Movement;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Tickable.FightingEntity.Hero
{
    public class Hero : Unit, ICollector<Item.Item>, ICollector<ICollectible>
    {
        protected override string Type { get { return "Hero"; } }
        private Model.Attribute.Inventory inventory;
        private Model.Attribute.ExperienceManager experienceManager;


        public override AttackabilityService.Group OffensiveGroup => AttackabilityService.Group.Friendly;
        public override AttackabilityService.Group DefensiveGroup => OffensiveGroup;

        public Hero(int maxHp, IMovementManager movementManager ) :
            base(maxHp, movementManager, new EmptyMovementStrategy())
        {
            inventory = new();
            
        }

        //public void Collect(ICollectible collectible)
        //{
        //    inventory.Collect(collectible);
        //}
        public override AttackBuilder AttackFactory(string attackName)
        {
            if (attackName == "light")
                return inventory.EquippedWeapon.LightAttackFactory;
            return null;
        }
        private void LightAttack()
        {
            //inventory.EquippedWeapon.OnLightAttack();
        }
        public override void OnTick(double delta)
        
        {
            base.OnTick(delta);
            //throw new NotImplementedException();
        }

        public void Collect(Item.Item collectible)
        {
            inventory.Collect(collectible);
        }
        public void Equip(Weapon weapon) {
            inventory.EquippedWeapon = weapon;
        }

        public void Collect(ICollectible collectible)
        {
            
        }
    }
}
