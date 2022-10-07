using BaseRPG.Model.Services;
using BaseRPG.Model.Tickable.FightingEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interfaces.Combat
{
    public interface IAttackable
    {
        AttackabilityService.Group Group { get; set; }
        void TakeDamage(double damage);
    }
}
