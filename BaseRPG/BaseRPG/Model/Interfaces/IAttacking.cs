using BaseRPG.Model.Tickable.FightingEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interfaces
{
    public interface IAttacking
    {
        AttackabilityService.Group Group { get; set; }
        int Damage { get; }
    }
}
