using BaseRPG.Model.Tickable.FightingEntity.Enemy;
using BaseRPG.View.EntityView;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;

namespace BaseRPG.View.EntityView
{
    public class EnemyView : Drawable
    {
        private Enemy enemy;

        public EnemyView(Enemy enemy, ICanvasImage image = null)
        {
            this.enemy = enemy;
        }

        public void Render(CanvasDrawEventArgs args,Camera camera, CanvasControl sender)
        {
            throw new System.NotImplementedException();
        }
    }
}