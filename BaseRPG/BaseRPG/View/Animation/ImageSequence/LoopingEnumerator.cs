using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation.ImageSequence
{
    internal class LoopingEnumerator<T> : IEnumerator<T>
    {
        private List<T> images;
        private int index = 0;
        public LoopingEnumerator(List<T> images)
        {
            this.images = images;
        }

        public T Current => images[index];

        object IEnumerator.Current => images[index];

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            if (index < images.Count - 1)
                index++;
            else index = 0;
            return true;

        }

        public void Reset()
        {
            index = 0;
        }
    }
}
