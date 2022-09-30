using BasicRPG.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicRPG.Model.Tickable.Item.Weapon
{
    public class Weapon:Item
    {
        private IWeaponAttackStrategy weaponAttackStrategy;
        public void OnLightAttackHit(IAttackable attackable) {
            weaponAttackStrategy.LightAttack();
        }
        public void OnHeavyAttackHit(IAttackable attackable) {
            weaponAttackStrategy.HeavyAttack();
        }
    }
}
