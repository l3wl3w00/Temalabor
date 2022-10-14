using BaseRPG.Model.Interfaces.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Skills
{
    public class SkillManager
    {
        private List<ISkill> skills = new();
        public void Learn(ISkill skill) {
            skills.Add(skill);
        }
        
    }
}
