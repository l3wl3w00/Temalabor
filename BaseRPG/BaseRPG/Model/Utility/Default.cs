using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Utility
{
    public class Default<T> where T : class
    {
        private T defaultValue;
        private T currentValue;

        public T CurrentValue { get => currentValue; set => currentValue = value; }
        public T DefaultValue { get => defaultValue; set => defaultValue = value; }
        public bool IsDefault
        {
            get
            {
                return CurrentValue == DefaultValue;
            }
        }
        public Default(T defaultValue, T currentValue)
        {
            DefaultValue = defaultValue;
            CurrentValue = currentValue;
        }
        public Default(T defaultValue)
        {
            DefaultValue = defaultValue;
            CurrentValue = defaultValue;
        }
        public void Reset()
        {
            CurrentValue = DefaultValue;
        }
        public static implicit operator Default<T>(T a) => new Default<T>(a);
    }

    public class DefaultInt
    {
        private int defaultValue;
        private int currentValue;

        public int CurrentValue { get => currentValue; set => currentValue = value; }
        public int DefaultValue { get => defaultValue; set => defaultValue = value; }
        public bool IsDefault
        {
            get
            {
                return CurrentValue == DefaultValue;
            }
        }
        public DefaultInt(int defaultValue, int currentValue)
        {
            DefaultValue = defaultValue;
            CurrentValue = currentValue;
        }
        public DefaultInt(int defaultValue)
        {
            DefaultValue = defaultValue;
            CurrentValue = defaultValue;
        }
        public void Reset()
        {
            CurrentValue = DefaultValue;
        }
    }
}
