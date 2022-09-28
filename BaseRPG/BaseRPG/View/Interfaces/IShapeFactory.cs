using BaseRPG.View.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Interfaces
{
    public interface IShapeFactory
    {
        Shape2D Create();
    }
}
