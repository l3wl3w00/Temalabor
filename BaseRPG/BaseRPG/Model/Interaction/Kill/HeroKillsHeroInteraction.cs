using BaseRPG.Model.Interfaces.Combat;
using BaseRPG.Model.ReflectionStuff;
using BaseRPG.Model.ReflectionStuff.Attribute;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interaction.Kill
{
    [Interaction(typeof(Hero), typeof(Hero), interactionType: InteractionType.Attack)]
    public abstract class HeroKillsHeroInteraction : IAttackInteraction
    {
        public abstract Hero Attacker { get; }
        public abstract Hero Attackable { get; }

        public void Attack()
        {
            Console.WriteLine("hero killed hero");
        }
    }
}
