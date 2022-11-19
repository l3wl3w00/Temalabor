﻿using BaseRPG.Controller.Interfaces;
using BaseRPG.Model.Effects.EffectParams;
using BaseRPG.Model.Tickable.FightingEntity;
using BaseRPG.Physics.TwoDimensional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Input.InputActions.Effect
{
    public class DamagingStunSkillOnPressInputAction : IInputAction
    {
        private Unit caster;
        private readonly CollisionNotifier2D collisionNotifier2D;

        public DamagingStunSkillOnPressInputAction(Unit caster, CollisionNotifier2D collisionNotifier2D)
        {
            this.caster = caster;
            this.collisionNotifier2D = collisionNotifier2D;
        }

        public void OnPressed() {
            var shapes = collisionNotifier2D.ShapesCollidingWithTrackedPosition;
            if (shapes.Count == 0) return;
            Unit target = null;
            foreach (var shape in shapes)
            {
                if (shape.Owner is Unit) {
                    target = shape.Owner as Unit;
                    break;
                }
            }
            if (target == null) return;
            TargetedEffectParams targetedEffectParams = new TargetedEffectParams(target);
            caster.CastSkill(3, targetedEffectParams);
            
        }
    }
}
