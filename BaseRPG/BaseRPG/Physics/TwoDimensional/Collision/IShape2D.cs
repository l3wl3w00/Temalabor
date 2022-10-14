using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable.FightingEntity;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Spatial;
using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Physics.TwoDimensional.Collision
{
    public interface IShape2D
    {
        
        public Vector2D GlobalPosition { get; }
        public IMovementManager MovementManager { get; }
        public IGameObject Owner { get; }
        public Vector2D Middle { get; }
        void Rotate(double angle);
        Polygon2D ToPolygon();
        bool IsColliding(IShape2D s2);
        IShape2D Shifted(Vector2D shift);
        IShape2D Shifted(params double[] values);
    }
}
