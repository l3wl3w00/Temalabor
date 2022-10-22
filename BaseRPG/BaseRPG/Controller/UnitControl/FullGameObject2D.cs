using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Worlds;
using BaseRPG.Physics.TwoDimensional;
using BaseRPG.Physics.TwoDimensional.Collision;
using BaseRPG.View.Animation;
using BaseRPG.View.EntityView;
using BaseRPG.View.Interfaces;
using BaseRPG.View.WorldView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.UnitControl
{
    public class FullGameObject2D
    {
        private IGameObject gameObject;
        private IShape2D shape;
        private IDrawable view;
        private readonly IPositionProvider positionProvider;

        public FullGameObject2D(IGameObject gameObject, IShape2D shape, IDrawable view, IPositionProvider positionProvider)
        {
            this.gameObject = gameObject;
            this.shape = shape;
            this.view = view;
            this.positionProvider = positionProvider;
        }

        
        public FullGameObject2D(IGameObject gameObject, IShape2D shape, IDrawable view):
            this(gameObject,  shape,  view, new PositionObserver(() => view.ObservedPosition))
        {

        }

        internal void AddTo(WorldView worldView, World world, CollisionNotifier2D collisionNotifier)
        {
            world.Add(gameObject);
            if (view != null)
                worldView.AddView(view);
            if (shape == null) return;

            if (System.Diagnostics.Debugger.IsAttached)
            {
                worldView.AddView(new ShapeView(shape, positionProvider));
            }

            collisionNotifier.AddToObservedShapes(shape);
        }
    }
}
