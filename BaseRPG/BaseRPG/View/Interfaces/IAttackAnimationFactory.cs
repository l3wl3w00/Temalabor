using BaseRPG.Model.Tickable.Attacks;
using BaseRPG.View.Animation.ImageSequence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Interfaces
{
    public interface IAttackAnimationFactory
    {
        
        TransformationAnimation2D CreateTransformation(AttackBuilder attackBuilder);
        ImageSequenceAnimation CreateImageSequence(AttackBuilder attackBuilder);
    }
}
