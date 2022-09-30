using BaseRPG.Model.Interfaces;
using BaseRPG.Model.Tickable.Item;
using BaseRPG.Physics.TwoDimensional.Collision;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.ItemView
{
    public class EquippedItemView : BaseItemView
    {
        private Shape2D hitbox;

        public EquippedItemView(Item item) : base(item)
        {
        }

        public override void Render(CanvasDrawEventArgs args, Camera camera)
        {
            throw new NotImplementedException();
        }

        public void StartHeavyAttackChargeAnimation() {
            throw new NotImplementedException();
        }

        public void StartHeavyAttackStrikeAnimation()
        {
            throw new NotImplementedException();
        }

        public void StartLightAttackAnimation() {
            throw new NotImplementedException();
        }
    }
}
