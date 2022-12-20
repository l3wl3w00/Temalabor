using BaseRPG.Model.ReflectionStuff;
using BaseRPG.Model.ReflectionStuff.InteractionBuilder;
using BaseRPG.Model.Services;
using BaseRPG.Model.Tickable.FightingEntity;
using BaseRPG.Model.Tickable.FightingEntity.Enemy;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interfaces.Combat
{
    public interface IAttackable
    {
        AttackabilityService.Group DefensiveGroup { get; }
        void TakeDamage(double damage, IAttacking attacker);
        void OnKilledBy(IAttacking attacker);
    }
}


