using BaseRPG.Model.Interaction.Collide;
using BaseRPG.Model.Interfaces.Collision;
using BaseRPG.Model.ReflectionStuff.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.ReflectionStuff.InteractionBuilder
{
    public class CollisionInteractionBuilder: InteractionBuilderBase<ICollisionDetector, ICollisionDetector, ICollisionInteraction>
    {
        protected override List<Type> InteractionTypes => interactionTypes;
        private static List<Type> interactionTypes;

        public CollisionInteractionBuilder() : base(InteractionType.Collision)
        {
        }

        public static void InitInteractionClasses(IEnumerable<Type> allTypes)
        {
            interactionTypes = GetInteractionClasses(allTypes, InteractionType.Collision);
        }
}
}
