using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Tickable.FightingEntity;
using MathNet.Spatial.Euclidean;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace BaseRPG.View.Camera
{
    public class FollowingCamera2D:Camera2D
    {
        private IMovementManager observedPosition;
        public Unit FollowedUnit { set {
                FollowedPosition = value.MovementManager;
            } }
        public IMovementManager FollowedPosition {
            set {
                observedPosition = value;
                observedPosition.Moved += Update;
            } }
 
        public FollowingCamera2D(Vector2D position, Size size):base(position, size)
        {
            
        }
        public override void OnCanvasSizeChanged(object sender, SizeChangedEventArgs e)
        {
            base.OnCanvasSizeChanged(sender, e);
            Update();
        }
        public void Update() {

            MiddlePosition = new Vector2D(observedPosition.Position.Values[0], observedPosition.Position.Values[1]);
        }
    }
}
