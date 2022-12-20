using BaseRPG.Model.Interaction.Collect;
using BaseRPG.Model.Interaction.Collide;
using BaseRPG.Model.Interaction.Kill;
using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Collecting;
using BaseRPG.Model.Interfaces.Collision;
using BaseRPG.Model.Interfaces.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.ReflectionStuff.InteractionBuilder.Factory
{
    public class ReflectionInteractionFactory : InteractionFactory
    {
        public override IAttackInteraction CreateAttackInteraction(IAttacking attacker, IAttackable attackable)
        {
            var result = create(new AttackInteractionBuilder(), attacker, attackable);
            if (result == null) return new EmptyAttackInteraction();
            return result;
        }

        public override ICollectInteraction CreateCollectInteraction(ICollector collector, ICollectible collectible)
        {
            var result = create(new CollectInteractorBuilder(), collector, collectible);
            if (result == null) return new EmptyCollectInteraction();
            return result;
        }

        public override ICollisionInteraction CreateCollisionInteraction(ICollisionDetector collidor1, ICollisionDetector collidor2)
        {
            var result = create(new CollisionInteractionBuilder(), collidor1, collidor2);
            if (result == null) return new EmptyCollisionInteraction();
            return result;
        }

        private INTERACTION_TYPE create<STARTER, REACTER, INTERACTION_TYPE>(
            InteractionBuilderBase<STARTER, REACTER, INTERACTION_TYPE> builder,
            STARTER starter,
            REACTER reacter)
                where REACTER : class
                where STARTER : class
                where INTERACTION_TYPE : class
        {
            builder.Starter = starter;
            builder.Reacter = reacter;
            return builder.Build();
        }
    }
}
