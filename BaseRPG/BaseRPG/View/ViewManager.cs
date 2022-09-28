using BaseRPG.Model.Game;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;


namespace BaseRPG.View
{
    public class ViewManager
    {
        private Game game;
        private WorldView.WorldView currentWorldView;
        public ViewManager(Game game)
        {
            this.game = game;
            currentWorldView = new WorldView.WorldView(game.CurrentWorld);
        }

        public void Draw(CanvasDrawEventArgs args)
        {
            currentWorldView.Render(args);
        }
    }
}
