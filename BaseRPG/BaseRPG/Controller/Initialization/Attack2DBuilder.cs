using BaseRPG.Controller.UnitControl;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.Model.Utility;
using BaseRPG.Physics.TwoDimensional.Collision;
using BaseRPG.Physics.TwoDimensional.Movement;
using BaseRPG.View.EntityView;
using BaseRPG.View.Image;
using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Initialization
{
    public class Attack2DBuilder
    {
        private string imageName;
        private Attack attack;
        private List<Point2D> vertices;
        private IPositionUnit ownerPosition;
        private IImageProvider imageProvider;
        public Attack2DBuilder(string imageName)
        {
            this.imageName = imageName;
        }
        public Attack2DBuilder Attack(Attack attack)
        {
            this.attack = attack;
            return this;
        }
        public Attack2DBuilder OwnerPosition(IPositionUnit ownerPosition) {
            this.ownerPosition = ownerPosition;
            return this;
        }
        public Attack2DBuilder ImageProvider(IImageProvider imageProvider)
        {
            this.imageProvider = imageProvider;
            return this;
        }
        public Attack2DBuilder PolygonShape(List<Point2D> vertices) {
            this.vertices = vertices;
            return this;
        }
        public ShapeViewPair CreateAttack(double secondsAfterAttackDestroyed = 0,bool rotated = true )
        {
            if (attack == null)  throw new RequiredParameterMissing("attack was null");
            DefaultImageRenderer attackImageRenderer = new DefaultImageRenderer(
                imageProvider.GetByFilename(imageName),
                imageProvider.GetSizeByFilename(imageName)
            );
            var shape = new Polygon(attack, attack.MovementManager, vertices);


            double initialRotation = 0;
            if (rotated) { 
                double[] values = ownerPosition.MovementTo(attack.Position).Values;
                initialRotation = Math.Atan2(values[1], values[0]);
                shape.Rotate(initialRotation - Math.PI / 2);
            }

            ShapeViewPair fullAttackObject =
                new ShapeViewPair(
                    shape,
                    new AttackView(attack, attackImageRenderer, initialRotation, secondsAfterAttackDestroyed));
            return fullAttackObject;
        }

    }
}
