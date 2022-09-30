using BaseRPG.View.Exceptions;
using BaseRPG.View.Interfaces;
using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Image
{
    public class RawImageProvider : IImageProvider
    {
        private Dictionary<string, ICanvasImage> images = new Dictionary<string, ICanvasImage>();
        public async Task LoadImages(ICanvasResourceCreator canvasResourceCreator)
        {
            var fileName = @"C:\main\Munka_Suli\Egyetem\targyak\5.felev\temalab\BaseRPG\BaseRPG\Assets\image\characters\character1-outlined.png";
            images.Add(fileName, await CanvasBitmap.LoadAsync(canvasResourceCreator, fileName));
        }

        public ICanvasImage GetByFilename(string fileName)
        {
            if (images.ContainsKey(fileName))
            {
                return images[fileName];
            }
            throw new NoSuchFileException("image doesn't exist: " + fileName);
        }

    }
}
