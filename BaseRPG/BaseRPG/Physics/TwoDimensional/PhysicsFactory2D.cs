using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Physics.TwoDimensional.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional
{
    public class PhysicsFactory2D : IPhysicsFactory
    {
        public IPositionUnit Origin => new PositionUnit2D(0,0);

        public IMovementUnit CreateMovement(params double[] args)
        {
            FillMissing(0, args);
            return new MovementUnit2D(args[0], args[1]);
        }

        public IPositionUnit CreatePosition(params double[] args)
        {
            FillMissing(0, args);
            return new PositionUnit2D(args[0], args[1]);
        }
        private double[] FillMissing(double defaultValue, double[] args) {
            
            // If args has less elements than 2, then the rest needs to be filled with 0
            if (args.Length < 2) {
                double[] result = new double[] { 0, 0 };
                for (int i = 0; i < args.Length; i++)
                    result[i] = args[i];
                return result;
            }
            // Otherwise it doesn't matter because only the first 2 elements are considered 
            return args;
        }
    }
}
