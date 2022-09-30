using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interfaces.Movement
{
    public interface IMovementUnit
    {
        IMovementUnit Scaled(double scalar);
        double[] Values { get; }
    }
}
