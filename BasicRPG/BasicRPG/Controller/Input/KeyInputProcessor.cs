using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicRPG.Controller.Input
{
    public class KeyInputProcessor
    {
        private static Dictionary<String, Action> keyinputMap;
        public static void InitInputMap()
        {
            keyinputMap = new Dictionary<String, Action>();
            keyinputMap.Add("W", () => { Console.WriteLine("move forward"); });
        }
        public void Process(string v)
        {
            keyinputMap[v].Invoke();
        }
    }
}
