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

namespace BaseRPG.View.Shapes
{
    public class Shape2D
    {
        private Vector2D[] vertices;
        public Shape2D(Vector2D[] vertices) {
            this.vertices = vertices;
        }

        public Vector2D Middle {
            get {
                Vector2D sum = new Vector2D(0,0);
                foreach (Vector2D v in vertices) {
                    sum += v;
                }
                return sum/vertices.Length;
            }
            set { 
                Vector2D delta = value - Middle;
                for (int i = 0; i< vertices.Length; i++){
                    vertices[i] += delta;
                }
            }
        }

        public void Rotate(double angle) {
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = vertices[i].Rotate(Angle.FromRadians(angle));
            }
        }

        public void RotateInPlace(double angle)
        {
            Vector2D middle = Middle;
            Middle = new Vector2D(0, 0);
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = vertices[i].Rotate(Angle.FromRadians(angle));
            }
            Middle = middle;
        }

        public bool CollidesWith(Vector2D point) {
            throw new NotImplementedException();
        }
        public bool CollidesWith(Shape2D r2) {
            throw new NotImplementedException();
        }

    }
}
