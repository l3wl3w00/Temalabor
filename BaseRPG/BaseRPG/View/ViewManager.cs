using BaseRPG.Model.Game;
using BaseRPG.Model.Worlds;
using BaseRPG.Physics.TwoDimensional.Interfaces;
using BaseRPG.View.Animation;
using BaseRPG.View.Camera;
using BaseRPG.View.Exceptions;
using BaseRPG.View.Interfaces;
using MathNet.Spatial.Euclidean;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;


namespace BaseRPG.View
{
    public class ViewManager
    {
        private Game game;
        private WorldView.WorldView currentWorldView;
        private IWorldNameImageMapper worldNameImageMapper;
        private CanvasVirtualControl canvas;
        public WorldView.WorldView CurrentWorldView { get { return currentWorldView; } set { currentWorldView = value; } }

        public Camera2D CurrentCamera { get { return currentWorldView.CurrentCamera; } set { currentWorldView.CurrentCamera = value; } }
        public IPositionProvider GlobalMousePositionProvider { get; set; }
        public CanvasVirtualControl Canvas { get => canvas; }

        public void SetCurrentWorldView(string worldName, World world,IImageProvider imageProvider, Camera2D camera) {
            
            canvas.SizeChanged += camera.OnCanvasSizeChanged;
            CurrentWorldView = new WorldView.WorldView(
                world,
                imageProvider.GetByFilename(worldNameImageMapper.ToImageName(worldName)),
                imageProvider.GetSizeByFilename(worldNameImageMapper.ToImageName(worldName)),
                camera
            );
            
        }
        public ViewManager(Game game, IWorldNameImageMapper worldNameImageMapper, CanvasVirtualControl canvas) {
            this.game = game;
            this.worldNameImageMapper = worldNameImageMapper;
            this.canvas = canvas;
        }

        public void Draw(DrawingArgs drawingArgs)
        {
            if (currentWorldView == null) {
                throw new ViewUninitializedException();
            }
            currentWorldView.Render(drawingArgs);
        }
    }
}
