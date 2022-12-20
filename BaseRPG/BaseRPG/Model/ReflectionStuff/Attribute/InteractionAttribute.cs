using BaseRPG.Model.ReflectionStuff.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.ReflectionStuff.Attribute
{
    public enum InteractionType {
        Collection,Attack,Collision
    }
    public class InteractionAttribute:System.Attribute
    {
        public Type InteractionStarterType { get; }
        public Type InteractionReacterType { get; }
        public InteractionType InteractionType { get; }

        public InteractionAttribute(Type interactionStarterType, Type interactionReacterType, InteractionType interactionType)
        {
            //if (!interationStarterInterface.IsAssignableFrom(interactionStarterType))
            //{
            //    throw new ArgumentException($"type must implement {interationStarterInterface.Name} interface", nameof(interactionStarterType));
            //}

            //if (!interationReacterInterface.IsAssignableFrom(interactionReacterType))
            //{
            //    throw new ArgumentException($"type must implement {interationReacterInterface.Name} interface", nameof(interactionReacterType));
            //}
            InteractionStarterType = interactionStarterType;
            InteractionReacterType = interactionReacterType;
            InteractionType = interactionType;
        }

        public static void MakeProperty(Type type, Type baseType, TypeBuilder typeBuilder, FieldBuilder field)
        {
            var propertyName = field.Name;// charArray.ToString();
            try
            {
                propertyName = baseType.GetProperties().Where(p => p.PropertyType.Equals(type)).Single().Name;

            }
            catch (InvalidOperationException e)
            {
                if (baseType.GetProperties().Count() == 0)
                    throw new NoValidPropertyException(type);

            }
            MethodBuilder getter = typeBuilder.DefineMethod("get_" + propertyName, MethodAttributes.Public | MethodAttributes.ReuseSlot | MethodAttributes.Virtual, type, Type.EmptyTypes);
            var generator = getter.GetILGenerator();

            generator.Emit(OpCodes.Ldarg_0); // Load "this" onto the stack
            generator.Emit(OpCodes.Ldfld, field); // Load the attackable field onto the stack
            generator.Emit(OpCodes.Ret); // Return

            PropertyBuilder property = typeBuilder.DefineProperty(propertyName, PropertyAttributes.None, type, null);
            property.SetGetMethod(getter);
        }
        
    }
}
