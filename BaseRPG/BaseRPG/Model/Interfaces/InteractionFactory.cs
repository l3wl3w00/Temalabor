using BaseRPG.Model.Interaction.Collect;
using BaseRPG.Model.Interaction.Collide;
using BaseRPG.Model.Interaction.Kill;
using BaseRPG.Model.Interfaces.Collecting;
using BaseRPG.Model.Interfaces.Collision;
using BaseRPG.Model.Interfaces.Combat;
using BaseRPG.Model.ReflectionStuff.InteractionBuilder.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interfaces
{
    public abstract class InteractionFactory
    {
        public static InteractionFactory Instance {get;} = new ReflectionInteractionFactory();
        public abstract ICollectInteraction CreateCollectInteraction(ICollector collector, ICollectible collectible);
        public abstract IAttackInteraction CreateAttackInteraction(IAttacking attacker, IAttackable attackable);
        public abstract ICollisionInteraction CreateCollisionInteraction(ICollisionDetector collidor1, ICollisionDetector collidor2);
    }
}
