using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interfaces.Skill
{
    public interface ISkillCaster
    {
        void Cast(ISkill skill);
    }
}
