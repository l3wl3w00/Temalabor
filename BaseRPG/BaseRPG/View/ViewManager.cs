using BaseRPG.Model.Game;
using BaseRPG.View.Interfaces;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;


namespace BaseRPG.View
{
    public class ViewManager
    {
        private Game game;
        private IImageProvider imageProvider;
        private WorldView.WorldView currentWorldView;
        public WorldView.WorldView CurrentWorldView { get { return currentWorldView; } set { currentWorldView = value; } }

        //internal IImageProvider ImageProvider { get => imageProvider; set => imageProvider = value; }

        public ViewManager(Game game, IImageProvider imageProvider)
        {
            this.imageProvider = imageProvider;
            this.game = game;
            currentWorldView = new WorldView.WorldView(game.CurrentWorld,imageProvider);
            
        }

        public void Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {

            
            currentWorldView.Render(sender,args);
            //args.DrawingSession.DrawImage(imageProvider.GetByFilename("character1"), 100, 100);
        }
    }
}
