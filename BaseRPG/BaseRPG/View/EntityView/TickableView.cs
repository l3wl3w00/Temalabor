using BaseRPG.Model.Interfaces;
using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.EntityView
{
    public class TickableView
    {
        private ITickable tickable;
        private ICanvasImage image;

        public TickableView(ITickable tickable,ICanvasImage image = null)
        {
            this.tickable = tickable;
            this.image = image;
        }

        public void Render() {
        
        }
    }
}
