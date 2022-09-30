using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicRPG.Model.Attribute
{
    public delegate void HealthReachedZeroEvent();
    public class Health
    {
        
        public event HealthReachedZeroEvent HealthReachedZeroEvent;

        private int currentValue;
        private int maxValue;

        public Health(int maxValue)
        {
            this.maxValue = maxValue;
        }

        public int CurrentValue{
            get { return currentValue; }
            set { 
                currentValue = value;
                if (currentValue <= 0) {
                    HealthReachedZeroEvent();
                }
                if (currentValue > maxValue){
                    currentValue = maxValue;
                }
            }
        }
        public int MaxValue {
            get { return maxValue; } 
            set {
                var delta = (value - maxValue);
                maxValue = value;
                CurrentValue += delta;
            }
        }
        

    }
}
