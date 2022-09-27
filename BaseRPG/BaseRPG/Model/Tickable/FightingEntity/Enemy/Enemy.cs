using BaseRPG.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Tickable.FightingEntity.Enemy
{
    public class Enemy : Unit
    {
        private IAttackFactory attackFactory;
        public override void Attack()
        {
            attackFactory.CreateAttack(Position);
        }

        public override void OnTick()
        {
            throw new NotImplementedException();
        }
    }
}
