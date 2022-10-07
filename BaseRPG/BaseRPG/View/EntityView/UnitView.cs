using BaseRPG.Controller;
using BaseRPG.Controller.UnitControl;
using BaseRPG.Model.Tickable.FightingEntity;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Physics.TwoDimensional;
using BaseRPG.View.Animation;
using BaseRPG.View.Image;
using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.UI;
using System;
using System.Numerics;

namespace BaseRPG.View.EntityView
{
    public class UnitView : IDrawable
    {
        private IImageRenderer imageRenderer;
        private Unit unit;
        private const MoveDirection defaultFacing = MoveDirection.Forward;
        private DirectionMovementUnitMapper directionMovementUnitMapper = DirectionMovementUnitMapper.CreateDefault2D();
        
        public UnitView(Unit unit, IImageRenderer imageRenderer)
        {
            this.unit = unit;
            this.imageRenderer = imageRenderer;
        }

        public Vector2D ObservedPosition => new(unit.Position.Values[0], unit.Position.Values[1]);

        public bool Exists => unit.Exists;

        public void OnRender(DrawingArgs drawingArgs)
        {
            double[] values;
            if (unit.LastMovement == null)
                values = directionMovementUnitMapper.FromDirection(defaultFacing).Values;
            else
                values = unit.LastMovement.Values;

            double angle = Math.Atan2(values[1], values[0]) + Math.PI / 2;
            imageRenderer.SetImageRotation(angle);
            imageRenderer.Render(drawingArgs);
        }


    }
}
