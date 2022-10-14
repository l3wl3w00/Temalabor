using BaseRPG.Model.Interfaces;
using BaseRPG.Physics.TwoDimensional.Collision;
using BaseRPG.View.EntityView;
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

        public FullGameObject2D(IGameObject gameObject, IShape2D shape, IDrawable view)
        {
            this.GameObject = gameObject;
            this.Shape = shape;
            this.View = view;
            
        }

        public IDrawable View { get => view; set => view = value; }
        public IShape2D Shape { get => shape; set => shape = value; }
        public IGameObject GameObject { get => gameObject; set => gameObject = value; }
    }
}
