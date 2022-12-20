using BaseRPG.Model.ReflectionStuff;
using BaseRPG.Model.ReflectionStuff.Attribute;
using BaseRPG.Model.Tickable.FightingEntity.Enemy;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Tickable.Item.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interaction.Kill
{
    [Interaction( typeof(Enemy),  typeof(Hero), interactionType: InteractionType.Attack)]
    public abstract class EnemyKillsHeroInteraction : IAttackInteraction
    {
        public abstract Enemy Enemy { get; }
        public abstract Hero Hero { get; }

        public void Attack()
        {
            Console.WriteLine("enemy killed hero");
        }
    }
}
