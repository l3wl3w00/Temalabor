using BaseRPG.Model.ReflectionStuff.Attribute;
using BaseRPG.Model.Tickable.FightingEntity.Enemy;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interaction.Collide
{
    [Interaction(typeof(Hero),typeof(Enemy),InteractionType.Collision)]
    public abstract class HeroCollidesEnemyInteraction : ICollisionInteraction
    {
        public void OnCollide(double delta)
        {

        }
        public abstract Hero Hero { get; }
        public abstract Enemy Enemy { get; }
    }
}
