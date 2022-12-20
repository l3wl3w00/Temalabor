using BaseRPG.Model.Interaction.Collect;
using BaseRPG.Model.Interaction.Collide;
using BaseRPG.Model.Interaction.Kill;
using BaseRPG.Model.ReflectionStuff.Attribute;
using BaseRPG.Model.ReflectionStuff.InteractionBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.ReflectionStuff.Generation
{
    public class AttibureImplGenerator
    {
        private Type interactionInterface;
        private readonly string interactionStarterName;
        private readonly string interactionReacterName;
        private static ModuleBuilder moduleBuilder;
        private InteractionType interactionType;

        public AttibureImplGenerator(Type interactionInterface, string interactionStarterName, string interactionReacterName, InteractionType interactionType)
        {
            this.interactionInterface = interactionInterface;
            this.interactionStarterName = interactionStarterName;
            this.interactionReacterName = interactionReacterName;
            this.interactionType = interactionType;
        }
        public Type GenerateImplementation(Type baseType)
        {

            if (moduleBuilder == null) { 
                AssemblyName assemblyName = new AssemblyName("MyDynamicAssembly");
                AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
                moduleBuilder = assemblyBuilder.DefineDynamicModule("MyDynamicModule");
            }


            // Get the AttackInteractionAttribute attribute from the base class
            InteractionAttribute attribute = baseType
                .GetCustomAttributes<InteractionAttribute>(inherit: false)
                .Where(a => a.InteractionType == interactionType).First();

            // Get the types of the attacker and attackable from the attribute
            Type interactionStarterType = attribute.InteractionStarterType;
            Type interactionReacterType = attribute.InteractionReacterType;

            // Create a new type that derives from HeroKillsEnemyInteractionBase
            TypeBuilder typeBuilder = moduleBuilder.DefineType(baseType.Name + "Impl", TypeAttributes.Public, baseType);

            // Define a field for the attacker
            FieldBuilder interactionStarterField = typeBuilder.DefineField(interactionStarterName, interactionStarterType, FieldAttributes.Private);

            // Define a field for the attackable
            FieldBuilder interactionReacterField = typeBuilder.DefineField(interactionReacterName, interactionReacterType, FieldAttributes.Private);

            // Define a constructor that takes in the attacker and attackable and sets the fields
            Type[] constructorTypes = new Type[] { interactionStarterType, interactionReacterType };
            ConstructorBuilder constructorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, constructorTypes);
            ILGenerator constructorIL = constructorBuilder.GetILGenerator();
            constructorIL.Emit(OpCodes.Ldarg_0); // Load "this" onto the stack
            constructorIL.Emit(OpCodes.Ldarg_1); // Load the attacker argument onto the stack
            constructorIL.Emit(OpCodes.Stfld, interactionStarterField); // Set the attacker field
            constructorIL.Emit(OpCodes.Ldarg_0); // Load "this" onto the stack
            constructorIL.Emit(OpCodes.Ldarg_2); // Load the attackable argument onto the stack
            constructorIL.Emit(OpCodes.Stfld, interactionReacterField); // Set the attackable field
            constructorIL.Emit(OpCodes.Ret); // Return

            InteractionAttribute.MakeProperty(interactionReacterType, baseType, typeBuilder, interactionReacterField);
            InteractionAttribute.MakeProperty(interactionStarterType, baseType, typeBuilder, interactionStarterField);
            var result = typeBuilder.CreateType();
            return result;

        }
        public HashSet<Type> GenerateImplementations(HashSet<Type> types)
        {
            return types.Select(t => t.IsAbstract ? GenerateImplementation(t) : t).ToHashSet();
        }
        public void GenerateAll(Action<IEnumerable<Type>> afterInteractionsInitialized) {
            var interactions = GenerateImplementations(TypeHelper.DerivedClasses(interactionInterface));
            afterInteractionsInitialized(Assembly.GetExecutingAssembly().GetTypes().Union(interactions));
            //AttackInteractionBuilder.InitInteractionClasses(Assembly.GetExecutingAssembly().GetTypes().Union(interactions));
            // get all the types in the assembly
            foreach (var pair in TypeHelper.TypesWithAttribute(interactionType))
            {
                var type = pair.Item1;
                if (type.IsInterface || type.IsAbstract)
                {
                    type = TypeHelper.DerivedNonAbstractClasses(type, interactions.ToArray()).First();
                }
                var attr = pair.Item2;
                if (!type.IsAssignableTo(interactionInterface))
                    throw new Exception($"Only {interactionInterface.Name} implementations should have an Interaction attribute");
                if (!interactions.Contains(type))
                    throw new Exception($"All {interactionInterface.Name} implementations should have an Interaction attribute");
                interactions.Remove(type);
            }
        }

        

    }
    public static class GeneratorInitializer {
        public static void GenerateAll()
        {
            new AttibureImplGenerator(typeof(IAttackInteraction),"Attacker","Attackable",InteractionType.Attack)
                .GenerateAll(types => AttackInteractionBuilder.InitInteractionClasses(types));

            new AttibureImplGenerator(typeof(ICollectInteraction), "Collector", "Collectible", InteractionType.Collection)
                .GenerateAll(types => CollectInteractorBuilder.InitInteractionClasses(types));

            new AttibureImplGenerator(typeof(ICollisionInteraction), "Collidor1", "Collidor2", InteractionType.Collision)
                .GenerateAll(types => CollisionInteractionBuilder.InitInteractionClasses(types));
        }
    }
}
