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
            currentWorldView = new WorldView.WorldView(game.CurrentWorld);
            
        }

        public void Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {

            args.DrawingSession.DrawImage(imageProvider.GetByFilename(@"C:\main\Munka_Suli\Egyetem\targyak\5.felev\temalab\BaseRPG\BaseRPG\Assets\image\characters\character1-outlined.png"), 0, 0);
            currentWorldView.Render(args);
        }
    }
}
