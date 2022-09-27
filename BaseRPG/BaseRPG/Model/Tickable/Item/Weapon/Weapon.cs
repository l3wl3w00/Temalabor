using BaseRPG.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Tickable.Item.Weapon
{
    public class Weapon:Item
    {
        private IAttackFactory lightAttackFactory;
        private IAttackFactory heavyAttackFactory;
        public void OnLightAttack(IAttackable attackable) {
            lightAttackFactory.CreateAttack(new MathNet.Spatial.Euclidean.Vector2D());
        }
        public void OnHeavyAttack(IAttackable attackable) {
            heavyAttackFactory.CreateAttack(new MathNet.Spatial.Euclidean.Vector2D());
        }
    }
}
