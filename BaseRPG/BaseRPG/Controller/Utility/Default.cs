using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Utility
{
    public class Default<T> where T:class
    {
        private T defaultValue;
        private T currentValue;

        public T CurrentValue { get => currentValue; set => currentValue = value; }
        public T DefaultValue { get => defaultValue; set => defaultValue = value; }
        public bool IsDefault 
        {
            get {
                return CurrentValue == DefaultValue;
            }
        }
        public Default(T defaultValue, T currentValue)
        {
            this.DefaultValue = defaultValue;
            this.CurrentValue = currentValue;
        }
        public Default(T defaultValue)
        {
            this.DefaultValue = defaultValue;
            this.CurrentValue = defaultValue;
        }
        public void Reset() {
            CurrentValue = DefaultValue;
        }
        public static implicit operator Default<T>(T a) => new Default<T>(a);
    }
}
