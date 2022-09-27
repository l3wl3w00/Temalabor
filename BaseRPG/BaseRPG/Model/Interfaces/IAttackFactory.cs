using BaseRPG.Model.Tickable.Item.Weapon;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interfaces
{
    public interface IAttackFactory
    {
        public static event Action<Attack> AttackCreatedEvent;
        public Attack CreateAttack(Vector2D position);
    }
}
