using BaseRPG.Model.Interfaces.Collision;
using BaseRPG.Model.ReflectionStuff.Attribute;
using BaseRPG.Model.Tickable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interaction.Collide
{
    [Interaction(typeof(InRangeDetector), typeof(ICollisionDetector), InteractionType.Collision)]
    public abstract class InRangeDetectorCollidesCollisionDetectorInteraction : ICollisionInteraction
    {
        public void OnCollide(double delta)
        {
            var objectsInRange = InRangeDetector.ObjectsInRange;
            if (!objectsInRange.Contains(CollisionDetector))
            {
                CollisionDetector.OnCeaseToExist += () => objectsInRange.Remove(CollisionDetector);
                objectsInRange.Add(CollisionDetector);
            }
            InRangeDetector.InvokeInRange(CollisionDetector);
        }
        public abstract InRangeDetector InRangeDetector{ get; }
        public abstract ICollisionDetector CollisionDetector{ get; }
    }

}
