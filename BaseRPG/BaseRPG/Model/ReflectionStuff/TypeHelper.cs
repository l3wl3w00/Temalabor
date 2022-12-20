using BaseRPG.Model.ReflectionStuff.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.ReflectionStuff
{
    internal class TypeHelper
    {
        public static IEnumerable<(Type, InteractionAttribute)> TypesWithAttribute(InteractionType interactionType)
        {
            // get all the types in the assembly
            Type[] types = Assembly.GetEntryAssembly().GetTypes();
            foreach (Type type in types)
            {
  
                //// get the custom attributes applied to the type
                var attributes = System.Attribute.GetCustomAttributes(type);

                //// iterate through the attributes
                foreach (System.Attribute attribute in attributes)
                {
                    // check if the attribute is of the correct type
                    if (attribute is not InteractionAttribute) continue;
                    if ((attribute as InteractionAttribute).InteractionType == interactionType)
                        yield return (type, attribute as InteractionAttribute);
                }
            }
        }
        public static HashSet<Type> DerivedNonAbstractClasses(Type baseType, IEnumerable<Type> types = null)
        {
            HashSet<Type> derivedClasses = new HashSet<Type>();
            // get all the types in the assembly
            if(types == null)
                types = Assembly.GetEntryAssembly().GetTypes();

            foreach (Type type in types)
            {
                if (type.IsInterface) continue;
                if (type.IsAbstract) continue;
                if (type == baseType) continue;
                if(baseType.IsAssignableFrom(type))
                    derivedClasses.Add(type);
            }
            return derivedClasses;
        }
        public static HashSet<Type> DerivedClasses(Type baseType)
        {
            HashSet<Type> derivedClasses = new HashSet<Type>();
            // get all the types in the assembly
            var types = Assembly.GetEntryAssembly().GetTypes().ToList();
            foreach (Type type in types)
            {
                if (type == baseType) continue;
                if (baseType.IsAssignableFrom(type))
                    derivedClasses.Add(type);
            }
            return derivedClasses;
        }

    }
}
