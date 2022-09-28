using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View
{
    public class Camera
    {
        private int width;
        private int height;
        private Vector2D middlePosition;

        public Camera(Vector2D position, int width, int height)
        {
            this.middlePosition = position;
            this.width = width;
            this.height = height;
        }
        public Camera(Vector2D position, int width) {
            this.middlePosition = position;
            this.width = width;
            height = (int)Math.Round(9.0 / 16.0 * (double)width,0);
        }

        public Vector2D CalculatePositionOnScreen(Vector2D globalPosition) {
            throw new NotImplementedException();
        }
    }
}
