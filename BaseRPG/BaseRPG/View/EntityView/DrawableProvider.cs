using BaseRPG.Model.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.EntityView
{
    public class DrawableProvider
    {
        private OneToManyProvider<object, IDrawable> provider = new();
        //private List<DrawableGameObjectPair> _list = new();
        //private Dictionary<string, IDrawable> lastRequestCache = new();
        //private object lastRequestParam;
        public void Connect(IDrawable drawable, object gameObject, string name = "default") {
            provider.Connect(gameObject, drawable, name);
            //var drawables = GetDrawablesOf(gameObject);
            //if (drawables == null) { 
            //    _list.Add(new(drawable, gameObject,name));
            //    return;
            //}
            //drawables.Add(name,drawable);
        }
        public bool AreConnected(IDrawable drawable, object gameObject) {
            //Dictionary<string, IDrawable> drawables = GetDrawablesOf(gameObject);
            //if (drawables == null) return false;
            //return drawables.Values.Contains(drawable);
            return provider.AreConnected(drawable, gameObject);
        }
        
        public Dictionary<string, IDrawable> GetDrawablesOf(object gameObject) {
            //lastRequestParam = gameObject;
            //lastRequestCache = _list.Find(p => p.GameObject == gameObject)?.Drawables;
            //return lastRequestCache;
            return provider.GetObjectsOf(gameObject);
        }
        public List<IDrawable> GetDrawablesAsListOf(object gameObject)
        {
            //return GetDrawablesOf(gameObject).Values.ToList();
            return provider.GetObjectsAsListOf(gameObject);
        }
        public IDrawable GetDrawable(object gameObject,string name, bool fromCache = false){

            //Dictionary<string, IDrawable> drawables = null;
            //if (fromCache) {
            //    if (lastRequestParam == gameObject)
            //        drawables = lastRequestCache;
            //}
            //else 
            //    drawables = GetDrawablesOf(gameObject);

            //if (drawables == null) return null;
            //if (!drawables.ContainsKey(name)) { 
            //    return null;
            //}
            //return drawables[name];
            return provider.GetObject(gameObject, name, fromCache);
        }
        public T GetDrawable<T>(object gameObject, string name, bool fromCache = false) where T : class, IDrawable
        {
            //return GetDrawable(gameObject,name,fromCache) as T;
            return provider.GetObject<T>(gameObject, name, fromCache);
        }

        //private class DrawableGameObjectPair {
        //    private Dictionary<string,IDrawable> drawables = new();
        //    private object gameObject;

        //    public DrawableGameObjectPair(IDrawable drawable, object gameObject, string name)
        //    {
        //        this.gameObject = gameObject;
        //        drawables.Add(name, drawable);
        //    }
        //    public Dictionary<string, IDrawable> Drawables => drawables;
        //    public object GameObject => gameObject;
        //}
    }
}
