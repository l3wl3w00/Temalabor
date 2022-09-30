using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Interfaces
{
    public interface IRawInputProcessedInputMapper
    {
        string toProcessedInput(string rawInput);
    }
}
