using BaseRPG.Model.Tickable.FightingEntity.Enemy;
using BaseRPG.View.EntityView;
using Microsoft.Graphics.Canvas.UI.Xaml;

namespace BaseRPG.View.EntityView
{
    public class EnemyView : GameObjectView
    {
        private Enemy enemy;

        public EnemyView(Enemy enemy)
        {
            this.enemy = enemy;
        }

        public override void Render(CanvasDrawEventArgs args,Camera camera)
        {
            throw new System.NotImplementedException();
        }
    }
}