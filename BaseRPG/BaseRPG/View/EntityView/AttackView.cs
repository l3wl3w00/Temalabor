using BaseRPG.Controller.UnitControl;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.View.Animation;
using BaseRPG.View.Image;
using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BaseRPG.View.EntityView
{
    public class AttackView:IDrawable
    {
        
        private Attack attack;
        private IImageRenderer imageRenderer;
        private bool exists = true;
        public AttackView(Attack attack, IImageRenderer imageRenderer, double initialRotation)
        {
            this.attack = attack;
            this.imageRenderer = imageRenderer;
            imageRenderer.SetImageRotation(initialRotation + Math.PI/2);
        }

        public Vector2D ObservedPosition => new(attack.Position.Values[0], attack.Position.Values[1]);

        public bool Exists => exists;

        public void OnRender(DrawingArgs drawingArgs)
        {
            if (!attack.Exists) {
                var timer = new Timer(200);
                timer.Elapsed += (a,b) => exists = false;
                timer.AutoReset = false;
                timer.Start();
            }
            var temp = imageRenderer.ImageRotation;
            imageRenderer.Render(drawingArgs);
        }
    }
}
