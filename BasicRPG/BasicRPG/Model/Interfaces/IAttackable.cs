using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicRPG.Model.Interfaces
{
    public interface IAttackable
    {
        void OnAttacked(IAttacking attacker);
    }
}
