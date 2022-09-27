using BaseRPG.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Tickable.FightingEntity
{
    
    public class AttackabilityService
    {
        public enum Group {
            Friendly, Enemy, Neutral
        }
        private Dictionary<Group, List<Group>> canAttackMapping;
        private Dictionary<Group, List<Group>> CanAttackMapping { get; set; }
        public static Dictionary<Group, List<Group>> DefaultCanAttackMapping() {
            Dictionary<Group, List<Group>> mapping = new Dictionary<Group, List<Group>> ();
            mapping.Add(Group.Friendly, new List<Group> { Group.Enemy, Group.Neutral });
            mapping.Add(Group.Enemy, new List<Group> { Group.Friendly, Group.Neutral });
            mapping.Add(Group.Neutral, new List<Group> { Group.Friendly, Group.Enemy });
            return mapping;
        }
        public AttackabilityService(Dictionary<Group, List<Group>> canAttackMapping) {
            this.canAttackMapping = canAttackMapping;
        }

        public AttackabilityService()
        {
            this.canAttackMapping = DefaultCanAttackMapping();
        }

        public bool CanAttack(IAttacking attacker, IAttackable attacked)
        {
            return canAttackMapping[attacker.Group].Contains(attacked.Group);
        }
    }
}
