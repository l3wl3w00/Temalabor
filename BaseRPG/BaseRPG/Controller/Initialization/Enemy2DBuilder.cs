using BaseRPG.Controller.UnitControl;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable;
using BaseRPG.Model.Tickable.FightingEntity.Enemy;
using BaseRPG.Physics.TwoDimensional.Collision;
using BaseRPG.Physics.TwoDimensional.Movement;
using BaseRPG.View.Animation;
using BaseRPG.View.EntityView;
using BaseRPG.View.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Initialization
{
    public class Enemy2DBuilder
    {
        private readonly Enemy enemy;
        private readonly IShape2D shape;
        private IDrawable view;
        private IPositionProvider positionProvider;
        private FullGameObject2D fullInRangeDetectorObject;
        public FullGameObject2D FullInRangeDetectorObject => fullInRangeDetectorObject;

        public Enemy2DBuilder(Enemy enemy, IShape2D shape) {
            this.enemy = enemy;
            this.shape = shape;
        }

        public Enemy2DBuilder PositionProvider(IPositionProvider positionProvider)
        {
            this.positionProvider = positionProvider;
            return this;
        }
        public Enemy2DBuilder WithDrawableView(IDrawable view)
        {
            this.view = view;
            return this;
        }
        public Enemy2DBuilder WithUnitView(UnitView unitView) {
            view = unitView;
            enemy.AttackableInRange += (a) =>
                unitView.StartAnimation("attack");
            return this;
        }
        public Enemy2DBuilder Range(double range) {

            fullInRangeDetectorObject = new FullGameObject2D(
                enemy.InRangeDetector,
                Polygon.Circle(
                    enemy.InRangeDetector,
                    enemy.MovementManager,
                    new(0, 0), range
                ),
                null,
                new PositionObserver(() => new(enemy.Position.Values[0], enemy.Position.Values[1]))
            );
            return this;
        }
        public FullGameObject2D Create() {

            
            FullGameObject2D fullGameObject2D = null;
            if (positionProvider == null)
                fullGameObject2D = new FullGameObject2D(enemy, shape, view);
            else fullGameObject2D = new FullGameObject2D(enemy, shape, view, positionProvider);

            return fullGameObject2D;
        }
    }
}
