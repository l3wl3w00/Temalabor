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
            var attackEffectImagePath = @"Assets\image\attacks\sword-attack-effect.png";
            var attackEffectImage = CanvasBitmap.LoadAsync(canvasResourceCreator, Path.Combine(projectPath, attackEffectImagePath));


            var slimeEnemyImagePath = @"Assets\image\enemies\slime-outlined.png";
            var loadSlimeEnemyImage = CanvasBitmap.LoadAsync(canvasResourceCreator, Path.Combine(projectPath, slimeEnemyImagePath));

            var characteImageOutlinedPath = @"Assets\image\characters\character1-outlined.png";
            var loadCharacterOutlinedImage = CanvasBitmap.LoadAsync(canvasResourceCreator, Path.Combine(projectPath, characteImageOutlinedPath));


            var backgroundImagePath = @"Assets\image\bacground\big-background-mozaic.jpg";
            var loadBackgroundImage = CanvasBitmap.LoadAsync(canvasResourceCreator, Path.Combine(projectPath, backgroundImagePath));

            var normalSwordImagePath = @"Assets\image\weapons\normal-sword-outlined.png";
            var loadNormalSwordImage = CanvasBitmap.LoadAsync(canvasResourceCreator, Path.Combine(projectPath, normalSwordImagePath));
            images.Add(attackEffectImagePath, await attackEffectImage);
            images.Add(slimeEnemyImagePath, await loadSlimeEnemyImage);
            images.Add(characteImageOutlinedPath, await loadCharacterOutlinedImage);
            images.Add(backgroundImagePath, await loadBackgroundImage);
            images.Add(normalSwordImagePath, await loadNormalSwordImage);
            initialized = true;
        }

        public ICanvasImage GetByFilename(string fileName)
        {
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
            var bitmap = GetByFilename(fileName) as CanvasBitmap;
            return new(bitmap.Size.Width, bitmap.Size.Height);
        }
    }
}
