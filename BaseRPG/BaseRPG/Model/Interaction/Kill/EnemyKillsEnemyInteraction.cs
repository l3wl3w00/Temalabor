using BaseRPG.Model.Interfaces.Combat;
using BaseRPG.Model.ReflectionStuff;
using BaseRPG.Model.ReflectionStuff.Attribute;
using BaseRPG.Model.Tickable.FightingEntity.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interaction.Kill
{
    [Interaction(typeof(Enemy), typeof(Enemy), interactionType: InteractionType.Attack)]
    public abstract class EnemyKillsEnemyInteraction : IAttackInteraction
    {
        public abstract Enemy Attacker { get; }
        public abstract Enemy Attackable { get; }
        public void Attack()
        {
            Console.WriteLine("enemy killed enemy");
        }
    }
}
