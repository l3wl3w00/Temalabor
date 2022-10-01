using BaseRPG.View.Exceptions;
using BaseRPG.View.Interfaces;
using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;

namespace BaseRPG.View.Image
{
    public class RawImageProvider : IImageProvider
    {
        private Dictionary<string, ICanvasImage> images = new Dictionary<string, ICanvasImage>();
        private List<IAsyncOperation<CanvasBitmap>> unfinishedTasks = new();
        private bool initialized = false;
        public async Task LoadImages(ICanvasResourceCreator canvasResourceCreator)
        {
            var characterImage = @"C:\main\Munka_Suli\Egyetem\targyak\5.felev\temalab\BaseRPG\BaseRPG\Assets\image\characters\character1-outlined.png";
            var loadCharacterImage = CanvasBitmap.LoadAsync(canvasResourceCreator, characterImage);

            var backgroundImage = @"C:\main\Munka_Suli\Egyetem\targyak\5.felev\temalab\BaseRPG\BaseRPG\Assets\image\bacground\big-background-mozaic.jpg";
            var loadBackgroundImage = CanvasBitmap.LoadAsync(canvasResourceCreator, backgroundImage);

            images.Add("character1", await loadCharacterImage);
            images.Add("background", await loadBackgroundImage);
            initialized = true;
        }

        public ICanvasImage GetByFilename(string fileName)
        {
            if (!initialized) {
                throw new ImageProviderUninitializedException();
            } 
            if (images.ContainsKey(fileName))
            {
                return images[fileName];
            }
            throw new NoSuchFileException("image doesn't exist: " + fileName);
        }

    }
}
