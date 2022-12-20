using BaseRPG.Model.Interfaces.Combat;
using BaseRPG.Model.ReflectionStuff.Attribute;
using BaseRPG.Model.Tickable.Attacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interaction.Collide
{
    [Interaction(typeof(Attack), typeof(IAttackable), InteractionType.Collision)]
    public abstract class AttackCollidesAttackableInteraction:ICollisionInteraction
    {
        public void OnCollide(double delta)
        {
            Attack.OnCollisionAttackable(Attackable, delta);
        }
        public abstract Attack Attack { get; }
        public abstract IAttackable Attackable { get; }
    }
}
