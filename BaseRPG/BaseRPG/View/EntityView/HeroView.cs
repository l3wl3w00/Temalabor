using BaseRPG.Model.Tickable.FightingEntity.Hero;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.EntityView
{
    public class HeroView:GameObjectView
    {
        private Hero hero;

        public HeroView(Hero hero)
        {
            this.hero = hero;
        }

        public override void Render(CanvasDrawEventArgs args, Camera camera)
        {
            DrawPicture(args, camera, hero.Position);
        }
    }
}
