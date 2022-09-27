﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interfaces
{
    public interface IAttackStrategy
    {
        void OnAttackHit(IAttacking attacker, IAttackable attacked);
    }
}
