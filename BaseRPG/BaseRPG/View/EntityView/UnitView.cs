using BaseRPG.Controller;
using BaseRPG.Controller.UnitControl;
using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Services;
using BaseRPG.Model.Tickable.FightingEntity;
using BaseRPG.Model.Tickable.FightingEntity.Enemy;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Physics.TwoDimensional;
using BaseRPG.View.Animation;
using BaseRPG.View.Animation.FacingPoint;
using BaseRPG.View.Animation.ImageSequence;
using BaseRPG.View.EntityView.Health;
using BaseRPG.View.Image;
using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.UI;
using System;
using System.Collections.Generic;
using System.Numerics;
using Windows.UI;

namespace BaseRPG.View.EntityView
{
    public class UnitView : IDrawable
    {
        private Unit unit;
        private HealthView healthView;
        private IAnimator animator;
        private readonly Dictionary<string, ImageSequenceAnimation> animations;

        private UnitView(Unit unit, HealthView healthView, IAnimator animator, Dictionary<string, ImageSequenceAnimation> animations)
        {
            this.unit = unit;
            this.healthView = healthView;
            this.animator = animator;
            this.animations = animations;
        }

        public Vector2D ObservedPosition => new(unit.Position.Values[0], unit.Position.Values[1]);

        public bool Exists => unit.Exists;

        public IAnimator Animator { get => animator; }

        public void OnRender(DrawingArgs drawingArgs)
        {
            Animator.Animate(drawingArgs);
            drawingArgs.PositionOnScreen -= 
                new Vector2D(
                    animator.Size.Item1 / 2,
                    animator.Size.Item2 / 1.2);
            healthView.Render(drawingArgs);
        }
        public void OnUnitMoved(
            FacingPointAnimation facingPointAnimation,
            DirectionMovementUnitMapper directionMovementUnitMapper,
            MoveDirection defaultFacing) 
        {
            double veryLargeNumber = 999999999999999D;
            double[] values = unit.MovementManager.LastMovement.Values;
            if (Math.Abs(values[0]) < double.Epsilon)
                if (Math.Abs(values[1]) < double.Epsilon)
                    values = directionMovementUnitMapper.FromDirection(defaultFacing).Values;
            facingPointAnimation.Point =
            new(values[0] * veryLargeNumber,
                values[1] * veryLargeNumber);
        }
        public class Builder {

            private IImageProvider imageProvider;
            private Color defaultHealthColor = Color.FromArgb(255,255,255,255);
            private Unit unit;
            private string imageName;
            private IAnimator animator;
            private const MoveDirection defaultFacing = MoveDirection.Forward;
            private Dictionary<string, ImageSequenceAnimation> animations = new();
            private DirectionMovementUnitMapper directionMovementUnitMapper = DirectionMovementUnitMapper.CreateDefault2D();

            public Builder(IImageProvider imageProvider, string imageName, Unit unit)
            {
                this.imageProvider = imageProvider;
                this.imageName = imageName;
                this.unit = unit;
            }
                
            private Color _calculateHealthColor(Unit unit) {
                if (unit.OffensiveGroup == AttackabilityService.Group.Enemy)
                    return Color.FromArgb(255, 200, 0, 0);
                if (unit.OffensiveGroup == AttackabilityService.Group.Friendly)
                    return Color.FromArgb(255, 0, 150, 200);
                return defaultHealthColor;
            }
            public Builder Animator(IAnimator animator) {
                if (this.animator != null) throw new ParameterAlreadyDefined("The animator parameter is already defined");
                this.animator = animator;
                return this;
            }
            public Builder IdleAnimation(ImageSequenceAnimation animation) {
                animations.Add("idle", animation);
                return this;
            }
            public Builder Animation(string name, ImageSequenceAnimation animation) {
                animations.Add(name, animation);
                return this;
            }
            public Builder WithFacingPointAnimation() {
                if (animator != null) throw new ParameterAlreadyDefined("The animator parameter is already defined");

                FacingPointAnimation facingPointAnimation = new FacingPointAnimation(0);
                animator = new DefaultAnimator(
                    facingPointAnimation,
                    animations["idle"]
                );
                unit.MovementManager.Moved += () =>
                {
                    double[] values = unit.MovementManager.LastMovement.Values;
                    if (Math.Abs(values[0]) < double.Epsilon)
                        if (Math.Abs(values[1]) < double.Epsilon)
                            values = directionMovementUnitMapper.FromDirection(defaultFacing).Values;
                    double veryLargeNumber = 999999999999999D;
                    facingPointAnimation.Point =
                    new(values[0] * veryLargeNumber,
                        values[1] * veryLargeNumber);
                };
                return this;
            }
            public UnitView Build() {
                if (animator == null) throw new RequiredParameterMissing("The animator parameter is missing!");
                HealthView healthView = 
                    new(unit.Health,
                    (float)imageProvider.GetSizeByFilename(imageName).Item1,
                    _calculateHealthColor(unit));
                
                return new UnitView(unit, healthView, animator, animations); ;
            }
        }

        internal void StartAnimation(string v)
        {
            animator.Start(animations[v]);
        }
    }
}
