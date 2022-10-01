using BaseRPG.Controller;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Physics.TwoDimensional;
using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.UI;
using System;
using System.Numerics;

namespace BaseRPG.View.EntityView
{
    public class HeroView : Drawable
    {
        
        private Hero hero;
        private Transform2DEffect image;
        private MoveDirection defaultFacing = MoveDirection.Forward;
        private DirectionMovementUnitMapper directionMovementUnitMapper = DirectionMovementUnitMapper.CreateDefault(new PhysicsFactory2D());
        public HeroView(Hero hero, ICanvasImage image)
        {
            this.hero = hero;
            this.image = new Transform2DEffect
            {
                Source = image,
            };
        }

        public void Render(CanvasDrawEventArgs args, Camera camera,CanvasControl sender)
        {
            double[] values;
            if (hero.LastMovement == null)
            {
                values = directionMovementUnitMapper.FromDirection(defaultFacing).Values;
            }
            else
            {

                values = hero.LastMovement.Values;

                //Console.WriteLine("####################################### "+values[0]+ ", "+ values[1]);

            }
            Vector2D lastMovement = new(values[0], values[1]);
            Console.WriteLine("values: " + lastMovement.X + ", " + lastMovement.Y);
            double angle = Math.Atan2(lastMovement.Y, lastMovement.X) - Math.PI;

            Vector2D heroPos = new((float)hero.Position.Values[0], (float)hero.Position.Values[1]);
            this.image.TransformMatrix = Matrix3x2.CreateRotation((float)(angle - Math.PI/2),new(64,64));        
            //Console.WriteLine(image.Angle);
            args.DrawingSession.DrawImage(image, (float)heroPos.X, (float)heroPos.Y);
            //DrawPicture(args, camera, hero.Position);
        }
        //public override void BeforeImageDrawn(ref ICanvasImage drawnImage) {
        //    double[] values;
        //    if (hero.LastMovement == null)
        //    {
        //        values = new double[] { 0, -1 };
        //    }
        //    else {
        //        values = hero.LastMovement.Values;
        //    }
        //    Vector2D lastMovement = new Vector2D(values[0], values[1]);

        //    Angle rotation = lastMovement.AngleTo(new Vector2D(0, -1));

        //    image.Angle = (float)rotation.Radians;
            
        //}
    }
}
