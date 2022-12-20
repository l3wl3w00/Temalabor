using BaseRPG.Model.Interaction.Kill;
using BaseRPG.Model.Interfaces.Combat;
using BaseRPG.Model.ReflectionStuff.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.ReflectionStuff.InteractionBuilder
{

    public class AttackInteractionBuilder:InteractionBuilderBase<IAttacking,IAttackable,IAttackInteraction>
    {
        private static List<Type> attackInteractionTypes;

        public AttackInteractionBuilder() : base(InteractionType.Attack)
        {
        }

        protected override List<Type> InteractionTypes => attackInteractionTypes;

        public static void InitInteractionClasses(IEnumerable<Type> allTypes)
        {
            attackInteractionTypes = GetInteractionClasses(allTypes,InteractionType.Attack);
        }
    }
}
