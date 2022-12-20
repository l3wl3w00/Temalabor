using BaseRPG.Model.Interfaces.Collecting;
using BaseRPG.Model.Interfaces.Combat;
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
    [Interaction( typeof(Hero),  typeof(Enemy), interactionType: InteractionType.Attack)]
    public abstract class HeroKillsEnemyInteraction : IAttackInteraction
    {

        public void Attack()
        {
            Hero.ExperienceManager.GainExpirence(Enemy.XpValue * 10);
            Hero.GoldManager.GainGold(Enemy.GoldValue);
            Hero.Collect(new SimpleBowFactory().Create(Hero.CurrentWorld));
        }
        public abstract Enemy Enemy { get; }
        public abstract Hero Hero { get; }
    }
}
