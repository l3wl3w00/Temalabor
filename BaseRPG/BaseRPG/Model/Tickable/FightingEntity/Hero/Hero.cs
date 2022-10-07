using BaseRPG.Model.Attribute;
using BaseRPG.Model.Interfaces.Collecting;
using BaseRPG.Model.Interfaces.Combat;
using BaseRPG.Model.Interfaces.Movement;
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

        public Hero(int maxHp, IMovementManager movementManager,Dictionary<string,IAttackFactory> attacks ) :
            base(maxHp, movementManager, new EmptyMovementStrategy(), attacks)
        {
            inventory = new();
        }

        //public void Collect(ICollectible collectible)
        //{
        //    inventory.Collect(collectible);
        //}
        public void Attack(string attackName)
        {
            if (attackName == "light") { LigtAttack(); return;  }
        }
        private void LigtAttack()
        {
            //inventory.EquippedWeapon.OnLightAttack();
        }
        public override void OnTick()
        {
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
