using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Attribute
{
    public class Health
    {
        
        public event Action HealthReachedZeroEvent;
        public event Action<int> HealthReachedMaxEvent;
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
                    HealthReachedMaxEvent(maxValue);
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
