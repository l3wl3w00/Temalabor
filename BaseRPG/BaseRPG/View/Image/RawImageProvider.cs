using BaseRPG.View.Exceptions;
using BaseRPG.View.Interfaces;
using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using Windows.Foundation;

namespace BaseRPG.View.Image
{
    public class RawImageProvider : IImageProvider
    {
        private Dictionary<string, ICanvasImage> images = new Dictionary<string, ICanvasImage>();
        private bool initialized = false;
        public async Task LoadImages(ICanvasResourceCreator canvasResourceCreator)
        {
            string projectPath =  AppDomain.CurrentDomain.BaseDirectory;

            List<string> pics = new List<string> 
            {
                @"Assets\image\enemies\attack-animation\slime-attack-0-outlined.png",
                @"Assets\image\enemies\attack-animation\slime-attack-1-outlined.png",
                @"Assets\image\enemies\attack-animation\slime-attack-2-outlined.png",
                @"Assets\image\enemies\attack-animation\slime-attack-3-outlined.png",
                @"Assets\image\attacks\sword-attack-effect.png",
                @"Assets\image\enemies\slime-outlined.png",
                @"Assets\image\characters\character1-outlined.png",
                @"Assets\image\bacground\big-background-mozaic.jpg",
                @"Assets\image\weapons\normal-sword-outlined.png",
                @"Assets\image\attacks\enemy-attack.png"
            };

            foreach (string p in pics)
                images.Add(p, await CanvasBitmap.LoadAsync(canvasResourceCreator, Path.Combine(projectPath, p)));
            initialized = true;
        }
        public ICanvasImage GetByFilename(string fileName)
        {
            if (fileName == null) return null;
            if (!initialized)
            {
                //This exception is also thrown if an image couldn't be loaded
                throw new ImageProviderUninitializedException();
            }
            if (images.ContainsKey(fileName))
            {
                return images[fileName];
            }
            throw new NoSuchFileException("image doesn't exist: " + fileName);
        }

        public Tuple<double, double> GetSizeByFilename(string fileName)
        {
            if (fileName == null) return null;
            var bitmap = GetByFilename(fileName) as CanvasBitmap;
            return new(bitmap.Size.Width, bitmap.Size.Height);
        }
    }
}
