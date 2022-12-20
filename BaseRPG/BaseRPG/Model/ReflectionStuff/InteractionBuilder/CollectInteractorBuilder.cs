using BaseRPG.Model.Interaction.Collect;
using BaseRPG.Model.Interfaces.Collecting;
using BaseRPG.Model.ReflectionStuff.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.ReflectionStuff.InteractionBuilder
{
    public class CollectInteractorBuilder : InteractionBuilderBase<ICollector, ICollectible, ICollectInteraction>
    {
        protected override List<Type> InteractionTypes => interactionTypes;
        private static List<Type> interactionTypes;

        public CollectInteractorBuilder() : base(InteractionType.Collection)
        {
        }

        public static void InitInteractionClasses(IEnumerable<Type> allTypes)
        {
            interactionTypes = GetInteractionClasses(allTypes, InteractionType.Collection);
        }
    }
}
