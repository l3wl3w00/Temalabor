using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseRPG.Model.Attribute;
using BaseRPG.Physics.TwoDimensional.Movement;
using BaseRPG.View.Animation;
using MathNet.Spatial.Euclidean;
using Windows.UI;

namespace BaseRPG.View.EntityView.Health
{
    public class HealthView
    {
        private Model.Attribute.Health health;
        private readonly Color color;
        private float width;
        private float height;
        public float Width { 
            set 
            {
                width = value;
                height = value / 8;
            } 
        }
        public HealthView(Model.Attribute.Health health, float width, Color color)
        {
            this.health = health;
            this.Width = width;
            this.color = color;
        }

        public void Render(DrawingArgs drawingArgs)
        {
           
            drawingArgs.DrawingSession.FillRectangle(
                (float)drawingArgs.PositionOnScreen.X,
                (float)drawingArgs.PositionOnScreen.Y,
                (float)(width * (health.CurrentValue/health.MaxValue)),
                height,
                color);
            
            drawingArgs.DrawingSession.FillRectangle(
                (float)drawingArgs.PositionOnScreen.X + width/20,
                (float)drawingArgs.PositionOnScreen.Y + height / 2,
                (float)(width * (health.CurrentValue / health.MaxValue))- width / 10,
                height / 2,
                Brighten(color,50));

            drawingArgs.DrawingSession.DrawRectangle(
                (float)drawingArgs.PositionOnScreen.X,
                (float)drawingArgs.PositionOnScreen.Y,
                (float)width,
                height,
                Color.FromArgb(255, 255, 255, 255),4);
        }
        private Color Brighten(Color color, byte amount) {
            var result = color;
            
            if (result.R >= 255 - amount)
                result.R = 255;
            else result.R += amount;
            if (result.G >= 255 - amount)
                result.G = 255;
            else result.G += amount;
            if (result.B >= 255 - amount)
                result.B = 255;
            else result.B += amount;
            return result;
        }

    }
}
