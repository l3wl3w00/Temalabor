using BaseRPG.Model.Interfaces.Movement;
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

        void Configure(Controller controller, ViewManager viewManager);
    }
}
