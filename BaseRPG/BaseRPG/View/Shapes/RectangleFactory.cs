using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Shapes
{
    public class RectangleFactory:IShapeFactory
    {
        private Vector2D middle;
        private int width;
        private int height;
        private double rotation;

        public RectangleFactory(Vector2D middle, int width, int height, double rotation = 0)
        {
            this.middle = middle;
            this.width = width;
            this.height = height;
            this.rotation = rotation;
        }

        public Shape2D Create() {
            throw new NotImplementedException();
        }
    }
}
