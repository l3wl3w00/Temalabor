using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.EntityView
{
    public class DrawableProvider
    {
        private List<DrawableGameObjectPair> _list = new();
        public void Connect(IDrawable drawable, object gameObject, string name = "default") {
            var drawables = GetDrawablesOf(gameObject);
            if (drawables == null) { 
                _list.Add(new(drawable, gameObject,name));
                return;
            }
            drawables.Add(name,drawable);
        }
        public bool AreConnected(IDrawable drawable, object gameObject) {
            Dictionary<string, IDrawable> drawables = GetDrawablesOf(gameObject);
            if (drawables == null) return false;
            return drawables.Values.Contains(drawable);
        }
        
        public Dictionary<string, IDrawable> GetDrawablesOf(object gameObject) {
            return _list.Find(p => p.GameObject == gameObject)?.Drawables;
        }
        public List<IDrawable> GetDrawablesAsListOf(object gameObject)
        {
            return GetDrawablesOf(gameObject).Values.ToList();
        }
        public IDrawable GetDrawable(object gameObject,string name) {
            return GetDrawablesOf(gameObject)[name];
        }


        private class DrawableGameObjectPair {
            private Dictionary<string,IDrawable> drawables = new();
            private object gameObject;

            public DrawableGameObjectPair(IDrawable drawable, object gameObject, string name)
            {
                this.gameObject = gameObject;
                drawables.Add(name, drawable);
            }
            public Dictionary<string, IDrawable> Drawables => drawables;
            public object GameObject => gameObject;
        }
    }
}
