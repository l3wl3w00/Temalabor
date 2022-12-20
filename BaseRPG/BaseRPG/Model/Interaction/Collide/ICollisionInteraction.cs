using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interaction.Collide
{
    public interface ICollisionInteraction
    {
        void OnCollide(double delta);
    }
}
