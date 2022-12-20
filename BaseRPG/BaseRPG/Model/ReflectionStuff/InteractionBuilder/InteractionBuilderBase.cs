using BaseRPG.Model.ReflectionStuff.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.ReflectionStuff.InteractionBuilder
{
    public abstract class InteractionBuilderBase<STARTER,REACTER,INTERACTION_TYPE>
        where REACTER : class 
        where STARTER : class
        where INTERACTION_TYPE : class
    {

        private List<Type> currentTypes;
        protected abstract List<Type> InteractionTypes { get; }
        public InteractionBuilderBase(InteractionType interactionType)
        {
            currentTypes = new List<Type>(InteractionTypes);
            this.interactionType = interactionType;
        }

        private REACTER reacter;
        private STARTER starter;
        private readonly InteractionType interactionType;

        public static List<Type> GetInteractionClasses(IEnumerable<Type> allTypes,InteractionType attributeType)
        {
            //TODO buggy if list is used instead of a hashset
            List<Type> result = new();
            foreach (var type in allTypes)
            {

                var attributes = GetInteractionAttribute(type,attributeType).ToList();
                if (attributes.Count > 0)
                {
                    var derived = TypeHelper.DerivedNonAbstractClasses(type, allTypes);
                    if (derived.Count > 0)
                    {
                        result.Add(derived.First());
                    }


                }
            }
            return result.ToList();
        }
        private static IEnumerable<InteractionAttribute> GetInteractionAttribute(Type type, InteractionType interactionType)
        {
            var attrs = type.GetCustomAttributes<InteractionAttribute>();
            return attrs.Where(a => 
                    a.InteractionType == interactionType
                    );
        }
        private InteractionAttribute GetInteractionAttribute(Type type) {
            return GetInteractionAttribute(type,interactionType)
                    .First();
        }
        public REACTER Reacter
        {
            set
            {
                currentTypes.RemoveAll(
                    type =>
                    !GetInteractionAttribute(type)
                    .InteractionReacterType.IsAssignableFrom(value.GetType())
                );
                reacter = value;
            }
        }
        public STARTER Starter
        {
            set
            {

                currentTypes.RemoveAll(type =>
                    !GetInteractionAttribute(type)
                    .InteractionStarterType.IsAssignableFrom(value.GetType())
                );
                starter = value;
            }
        }
        public INTERACTION_TYPE Build()
        {
            if (currentTypes. Count == 1)
            {
                var constructor = currentTypes[0].GetConstructor(new[] { starter.GetType(), reacter.GetType() });
                var attackInteraction = constructor.Invoke(new object[] { starter, reacter }) as INTERACTION_TYPE;
                return attackInteraction;
            }
            if(currentTypes. Count > 1)
                throw new Exception();
            return null;
        }
    }
}
