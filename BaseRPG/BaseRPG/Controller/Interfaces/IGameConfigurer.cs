using BaseRPG.Controller.UnitControl;
using BaseRPG.Controller.UnitControl.ItemCollection;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Utility;
using BaseRPG.Physics.TwoDimensional.Collision;
using BaseRPG.View;
using BaseRPG.View.Animation;
using BaseRPG.View.EntityView;
using BaseRPG.View.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Interfaces
{
    public interface IGameConfigurer
    {
        IImageProvider ImageProvider { get; }
        AnimationProvider AnimationProvider { get; }
        DrawableProvider DrawableProvider { get; }
        InventoryControl InventoryControl { get; }
        SpellControl SpellControl { get; }
        ShopControl ShopControl { get; set; }

        void Configure(Controller controller, ViewManager viewManager, PositionObserver globalMousePositionObserver, MainWindow window);
    }
}
