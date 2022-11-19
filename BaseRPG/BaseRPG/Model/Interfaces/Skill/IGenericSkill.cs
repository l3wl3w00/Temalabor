﻿using BaseRPG.Model.Utility;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Model.Interfaces.Skill
{
    public abstract class IGenericSkill<PARAM_TYPE> : ISkill where PARAM_TYPE : class 
    {
        public void Cast(object skillCastParams)
        {
            PARAM_TYPE @params = skillCastParams as PARAM_TYPE;
            if (@params == null) 
                throw new InvalidSkillCastParamsException(skillCastParams.GetType(),typeof(PARAM_TYPE));
            Cast(@params);
        }
        protected abstract void Cast(PARAM_TYPE skillCastParams);
    }
}