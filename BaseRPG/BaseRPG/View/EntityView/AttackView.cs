using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.View.Animation;
using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.EntityView
{
    public class AttackView:IDrawable
    {
        
        private Attack attack;
        private IImageRenderer imageRenderer;

        public AttackView(Attack attack, IImageRenderer imageRenderer, double initialRotation)
        {
            this.attack = attack;
            this.imageRenderer = imageRenderer;
            imageRenderer.SetImageRotation(initialRotation + Math.PI/2);
        }

        public Vector2D ObservedPosition => new(attack.Position.Values[0], attack.Position.Values[1]);

        public bool Exists => attack.Exists;

        public void OnRender(DrawingArgs drawingArgs)
        {
            var temp = imageRenderer.ImageRotation;
            imageRenderer.Render(drawingArgs);
        }
    }
}
